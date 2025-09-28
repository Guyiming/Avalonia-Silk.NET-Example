using System;
using System.IO;
using Silk.NET.OpenGL;

namespace Tutorial
{
    public class Shader : IDisposable
    {
        private uint _programID;
        private GL _gl;

        public Shader(GL gl, string vertexPath, string fragmentPath)
        {
            _gl = gl;

            uint vertex = LoadShader(ShaderType.VertexShader, vertexPath);
            uint fragment = LoadShader(ShaderType.FragmentShader, fragmentPath);
            _programID = _gl.CreateProgram();//4.������ɫ��������������ˡ�ͨ��Attach��Link������װ��һ����ɫ������
            _gl.AttachShader(_programID, vertex);
            _gl.AttachShader(_programID, fragment);
            _gl.LinkProgram(_programID);

            _gl.GetProgram(_programID, GLEnum.LinkStatus, out var status);//5.�������״̬
            if (status == 0)
            {
                throw new Exception($"Program failed to link with error: {_gl.GetProgramInfoLog(_programID)}");
            }

            _gl.DetachShader(_programID, vertex);//5. һ��Link�ɹ���GPU�����ͽ�������븴�Ƶ����Ǹ�Program�У�ԭ������ɫ�������������м��ļ����Ѿ�û���ˣ�����ɾ����
            _gl.DetachShader(_programID, fragment);
            _gl.DeleteShader(vertex);
            _gl.DeleteShader(fragment);
        }

        public void Use()
        {
            _gl.UseProgram(_programID);//6. ���������ɫ�����򣬿ɱ�ʹ��
        }

        public void SetUniform(string name, int value)
        {
            int location = _gl.GetUniformLocation(_programID, name);
            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }
            _gl.Uniform1(location, value);
        }

        public void SetUniform(string name, float value)
        {
            int location = _gl.GetUniformLocation(_programID, name);
            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }
            _gl.Uniform1(location, value);
        }

        public void Dispose()
        {
            _gl.DeleteProgram(_programID);
        }

        private uint LoadShader(ShaderType type, string path)
        {
            string src = File.ReadAllText(path);
            uint handle = _gl.CreateShader(type);//1.�������ʹ���һ���յ���ɫ������
            _gl.ShaderSource(handle, src);//2. ����ɫ��Դ�븽�ӵ���ɫ��������
            _gl.CompileShader(handle);//3.������

            string infoLog = _gl.GetShaderInfoLog(handle);
            if (!string.IsNullOrWhiteSpace(infoLog))
            {
                throw new Exception($"Error compiling shader of type {type}, failed with error {infoLog}");
            }

            return handle;
        }
    }
}
