using System;
using System.Drawing;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;
using Silk.NET.OpenGL;

namespace Tutorial
{
    public class SilkNetExampleControl : OpenGlControlBase
    {
        private GL _gl;
        private BufferObject<float> _vbo;
        private BufferObject<uint> _ebo;
        private VertexArrayObject<float, uint> _vao;
        private Shader _shader;

        private static readonly float[] Vertices =
        {
            //X    Y      Z     R  G  B  A
            0.5f,  0.5f, 0.0f, 1, 0, 0, 1,
            0.5f, -0.5f, 0.0f, 0, 0, 0, 1,
            -0.5f, -0.5f, 0.0f, 0, 0, 1, 1,
            -0.5f,  0.5f, 0.5f, 0, 0, 0, 1
        };

        private static readonly uint[] Indices =
        {
            0, 1, 3,
            1, 2, 3
        };



        protected override void OnOpenGlInit(GlInterface gl)
        {
            base.OnOpenGlInit(gl);
            
            _gl = GL.GetApi(gl.GetProcAddress);
            

            //Instantiating our new abstractions
            _ebo = new BufferObject<uint>(_gl, Indices, BufferTargetARB.ElementArrayBuffer);
            _vbo = new BufferObject<float>(_gl, Vertices, BufferTargetARB.ArrayBuffer);
            _vao = new VertexArrayObject<float, uint>(_gl, _vbo, _ebo);

            //Telling the VAO object how to lay out the attribute pointers
            _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 7, 0);
            _vao.VertexAttributePointer(1, 4, VertexAttribPointerType.Float, 7, 3);

            _shader = new Shader(_gl, "shader.vert", "shader.frag");

        }
       

        protected override void OnOpenGlDeinit(GlInterface gl)
        {
            _vbo.Dispose();
            _ebo.Dispose();
            _vao.Dispose();
            _shader.Dispose();
            base.OnOpenGlDeinit(gl);
        }

        protected override unsafe void OnOpenGlRender(GlInterface gl, int fb)
        {
            _gl.ClearColor(Color.White);//±³¾°É«
            _gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
            _gl.Enable(EnableCap.DepthTest);
            _gl.Viewport(0,0, (uint)Bounds.Width, (uint)Bounds.Height);
            
            _ebo.Bind();
            _vbo.Bind();
            _vao.Bind();
            _shader.Use();
            //_shader.SetUniform("uBlue", (float) Math.Sin(DateTime.Now.Millisecond / 1000f * Math.PI));

            _gl.DrawElements(PrimitiveType.Triangles, (uint) Indices.Length, DrawElementsType.UnsignedInt, null);
            Dispatcher.UIThread.Post(RequestNextFrameRendering, DispatcherPriority.Background);
        }
    }
}