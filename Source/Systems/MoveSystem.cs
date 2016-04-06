using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class MoveSystem : IUpdateSystem
    {
        /// <summary>
        /// Updates an entity's position
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            List<List<Entity>> sceneEntities = SceneManager.Instance.GetActiveScene().GetAllLayers();
            if (sceneEntities == null)
            {
                //ErrorLogger.Instance.LogErrorToDisk("MoveSystem - no entities in scene to update", "MoveSystemLog.Txt");
                return;
            }

            for (int i = 0; i < sceneEntities.Count; ++i)
            {
                foreach(Entity e in sceneEntities[i])
                {
                    if (e.Updateable)
                    {
                        Velocity2DComponent v = ComponentManager.Instance.GetEntityComponent<Velocity2DComponent>(e);
                        Position2DComponent p = ComponentManager.Instance.GetEntityComponent<Position2DComponent>(e);
                        RectangleCollisionComponent r = ComponentManager.Instance.GetEntityComponent<RectangleCollisionComponent>(e);
                        if (r != null)
                        {
                            int w = r.CollisionRect.Width;
                            int h = r.CollisionRect.Height;
                            r.CollisionRect = new Rectangle((int)p.Position.X, (int)p.Position.Y, w, h);
                        }

                        if (p != null && v != null)
                        {
                            p.Position += v.Velocity * v.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                    }
                }
            }
        }
    }
}
