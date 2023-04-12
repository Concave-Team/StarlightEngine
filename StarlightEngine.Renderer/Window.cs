using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace StarlightEngine.Renderer
{
    public enum API
    {
        OpenGL,
        Vulkan, // Unimplemented.
        D3D11 // Unimplemented.
    }

    // Platform-agnostic window base class.
    public abstract class Window : RenderContext
    {
        public Action<double> r_OnUpdate { get; set; }
        public Action<double> r_OnRender { get; set; }
        public Action r_OnLoad { get; set; }
        public abstract void Run();

        public abstract void ModifyName(string name);

        public abstract void ModifySize(Vector2 size);

        public abstract void Exit();

        public abstract bool WindowShouldClose();

        public static Window Create(API usedAPI, string name, Vector2 size)
        {
            switch(usedAPI)
            {
                case API.OpenGL:
                    return new OpenGL.OGLWindow(name, size);
                default:
                    throw new Exception("Unknown/Invalid API passed to Window creation.");
            }
        }
    }
}
