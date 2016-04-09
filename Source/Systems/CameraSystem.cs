using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class CameraSystem : IUpdateSystem
    {
        private static int CAMERAMODE_STATIC = 0;
        private static int CAMERAMODE_CHASE = 1;

        private Vector3 origo = new Vector3(0f, 0f, 0f);
        private Vector3 staticCameraPos = new Vector3(30.0f, 30.0f, 30.0f);

        public void Update(GameTime gameTime)
        {
            //get the camera entity
            Entity camera = ComponentManager.Instance.GetFirstComponentOfType<CameraComponent>();

            //get the camera component
            CameraComponent c = ComponentManager.Instance.GetEntityComponent<CameraComponent>(camera);

            //get the target entity (used in chase camera mode)
            Entity targetEntity = c.targetEntity;

            //a static camera that looks at origo
            if(c.cameraMode == CAMERAMODE_STATIC)
            {
                
            }

            if (c.cameraMode == CAMERAMODE_CHASE && targetEntity!=null)
            {
                //set the camera behind the target object
                Vector3 pos = new Vector3(0, 0.1f, 0.62f);

                //get transform component from the entity the camera i following
                TransformComponent t = ComponentManager.Instance.GetEntityComponent<TransformComponent>(targetEntity);

                //get the rotation
                pos = Vector3.Transform(pos, Matrix.CreateFromQuaternion(t.rotation));

                //move the camera to the object position
                pos += t.position;

                //update the view
                c.viewMatrix = Matrix.CreateLookAt(pos, t.position, c.upDirection);

                //update the projection
                c.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, c.aspectRatio, c.nearClipPlane, c.farClipPlane);
            }
        }

        public void SetCameraToStatic()
        {
            //get the camera entity
            Entity camera = ComponentManager.Instance.GetFirstComponentOfType<CameraComponent>();

            //get the camera component
            CameraComponent c = ComponentManager.Instance.GetEntityComponent<CameraComponent>(camera);

            c.position = staticCameraPos;
            c.target = origo;
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

        public void SetCameraLookAt(Vector3 target)
        {
            //get the camera entity
            Entity camera = ComponentManager.Instance.GetFirstComponentOfType<CameraComponent>();

            //get the camera component
            CameraComponent c = ComponentManager.Instance.GetEntityComponent<CameraComponent>(camera);

            //set the camera to look at the target
            c.target = target;
        }

        public void SetCameraPosition(Vector3 cameraPosition)
        {
            //get the camera entity
            Entity camera = ComponentManager.Instance.GetFirstComponentOfType<CameraComponent>();

            //get the camera component
            CameraComponent c = ComponentManager.Instance.GetEntityComponent<CameraComponent>(camera);

            //set the camera position
            c.position = cameraPosition;
        }

        public void SetCameraFieldOfView(int degrees)
        {
            //get the camera entity
            Entity camera = ComponentManager.Instance.GetFirstComponentOfType<CameraComponent>();

            //get the camera component
            CameraComponent c = ComponentManager.Instance.GetEntityComponent<CameraComponent>(camera);

            //set field of view
            c.fieldOfView = MathHelper.ToRadians(degrees);
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
