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

            _handle = _gl.GenBuffer();//1.����һ�����л���ID��VBO��EBO����

            Bind();

            GlErrorException.ThrowIfError(gl);

            fixed (void* d = data)
            {
                _gl.BufferData(bufferType, (nuint) (data.Length * sizeof(T)), d, BufferUsageARB.StaticDraw);//3. �Ѵ�������ݸ��Ƶ�ָ��bufferType���͵Ļ����ڴ��С�StaticDraw��ʾ���ݲ���򼸺�����ı䣬DynamicDraw��ʾ���ݻᱻƵ���ı䣬StreamDraw��ʾ����ÿ�λ���ʱ����ı䣬Ƶ���ı�����ݻ�����ܹ�����д����ڴ沿�֡�
            }

            GlErrorException.ThrowIfError(gl);
        }

        public void Bind()
        {
            _gl.BindBuffer(_bufferType, _handle);//2. ������󶨵�ĳ���������͵��ڴ��ϣ�����VBO��Ŀ�꣨_bufferType����GL_ARRAY_BUFFER��EBO��Ŀ����GL_ELEMENT_ARRAY_BUFFER
        }

        public void Dispose()
        {
            _gl.DeleteBuffer(_handle);
        }
    }
}
