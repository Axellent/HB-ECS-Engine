using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class ModelComponent : IComponent
    {
        public Model model {get;set;}

        public bool useBasicEffect { get; set; }
        Dictionary<int, Vector3> MeshTransforms { get; set; }

        public ModelComponent(Model model, bool useBasicEffect)
        {
            this.model = model;
            this.useBasicEffect = useBasicEffect;
            MeshTransforms = new Dictionary<int, Vector3>();
        }        
    }
}
