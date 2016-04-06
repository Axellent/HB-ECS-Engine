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
    class ModelComponent : IComponent
    {
        public Model model {get;set;}
        public Matrix scale { get; set; }
        public Matrix position { get; set; }
        public Quaternion rotation { get; set; }

        void SetModel(Model model)
        {
            this.model = model;
        }
        /// <summary>
        /// This function rotates the given bone by the given matrix
        /// </summary>
        /// <param name="boneIndex"></param>
        /// <param name="t"></param>
        public void ChangeBoneTransform(int boneIndex, Matrix t)
        {
            model.Bones[boneIndex].Transform = t * model.Bones[boneIndex].Transform;
        }
    }
}
