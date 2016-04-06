using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class HeightmapComponent : IComponent
    {
        List<VertexPositionColor> vertices;
        Vector3[][] heightMap; 
        public List<short> vertexBuffer { get; set; }

    }
}
