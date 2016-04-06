using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class AnimateSystem : IUpdateSystem
    {
        public void Update(GameTime gameTime)
        {
            List<Entity> entities = ComponentManager.Instance.GetAllEntitiesWithComponentType<AnimationComponent>();

            foreach (Entity enitity in entities)
            {
                AnimationComponent anim = ComponentManager.Instance.GetEntityComponent<AnimationComponent>(enitity);
                anim.CurrentElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

                if (anim.CurrentElapsedTime >= anim.TimePerFrame)
                {
                    anim.CurrentElapsedTime -= anim.TimePerFrame;

                    if (anim.CurrentXFrame >= anim.maxXFrames - 1)
                    {
                        anim.CurrentXFrame = 0;
                        anim.CurrentYFrame++;
                    }
                    else
                    {
                        anim.CurrentXFrame++;
                    }

                    if (anim.CurrentXFrame > anim.GetCurrentAnimation().EndX || anim.CurrentYFrame > anim.GetCurrentAnimation().EndY)
                    {
                        anim.CurrentXFrame = anim.GetCurrentAnimation().StartX;
                        anim.CurrentYFrame = anim.GetCurrentAnimation().StartY;
                    }
                }
            }
        }

    }
}
