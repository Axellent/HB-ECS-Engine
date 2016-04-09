using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class HeightmapComponent : IComponent
    {
        List<VertexPositionColor> vertices;
        float[,] heightData;
        public List<short> vertexBuffer { get; set; }

    }
}
