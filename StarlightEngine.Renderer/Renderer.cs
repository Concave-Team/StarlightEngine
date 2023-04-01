using StarlightEngine.Renderer.OpenGL;

namespace StarlightEngine.Renderer
{
    public abstract class Renderer
    {
        public abstract void DrawCube();
        public abstract void DrawPlane();
        public abstract void DrawPoint();
        public abstract void DrawTriangle();

        public abstract void DrawEntity();

        public abstract void Clear();

        public abstract void Init();

        public abstract void EndFrame();

        public static Renderer Create(API api, Window Context)
        {
            switch (api)
            {
                case API.OpenGL:
                    return new OGLRenderer(Context);
                default:
                    throw new Exception("Invalid/Unknown API passed when creating renderer.");
            }
        }
    }
}