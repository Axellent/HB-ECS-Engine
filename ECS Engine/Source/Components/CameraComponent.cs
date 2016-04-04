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
        public float nearClipPlane { get; set; }
        public float farClipPlane { get; set; }
        public float aspectRatio { get; set; }

        public float fieldOfView { get; set; }

        public Vector3 upVector { get; set; }
        public Vector3 position { get; set; }
        public Vector3 direction { get; set; }
        public Vector3 rotation { get; set; }


        public CameraComponent(GraphicsDeviceManager graphics)
        {
            aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
            farClipPlane = 500;
            nearClipPlane = 1;
            fieldOfView = MathHelper.PiOver4;
        }
        
        void test()
        {
            Matrix.create
        }

        public void LookAt(ref CameraPosition)
        {

        }



    }
}
