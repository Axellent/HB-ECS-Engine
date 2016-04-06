using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame;

namespace ECS_Engine.Source.Components
{
    class ModelComponent
    {
        Model model;

        void SetModel(Model model)
        {
            this.model = model;
        }
    }
}
