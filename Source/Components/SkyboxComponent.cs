using Microsoft.Xna.Framework.Graphics;

namespace GameEngine {
    public class SkyboxComponent : IComponent{
        public Model skyboxModel;
        public TextureCube skyboxTexture;
        public Effect skyboxEffect;
        public float size = 1000f;

        public SkyboxComponent(Model skyboxModel, TextureCube skyboxTexture, Effect skyboxEffect) {
            this.skyboxModel = skyboxModel;
            this.skyboxTexture = skyboxTexture;
            this.skyboxEffect = skyboxEffect;
        }

        public SkyboxComponent(Model skyboxModel, TextureCube skyboxTexture, Effect skyboxEffect, float size)
            : this(skyboxModel, skyboxTexture, skyboxEffect){
            this.size = size;
        }
    }
}
