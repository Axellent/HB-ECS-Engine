using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine.Source.Components
{
    class TransformComponent
    {
        Vector3 position { get; set; }
        Vector3 rotation { get; set; }
        Vector3 scale { get; set; }
    }
}
