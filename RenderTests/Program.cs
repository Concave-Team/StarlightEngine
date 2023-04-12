using StarlightEngine;
using StarlightEngine.Renderer;
using StarlightEngine.Renderer.OpenGL;
using System.Drawing;

namespace RenderTests
{
    internal class Program
    {
        public static Renderer rend;
        static void Main(string[] args)
        {
            var window = Window.Create(API.OpenGL, "Render Test", new System.Numerics.Vector2(800, 600));

            rend = Renderer.Create(API.OpenGL, window);

            rend.ClearingColor = Color.Red;

            window.Run();

            rend.Init();

            try
            {
                while (!window.WindowShouldClose())
                {
                    rend.Clear();

                    rend.DrawTriangle();

                    rend.EndFrame();
                }
                window.Exit();
            }
            catch(Exception e)
            {
                Console.WriteLine("Lil' exception happened here, oh no!");
                Console.WriteLine(e.Message+" "+e.StackTrace+" "+e.Source);
                window.Exit();
                return;
            }
        }
    }
}