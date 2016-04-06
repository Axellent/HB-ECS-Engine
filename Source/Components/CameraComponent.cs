using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Engine.Source.Components
{
    class CameraComponent
    {
        public float nearClipPlane = 1f;
        public float farClipPlane = 1000f;
        public float aspectRatio { get; set; }

        public float fieldOfView { get; set; }

        public Vector3 position { get; set; }
        public Vector3 direction { get; set; }
        public Vector3 rotation { get; set; }

        public Matrix viewMatrix;
        public Matrix projectionMatrix;
        
        public CameraComponent(GraphicsDeviceManager graphics, Vector3 position, Vector3 direction, Vector3 movement)
        {
            aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
            fieldOfView = MathHelper.PiOver4;
            viewMatrix = Matrix.CreateLookAt(position, direction, Vector3.Up);
            Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane, out projectionMatrix);
        }

        public void LookAt(ref Vector3 CameraPosition)
        {

        }

        public void SetNearClipPlane(float value)
        {
            if(value<=0) {
                this.nearClipPlane = 1f;
            }

            this.nearClipPlane = value;
        }

        public void SetFarClipPlane(float value)
        {
            if (value >= 10000)
            {
                this.farClipPlane = 10000;
            }
            else if (value < 50)
            {
                this.farClipPlane = 50;
            }
            else
            {
                this.farClipPlane = value;
            }
        }

    }
}
