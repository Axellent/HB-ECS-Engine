using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class TransformComponent : IComponent
    {
        public Matrix world { get; set; }
        public Vector3 position { get; set; }
        public Vector3 scale { get; set; }
        public Quaternion rotation { get; set; }

        public Vector3 vRotation { get; set; }
        public Vector3 forward { get; set; }

        public TransformComponent()
        {
            scale = Vector3.One;
            rotation = Quaternion.Identity;
            position = Vector3.Zero;
        }
    }
}
