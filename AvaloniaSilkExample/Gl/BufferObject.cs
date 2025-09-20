using AvaloniaSilkExample.Gl;
using Silk.NET.OpenGL;
using System;

namespace Tutorial
{
    public class BufferObject<T> : IDisposable
        where T : unmanaged
    {
        private uint _handle;
        private BufferTargetARB _bufferType;
        private GL _gl;

        public unsafe BufferObject(GL gl, Span<T> data, BufferTargetARB bufferType)
        {
            _gl = gl;
            _bufferType = bufferType;

            //Clear existing error code.
            GLEnum error;
            do error = _gl.GetError();
            while (error != GLEnum.NoError);

            _handle = _gl.GenBuffer();//1.生成一个带有缓冲ID的VBO、EBO对象

            Bind();

            GlErrorException.ThrowIfError(gl);

            fixed (void* d = data)
            {
                _gl.BufferData(bufferType, (nuint) (data.Length * sizeof(T)), d, BufferUsageARB.StaticDraw);//3. 把传入的数据复制到指定bufferType类型的缓冲内存中。StaticDraw表示数据不会或几乎不会改变，DynamicDraw表示数据会被频繁改变，StreamDraw表示数据每次绘制时都会改变，频繁改变的数据会放在能够高速写入的内存部分。
            }

            GlErrorException.ThrowIfError(gl);
        }

        public void Bind()
        {
            _gl.BindBuffer(_bufferType, _handle);//2. 将缓冲绑定到某个缓冲类型的内存上，比如VBO绑定目标（_bufferType）是GL_ARRAY_BUFFER，EBO绑定目标是GL_ELEMENT_ARRAY_BUFFER
        }

        public void Dispose()
        {
            _gl.DeleteBuffer(_handle);
        }
    }
}
