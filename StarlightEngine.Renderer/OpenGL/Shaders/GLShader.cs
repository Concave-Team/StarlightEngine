using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlightEngine.Renderer.OpenGL.Shaders
{
    public class GLShader
    {
        uint fragmentShaderId;
        uint vertexShaderId;
        uint programId;

        public string fragmentShaderPath;
        public string vertexShaderPath;

        private string fragmentShaderSource;
        private string vertexShaderSource;

        public GL _GL;

        public Dictionary<string, int> _Uniforms = new Dictionary<string, int>();

        public void GLI_CreateShader()
        {
            fragmentShaderId = _GL.CreateShader(ShaderType.FragmentShader);
            vertexShaderId = _GL.CreateShader(ShaderType.VertexShader);
            _GL.ShaderSource(vertexShaderId, vertexShaderSource);
            _GL.ShaderSource(fragmentShaderId, fragmentShaderSource);

            _GL.CompileShader(fragmentShaderId);

            string info;
            _GL.GetShaderInfoLog(fragmentShaderId, out info);

            if (info != null)
                Console.WriteLine(info);

            _GL.CompileShader(vertexShaderId);

            _GL.GetShaderInfoLog(vertexShaderId, out info);

            if (info != null)
                Console.WriteLine(info);

            programId = _GL.CreateProgram();

            _GL.AttachShader(programId, fragmentShaderId);
            _GL.AttachShader(programId, vertexShaderId);

            _GL.LinkProgram(programId);

            _GL.GetProgramInfoLog(programId, out info);

            if (info != null)
                Console.WriteLine(info);

            _GL.DetachShader(programId, vertexShaderId);
            _GL.DetachShader(programId, fragmentShaderId);
            _GL.DeleteShader(vertexShaderId);
            _GL.DeleteShader(fragmentShaderId);

            int p;
            _GL.GetProgram(programId, GLEnum.ActiveUniforms, out p);

            for(int i = 0; i<p; i++)
            {
                var key = _GL.GetActiveUniform(programId, (uint)i, out _, out _);
                var loc = _GL.GetUniformLocation(programId, key);

                _Uniforms.Add(key, loc);
            }
        }

        public void Use()
        {
            _GL.UseProgram(programId);
        }

        public int GetAttribLoc(string name)
            => _GL.GetAttribLocation(programId, name);

        public int GetUniformLoc(string name)
        {
            if (_Uniforms.ContainsKey(name))
                return _Uniforms[name];
            throw new Exception("Uniform " + name + " cannot be found in the current shader.");
        }

        public void SetUniform(string name, int value)
        {
            _GL.Uniform1(GetUniformLoc(name), value);
        }

        public void SetUniform(string name, float value)
        {
            _GL.Uniform1(GetUniformLoc(name), value);
        }

        public void SetUniform(string name, System.Numerics.Vector2 value)
        {
            _GL.Uniform2(GetUniformLoc(name), value);
        }

        public void SetUniform(string name, System.Numerics.Vector3 value)
        {
            _GL.Uniform3(GetUniformLoc(name), value);
        }

        public void SetUniform(string name, System.Numerics.Vector4 value)
        {
            _GL.Uniform4(GetUniformLoc(name), value);
        }

        public unsafe void SetUniform(string name, System.Numerics.Matrix4x4 value)
        {
            _GL.UniformMatrix4(GetUniformLoc(name), 1, false, (float*) &value);
        }

        public void Destroy()
        {
            _GL.DeleteProgram(programId);
            GC.Collect();
        }

        public GLShader(GL API, string _fragmentShaderPath, string _vertexShaderPath, bool intern = false)
        {
            fragmentShaderPath = _fragmentShaderPath;
            vertexShaderSource = _vertexShaderPath;

            if (intern == false)
            {
                fragmentShaderSource = File.ReadAllText(_fragmentShaderPath);
                vertexShaderSource = File.ReadAllText(_vertexShaderPath);
            }
            else
            {
                fragmentShaderSource = _fragmentShaderPath;
                vertexShaderSource = _vertexShaderPath;
            }

            _GL = API;

            GLI_CreateShader();
        }
    }
}
