﻿using System;
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
        private Vector3 staticCameraPos = new Vector3(30.0f, 30.0f, -100f);
        
        public void Update(GameTime gameTime)
        {
            //get the camera entity
            Entity camera = ComponentManager.Instance.GetFirstEntityOfType<CameraComponent>();

            //get the camera component
            CameraComponent c = ComponentManager.Instance.GetEntityComponent<CameraComponent>(camera);

            //a static camera that looks at origo
            if(c.cameraMode == CAMERAMODE_STATIC)
            {
                c.viewMatrix = Matrix.CreateLookAt(c.position, c.target, c.upDirection);
            }

            System.Console.WriteLine("X:" + c.position.X + "Y:" + c.position.Y + "Z:" + c.position.Z);

            if (c.targetEntity!=null)
            {
                List<Entity> elist = ComponentManager.Instance.GetAllEntitiesWithComponentType<ModelComponent>();
                Entity e = ComponentManager.Instance.GetEntityWithTag(c.targetEntity,elist);

                //set the camera behind the target object
                Vector3 pos = c.camChasePosition;

                //get transform component from the entity the camera i following
                TransformComponent t = ComponentManager.Instance.GetEntityComponent<TransformComponent>(e);

                //get the rotation
                pos = Vector3.Transform(pos, Matrix.CreateFromQuaternion(t.rotation));

                //move the camera to the object position
                pos += t.position;

                //update the view
                c.viewMatrix = Matrix.CreateLookAt(pos, t.position, c.upDirection);

                //update the projection
               // c.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, c.aspectRatio, c.nearClipPlane, c.farClipPlane);
            }
        }
    }
}
