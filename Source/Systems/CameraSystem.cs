using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using Microsoft.Xna.Framework;

namespace GameEngine.Source.Systems
{

    class CameraSystem : IUpdateSystem
    {
        private static int CAMERAMODE_STATIC = 0;
        private static int CAMERAMODE_CHASE = 1;

        public void Update(GameTime gameTime)
        {
            List<Entity> entities = ComponentManager.Instance.GetAllEntitiesWithComponentType<CameraComponent>();
            foreach (Entity entity in entities)
            {
                CameraComponent c = ComponentManager.Instance.GetEntityComponent<CameraComponent>(entity);
                Entity targetEntity = c.targetEntity;

                if (c.cameraMode == CAMERAMODE_CHASE)
                {
                    Vector3 pos = new Vector3(0, 0.1f, 0.62f);
                    /*Transform mc = ComponentManager.Instance.GetEntityComponent<ModelComponent>(targetEntity);

                    pos = Vector3.Transform(pos, Matrix.CreateFromQuaternion(objRotation));
                    pos += position;

                    Vector3 up = new Vector3(0, 1, 0);
                    viewMatrix = Matrix.CreateLookAt(pos, position, up);
                    projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, nearClipPlane, farClipPlane);*/
                }
            }
        }

        public void SetTargetEntity(string EntityTag)
        {
            List<Entity> entities = ComponentManager.Instance.GetAllEntitiesWithComponentType<ModelComponent>();

            Entity camEnt = ComponentManager.Instance.GetFirstComponentOfType<CameraComponent>();
            CameraComponent c = ComponentManager.Instance.GetEntityComponent<CameraComponent>(camEnt);

            foreach (Entity e in entities)
            {
                TagComponent t = ComponentManager.Instance.GetEntityComponent<TagComponent>(e);
                if(t.tagName.Equals(EntityTag))
                {
                    c.targetEntity = e;
                }
            }
        }

        public void SetNearClipPlane(float value)
        {
            List<Entity> entities = ComponentManager.Instance.GetAllEntitiesWithComponentType<CameraComponent>();
            foreach (Entity enitity in entities)
            {
                CameraComponent c = ComponentManager.Instance.GetEntityComponent<CameraComponent>(enitity);

                if (value <= 0)
                {
                    c.nearClipPlane = 1f;
                }

                c.nearClipPlane = value;
            }
        }

        public void SetFarClipPlane(float value)
        {
            List<Entity> entities = ComponentManager.Instance.GetAllEntitiesWithComponentType<CameraComponent>();
            foreach (Entity enitity in entities)
            {
                CameraComponent c = ComponentManager.Instance.GetEntityComponent<CameraComponent>(enitity);

                if (value >= 10000)
                {
                    c.farClipPlane = 10000;
                }
                else if (value < 50)
                {
                    c.farClipPlane = 50;
                }
                else
                {
                    c.farClipPlane = value;
                }
            }
        }
    }
}
