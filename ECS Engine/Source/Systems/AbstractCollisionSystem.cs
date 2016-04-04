using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    public abstract class AbstractCollisionSystem : IUpdateSystem
    {
        public Type collider { get; set; }
        public Type collideAgainst { get; set; }
        public bool PhysicsOn { get; set; }

        public AbstractCollisionSystem()
        {
            PhysicsOn = true;
        }

        /// <summary>
        /// Checks if a collision has occured between an entity and entities in a list
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="collideEntities"></param>
        /// <returns>A struct of CollidedObjects containing the tags of the entities that collided.</returns>
        /// <returns>Null if no collision occured</returns>
        private bool RectangleCollision(Entity entity, List<Entity> collideEntities, out Entity collider, out Entity collidedWith)
        {
            collider = entity;
            collidedWith = null;
            if (PhysicsOn)
            {
                RectangleCollisionComponent eRect = ComponentManager.Instance.GetEntityComponent<RectangleCollisionComponent>(entity);
                Velocity2DComponent eVel = ComponentManager.Instance.GetEntityComponent<Velocity2DComponent>(entity);
                //if the entity didn't have a rectangle collision component
                if (eRect == null)
                {
                    return false;
                }


                foreach (Entity e in collideEntities)
                {
                    RectangleCollisionComponent eTarget = ComponentManager.Instance.GetEntityComponent<RectangleCollisionComponent>(e);
                    Velocity2DComponent eTargetVel = ComponentManager.Instance.GetEntityComponent<Velocity2DComponent>(e);

                    //if both are non-movable objects, contine to the next entity
                    if (eVel == null && eTargetVel == null)
                    {
                        continue;
                    }

                    if (eTarget != null)
                    {
                        if (eRect.CollisionRect.Intersects(eTarget.CollisionRect) && eRect != eTarget)
                        {
                            collidedWith = e;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if a collision has occured between an entity and entities in a list
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="collideEntities"></param>
        /// <returns>True if a collision occured, false if if it didn't</returns>
        private bool CircleCollision(Entity entity, List<Entity> collideEntities,out Entity collider, out Entity collidedWith)
        {
            collider = entity;
            collidedWith = null;
            if (PhysicsOn)
            {
                CircleCollisionComponent eCircle = ComponentManager.Instance.GetEntityComponent<CircleCollisionComponent>(entity);
                Position2DComponent ePos = ComponentManager.Instance.GetEntityComponent<Position2DComponent>(entity);
                Velocity2DComponent eVel = ComponentManager.Instance.GetEntityComponent<Velocity2DComponent>(entity);
                
                //if the entity doesn't have a position2D or cirlce collision component
                if (eCircle == null || ePos == null )
                {
                    return false;
                }

                foreach (Entity e in collideEntities)
                {
                    CircleCollisionComponent eTarget = ComponentManager.Instance.GetEntityComponent<CircleCollisionComponent>(e);
                    Position2DComponent eTargetPos = ComponentManager.Instance.GetEntityComponent<Position2DComponent>(e);
                    Velocity2DComponent eTargetVel = ComponentManager.Instance.GetEntityComponent<Velocity2DComponent>(e);
                    
                    //if both are non-movable objects, contine to the next entity
                    if (eVel == null && eTargetVel == null)
                    {
                        continue;
                    }

                    if (eTarget != null && eTargetPos != null )
                    {
                        double distSquare = Math.Pow(ePos.Position.X - eTargetPos.Position.X, 2) + Math.Pow(ePos.Position.Y - eTargetPos.Position.Y, 2);
                        double totalCollisionRadius = Math.Pow(eCircle.Radius + eTarget.Radius, 2);
                        if (distSquare - totalCollisionRadius < 0 && eCircle != eTarget)
                        {
                            collidedWith = e;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// The game developer can override this method to choose what happens when a collision occurs
        /// between two entities by checking the types of the different colliders.
        /// </summary>
        /// <param name="collider">the entity</param>
        /// <param name="collidedWith">the entity which was collided with</param>
        public abstract void OnCollision(Entity collider, Entity collidedWith);

        /// <summary>
        /// This method checks all collisions between entities in the current active scene
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            Entity collider = null;
            Entity collidedWith = null;

            //Retrive the current active scene
            Scene activeScene = SceneManager.Instance.GetActiveScene();

            //get all entities from the scene
            List<Entity> sceneEntities = activeScene.GetAllEntities();

            //return if there is only one entity in the scene
            if (sceneEntities.Count <= 1)
                return;

            //loop through all entities in the scene
            foreach (Entity e in sceneEntities)
            {
                if(RectangleCollision(e,sceneEntities,out collider,out collidedWith))
                {
                    OnCollision(collider, collidedWith);
                }
                if (CircleCollision(e, sceneEntities, out collider, out collidedWith))
                {
                    OnCollision(collider, collidedWith);
                }
            }
        }

    }
}
