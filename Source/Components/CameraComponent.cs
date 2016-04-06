using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class CameraComponent : IComponent
    {
        public float nearClipPlane = 1f;
        public float farClipPlane = 500f;
        public float aspectRatio { get; set; }

        public float fieldOfView { get; set; }

        public Vector3 upDirection { get; set; }
        public Vector3 position { get; set; }

        public Entity targetEntity { get; set; }    //target entity to chase / look at

        public Matrix viewMatrix;
        public Matrix projectionMatrix;

        public int cameraMode { get; set; }
        
        public CameraComponent(GraphicsDeviceManager graphics, Vector3 position, Vector3 direction, Vector3 movement)
        {
            aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
            fieldOfView = MathHelper.PiOver4;
            upDirection = Vector3.Up;
            viewMatrix = Matrix.CreateLookAt(position, direction, upDirection);
            Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane, out projectionMatrix);
        }
    }
}
