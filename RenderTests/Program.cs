using StarlightEngine;
using StarlightEngine.Renderer;

namespace RenderTests
{
    internal class Program
    {
        public static Renderer rend;
        static void Main(string[] args)
        {
            var window = Window.Create(API.OpenGL, "Render Test", new System.Numerics.Vector2(800, 600));

            rend = Renderer.Create(API.OpenGL, window);

            window.r_OnLoad = Load;
            window.r_OnRender = Render;


            window.Run();
        }

        static void Load()
        {
            rend.Init();
        }

        static void Render(double delta)
        {
            Console.WriteLine("F");
            rend.Clear();

            rend.EndFrame();
        }
    }
}