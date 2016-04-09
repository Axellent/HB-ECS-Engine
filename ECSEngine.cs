using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public abstract class ECSEngine
    {
        private Game game = null;
        private GraphicsDeviceManager gdm = null;

        public abstract void Initialise();
        public abstract void InitialiseContent();

        public T LoadContent<T>(string name)
        {
            return game.Content.Load<T>(name);
        }

        public GraphicsDevice GetGraphicsDevice()
        {
            return game.GraphicsDevice;
        }

        public GraphicsDeviceManager GetGraphicsDeviceManager()
        {
            return gdm;
        }

    }
}
