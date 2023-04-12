using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarlightEngine.Renderer.OpenGL.Shaders
{
    internal class InternalShaders
    {
        public static readonly string Vertex2DShader = "#version 330 core\r\nlayout (location = 0) in vec3 aPos;\r\n\r\nvoid main()\r\n{\r\n    gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0);\r\n}";
        public static readonly string FragmentShader = "#version 330 core\r\nout vec4 FragColor;\r\n\r\nvoid main()\r\n{\r\n    FragColor = vec4(1.0f, 1.0f, 0.2f, 1.0f);\r\n}";
    }
}
