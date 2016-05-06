using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine {
    public class SkyboxSystem : IRender3DSystem {

        public void Render(GraphicsDevice graphics, GameTime gameTime) {
            Entity skyboxEnt = ComponentManager.Instance.GetFirstEntityOfType<SkyboxComponent>();
            SkyboxComponent skybox = ComponentManager.Instance.GetEntityComponent<SkyboxComponent>(skyboxEnt);
            Entity camEnt = ComponentManager.Instance.GetFirstEntityOfType<CameraComponent>();
            CameraComponent cam = ComponentManager.Instance.GetEntityComponent<CameraComponent>(camEnt);

            // Draw skybox with effect
            foreach (EffectPass pass in skybox.skyboxEffect.CurrentTechnique.Passes) {
                foreach (ModelMesh mesh in skybox.skyboxModel.Meshes) {
                    // Get effect parameters for the mesh
                    foreach (ModelMeshPart part in mesh.MeshParts) {
                        part.Effect = skybox.skyboxEffect;
                        part.Effect.Parameters["World"].SetValue(Matrix.CreateScale(skybox.size) * Matrix.CreateTranslation(cam.position));
                        part.Effect.Parameters["View"].SetValue(cam.viewMatrix);
                        part.Effect.Parameters["Projection"].SetValue(cam.projectionMatrix);
                        part.Effect.Parameters["SkyBoxTexture"].SetValue(skybox.skyboxTexture);
                        part.Effect.Parameters["CameraPosition"].SetValue(cam.position);
                    }

                    // Draw mesh with skybox effect
                    mesh.Draw();
                }
            }
        }
    }
}
