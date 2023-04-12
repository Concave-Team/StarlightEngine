using Silk.NET.GLFW;
using Silk.NET.OpenGL;
using Silk.NET.Vulkan;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Glfw;
using StarlightEngine.Renderer.OpenGL.Internal;
using StarlightEngine.Renderer.OpenGL.Shaders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StarlightEngine.Renderer.OpenGL
{
    public class OGLRenderer : Renderer
    {
        public OGLWindow RenderContext;
        public GL OGL;
        public VAO vao;
        public VBO vbo;
        public EBO ebo;
        public GLShader LoadedShader;

        #region CollapseForReadability
        public override void DrawCube()
        {
            throw new NotImplementedException();
        }

        public override void DrawPlane()
        {
            throw new NotImplementedException();
        }

        public unsafe override void DrawTriangle() // not really.
        {
            float[] vertices = {
                0.5f,  0.5f, 0.0f,  // top right
                0.5f, -0.5f, 0.0f,  // bottom right
                -0.5f, -0.5f, 0.0f,  // bottom left
                -0.5f,  0.5f, 0.0f
            };

            uint[] indices = {
                0, 1, 3,
                1, 2, 3
            };

            OGL.BindBuffer(BufferTargetARB.ArrayBuffer, (uint)vbo.Id);
            OGL.BufferData<float>(GLEnum.ArrayBuffer, (nuint)vertices.Length*sizeof(float), vertices, BufferUsageARB.StaticDraw);

            OGL.BindBuffer(BufferTargetARB.ElementArrayBuffer, (uint)ebo.Id);
            OGL.BufferData<uint>(GLEnum.ElementArrayBuffer, (nuint)indices.Length * sizeof(uint), indices, BufferUsageARB.StaticDraw);
            
            OGL.BindVertexArray((uint)vao.Id);
            OGL.BindBuffer(BufferTargetARB.ElementArrayBuffer, (uint)ebo.Id);

            LoadedShader.Use();

            OGL.DrawElements(GLEnum.Triangles, 6, DrawElementsType.UnsignedInt, (void*)0);
            OGL.BindVertexArray(0);
        }

        public override void DrawPoint()
        {
            throw new NotImplementedException();
        }

        public override void DrawEntity()
        {
            throw new NotImplementedException();
        }

        public GLShader CreateShader(string VSPath, string FSPath)
        {
            return new GLShader(OGL, FSPath, VSPath);
        }

        public GLShader CreateShaderI(string VSSrc, string FSSrc)
        {
            return new GLShader(OGL, FSSrc, VSSrc, true);
        }

        public VAO CreateVAO()
        {
            return new VAO((int)OGL.GenVertexArray());
        }

        public VBO CreateVBO()
        {
            return new VBO((int)OGL.GenBuffer());
        }

        public EBO CreateEBO()
        {
            return new EBO((int)OGL.GenBuffer());
        }

        public unsafe override void Init()
        {
            OGL.Enable(EnableCap.CullFace);

            vao = CreateVAO();

            OGL.BindVertexArray((uint)vao.Id);

            vbo = CreateVBO();

            OGL.BindBuffer(BufferTargetARB.ArrayBuffer, (uint)vbo.Id);

            ebo = CreateEBO();

            OGL.BindBuffer(BufferTargetARB.ElementArrayBuffer, (uint)ebo.Id);

            OGL.VertexAttribPointer(0, 3, GLEnum.Float, Silk.NET.OpenGL.Boolean.False, 3 * sizeof(float), (void*)0);
            OGL.EnableVertexAttribArray(0);

            LoadedShader = CreateShaderI(InternalShaders.Vertex2DShader, InternalShaders.FragmentShader);
            LoadedShader.Use();
        }

        public override void Clear()
        {
            OGL.ClearColor(ClearingColor);
            OGL.Clear((uint)ClearBufferMask.ColorBufferBit);
        }

        public unsafe override void EndFrame()
        {
            RenderContext.GlfwAPI.SwapBuffers(RenderContext._Handle);
            RenderContext.GlfwAPI.PollEvents();
        }
        #endregion

        public unsafe OGLRenderer(Window wnd)
        {
            RenderContext = wnd as OGLWindow;
            OGL = GL.GetApi(RenderContext._glContext);
        }

        public OGLRenderer()
        {

        }
    }
}
