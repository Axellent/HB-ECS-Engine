using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class SpriteRenderSystem : IRenderSystem
    {
        /// <summary>
        /// Renders a sprite on the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        public void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // TODO: Make sure that sourceRectangle is valid, when the animation system is implemented
            // TODO: Act with scene manager to extract enties that belong to the active scene

            List<List<Entity>> sceneEntities = SceneManager.Instance.GetActiveScene().GetAllLayers();

            if (sceneEntities == null)
            {
                //ErrorLogger.Instance.LogErrorToDisk("No entities to render in the active scene!", "SpriteRenderSystemLog.txt");
                return;
            }

            for (int i = 0; i < sceneEntities.Count; ++i)
            {
                foreach (Entity entity in sceneEntities[i])
                {
                    if (entity.Visible)
                    {
                        Render2DComponent r = ComponentManager.Instance.GetEntityComponent<Render2DComponent>(entity);
                        if (r != null)
                        {
                            Position2DComponent p = ComponentManager.Instance.GetEntityComponent<Position2DComponent>(entity);
                            if (p != null)
                            {
                                AnimationComponent a = ComponentManager.Instance.GetEntityComponent<AnimationComponent>(entity);
                                if (a != null)
                                {
                                    r.DestRect = new Rectangle((int)p.Position.X, (int)p.Position.Y, 32 * 4, 32 * 4);
                                    r.SourceRect = a.GetSourceRect();
                                    spriteBatch.Draw(r.Texture, r.DestRect, r.SourceRect, Color.White);
                                }
                                else
                                {
                                    r.DestRect = new Rectangle((int)p.Position.X, (int)p.Position.Y, r.Width, r.Height);
                                    //TODO: gör en generell sourcerect för animationer senare. Hårdkodat just nu för test.
                                    r.SourceRect = new Rectangle(0, 0, r.Width, r.Height);
                                    spriteBatch.Draw(r.Texture, r.DestRect, r.SourceRect, Color.White);
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
