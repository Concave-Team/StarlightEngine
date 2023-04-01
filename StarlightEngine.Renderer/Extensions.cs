using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace StarlightEngine.Renderer
{
    public static class Extensions
    {
        public static Silk.NET.Maths.Vector2D<int> ToSilkV2(this Vector2 vec)
        {
            return new Silk.NET.Maths.Vector2D<int>((int)vec.X, (int)vec.Y);
        }
    }
}
