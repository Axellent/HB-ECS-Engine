using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    /// <summary>
    /// Factory to create new entities
    /// </summary>
    public class EntityFactory
    {
        private static EntityFactory instance;
        public static EntityFactory Instance
        {
            get 
            {
                if (instance == null)
                    instance = new EntityFactory();
                return instance;
            }
        }

        private EntityFactory() { }

        /// <summary>
        /// Creates new entity (Do not use for entities that need collision handling)
        /// </summary>
        /// <returns> A new entity</returns>
        public Entity NewEntity() 
        {
            Entity newEnt = new Entity();;
            newEnt.Visible = true;
            return newEnt;
        }

        /// <summary>
        /// Creates a new entity with a tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public Entity NewEntityWithTag(string tagName)
        {
            Entity newEnt = new Entity();
            newEnt.Visible = true;
            ComponentManager.Instance.AddComponentToEntity(newEnt, new TagComponent(tagName));
            return newEnt;
        }
        /// <summary>
        /// Creates a new non-movable game entity with a tag(chest,rock etc)
        /// Includes: a render, tag, rectangle collision and position2D component
        /// Visible: yes
        /// Updateable: no
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public Entity NewGameEntity(string tagName,Texture2D texture)
        {
            Render2DComponent renderComponent = new Render2DComponent(texture);
            RectangleCollisionComponent collisionRect = new RectangleCollisionComponent(renderComponent.DestRect);
            Entity newEnt = new Entity();
            newEnt.Visible = true;
            ComponentManager.Instance.AddComponentToEntity(newEnt, renderComponent);
            ComponentManager.Instance.AddComponentToEntity(newEnt, new TagComponent(tagName));
            ComponentManager.Instance.AddComponentToEntity(newEnt,new Position2DComponent(new Vector2(0,0)));
            ComponentManager.Instance.AddComponentToEntity(newEnt,collisionRect);
            return newEnt;
        }
        /// <summary>
        /// Creates a new game entity with a tag(enemy, player, spear, car, box)
        /// Includes: Tag, Position2D, rectangleCollision and velocity component
        /// Visible: yes
        /// Updateable: yes
        /// </summary>
        /// <param name="tagName">Tag for the entity</param>
        /// <returns></returns>
        public Entity NewMoveableGameEntity(string tagName,Texture2D texture, float speed, int x,int y)
        {
            Render2DComponent renderComponent = new Render2DComponent(texture);
            RectangleCollisionComponent collisionRect = new RectangleCollisionComponent(new Rectangle(x,y,renderComponent.Width,renderComponent.Height));
            Entity newEnt = new Entity();
            newEnt.Visible = true;
            newEnt.Updateable = true;
            ComponentManager.Instance.AddComponentToEntity(newEnt, renderComponent);
            ComponentManager.Instance.AddComponentToEntity(newEnt, new TagComponent(tagName));
            ComponentManager.Instance.AddComponentToEntity(newEnt, new Position2DComponent(new Vector2(x, y)));
            ComponentManager.Instance.AddComponentToEntity(newEnt, new Velocity2DComponent(new Vector2(0, 0), speed));
            ComponentManager.Instance.AddComponentToEntity(newEnt, collisionRect);
            return newEnt;
        }
    }
}
