using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class TransformComponent
    {
        public Matrix rotation {get;set;}
        public Matrix position { get; set; }
        public Matrix scale { get; set; }
    }
}
