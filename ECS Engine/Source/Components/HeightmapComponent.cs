using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine.Source.Components
{
    class HeightmapComponent
    {
        List<VertexPositionColor> vertices;
        Vector2[][] heightMap; 
        public List<short> vertexBuffer { get; set; }

    }
}
