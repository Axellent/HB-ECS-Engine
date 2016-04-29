using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine 
{
    public class TerrainMapRenderSystem : IRender3DSystem
    {
        DebugRenderBoundingBox boxRenderer;
        BoundingBoxToWorldSpace boxConvert;
        bool renderBoxInitialised = false;

        public void Render(GraphicsDevice graphicsDevice, GameTime gameTime)
        {
            if (renderBoxInitialised.Equals(false))
            {
                boxConvert = new BoundingBoxToWorldSpace();
                boxRenderer = new DebugRenderBoundingBox(graphicsDevice);
                renderBoxInitialised = true;
            }

            Entity e = ComponentManager.Instance.GetFirstEntityOfType<TerrainMapComponent>();
            Entity c = ComponentManager.Instance.GetFirstEntityOfType<CameraComponent>();
            CameraComponent camera = ComponentManager.Instance.GetEntityComponent<CameraComponent>(c);
            TerrainMapComponent terrainComponent = ComponentManager.Instance.GetEntityComponent<TerrainMapComponent>(e);
            TransformComponent transformComponent = ComponentManager.Instance.GetEntityComponent<TransformComponent>(e);

            if (terrainComponent != null)
            {
                if (transformComponent != null)
                {
                    /*RasterizerState r = new RasterizerState();
                    r.CullMode = CullMode.None;
                    //r.FillMode = FillMode.WireFrame;
                    graphicsDevice.RasterizerState = r;//*/

                    terrainComponent.numChunksInView = 0;

                    for (int i = 0; i < terrainComponent.terrainChunks.Count; ++i)
                    {
                        terrainComponent.terrainChunks[i].effect.TextureEnabled = true;
                        terrainComponent.terrainChunks[i].effect.VertexColorEnabled = false;
                        terrainComponent.terrainChunks[i].effect.Texture = terrainComponent.terrainChunks[i].terrainTex;
                        terrainComponent.terrainChunks[i].effect.Projection = camera.projectionMatrix;
                        terrainComponent.terrainChunks[i].effect.View = camera.viewMatrix;
                        terrainComponent.terrainChunks[i].effect.World = transformComponent.world * Matrix.CreateTranslation(terrainComponent.terrainChunks[i].offsetPosition);
                        terrainComponent.terrainChunks[i].effect.EnableDefaultLighting();

                        BoundingBox box = boxConvert.ConvertBoundingBoxToWorldCoords(terrainComponent.terrainChunks[i].boundingBox, terrainComponent.terrainChunks[i].effect.World);

                        if (box.Intersects(camera.cameraFrustrum))
                        {
                            foreach (EffectPass p in terrainComponent.terrainChunks[i].effect.CurrentTechnique.Passes)
                            {
                                p.Apply();
                                graphicsDevice.Indices = terrainComponent.terrainChunks[i].iBuffer;
                                graphicsDevice.SetVertexBuffer(terrainComponent.terrainChunks[i].vBuffer);
                                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, terrainComponent.terrainChunks[0].indicesLenDiv3);
                            }
                            boxRenderer.RenderBoundingBox(terrainComponent.terrainChunks[i].boundingBox, terrainComponent.terrainChunks[i].effect.World, camera.viewMatrix, camera.projectionMatrix);
                            terrainComponent.numChunksInView++;
                        }
                    }
                }
            }
        }
    }
}

