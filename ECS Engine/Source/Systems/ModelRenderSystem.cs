using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class ModelRenderSystem : IRenderSystem
    {
        public void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }

        private void RenderBasicEffectModel(ModelComponent modelComp)
        {
            Matrix[] transforms = new Matrix[modelComp.model.Bones.Count];
            modelComp.model.CopyAbsoluteBoneTransformsTo(transforms);


        }
    }
}
