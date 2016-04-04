using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine.Source.Engine
{
    class ModelFactory
    {
        private static ModelFactory instance;
        public static ModelFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new ModelFactory();
                return instance;
            }
        }

        private ModelFactory() { }

    }
}
