using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class TransformComponent : IComponent
    {
        public Matrix world { get; set; }
        public Vector3 position { get; set; }
        public Vector3 scale { get; set; }
        public Quaternion rotation { get; set; }
        public TransformComponent()
        {
            scale = new Vector3(1.0f, 1.0f, 1.0f);
            rotation = Quaternion.Identity;
            position = new Vector3(0f, 0f, 0f);
        }
    }
}
