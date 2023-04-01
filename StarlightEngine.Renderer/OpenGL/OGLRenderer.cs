using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlightEngine.Renderer.OpenGL
{
    public class OGLRenderer : Renderer
    {
        public Color ClearingColor = Color.AliceBlue;
        public OGLWindow RenderContext;
        public GL OGL;

        #region CollapseForReadability
        public override void DrawCube()
        {
            throw new NotImplementedException();
        }

        public override void DrawPlane()
        {
            throw new NotImplementedException();
        }

        public override void DrawTriangle()
        {
            throw new NotImplementedException();
        }

        public override void DrawPoint()
        {
            throw new NotImplementedException();
        }

        public override void DrawEntity()
        {
            throw new NotImplementedException();
        }

        public override void Init()
        {
            this.OGL = this.RenderContext._Handle.CreateOpenGL();

            OGL.Clear(ClearBufferMask.ColorBufferBit);
        }

        public override void Clear()
        {
            OGL.ClearColor(ClearingColor);
            OGL.Clear((uint)ClearBufferMask.ColorBufferBit);
        }

        public override void EndFrame()
        {
            RenderContext._Handle.SwapBuffers();
        }
        #endregion

        public OGLRenderer(Window wnd)
        {
            RenderContext = wnd as OGLWindow;
        }

        public OGLRenderer()
        {

        }
    }
}
