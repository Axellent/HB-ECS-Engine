using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class HeightmapComponent : IComponent
    {
        public GraphicsDevice graphicsDevice;
        public BasicEffect effect;
        public Texture2D heightmapTexture;
        public Matrix viewMatrix;
        public Matrix projectionMatrix;

        public VertexBuffer vertexBuffer;
        public IndexBuffer indexBuffer;

        public int terrainWidth = 4;
        public int terrainHeight = 3;
        public float indicesLenDiv3 = 1;
        public float angle = 0;

        public HeightmapComponent(GraphicsDevice graphicsDevice, BasicEffect effect, Texture2D texture)
        {
            this.graphicsDevice = graphicsDevice;
            this.effect = effect;
            heightmapTexture = texture;
        }

        public void SetMatrices(Matrix viewMatrix, Matrix projectionMatrix)
        {
            this.viewMatrix = viewMatrix;
            this.projectionMatrix = projectionMatrix;
        }

        public void UpdateHeightmap(VertexBuffer vertexBuffer, IndexBuffer indexBuffer, int terrainWidth, int terrainHeight, float indicesLenDiv3)
        {
            this.vertexBuffer = vertexBuffer;
            this.indexBuffer = indexBuffer;
            this.terrainWidth = terrainWidth;
            this.terrainHeight = terrainHeight;
            this.indicesLenDiv3 = indicesLenDiv3;
        }
    }
}
