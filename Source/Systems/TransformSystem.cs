using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class TransformSystem : IUpdateSystem
    {
        /// <summary>
        /// This function rotates the given bone by the given matrix
        /// </summary>
        /// <param name="boneIndex"></param>
        /// <param name="t"></param>
        /* public void ChangeBoneTransform(ModelComponent m, int boneIndex, Matrix t)
         {
             //model.Bones[boneIndex].Transform = t * model.Bones[boneIndex].Transform;
         }*/
        public void Update(GameTime gameTime)
        {
            List<Entity> entities = SceneManager.Instance.GetActiveScene().GetAllEntities();

            foreach(Entity e in entities)
            {
                TransformComponent t = ComponentManager.Instance.GetEntityComponent<TransformComponent>(e);

                //Update world matrix
                t.world = Matrix.CreateFromQuaternion(t.rotation)
                                   * Matrix.CreateTranslation(t.position)
                                   * Matrix.CreateScale(t.scale);
            }
        }
    }
}
