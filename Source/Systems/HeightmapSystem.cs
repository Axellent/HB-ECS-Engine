using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class HeightmapSystem : IRenderSystem
    {
        public struct VertexPositionColorNormal
        {
            public Vector3 Position;
            public Color Color;
            public Vector3 Normal;

            public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration
            (
                new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                new VertexElement(sizeof(float) * 3, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new VertexElement(sizeof(float) * 3 + 4, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0)
            );
        }

        public void Render(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Entity heightMapEntity = ComponentManager.Instance.GetFirstComponentOfType<HeightmapComponent>();
            HeightmapComponent heightmapComponent = ComponentManager.Instance.GetEntityComponent<HeightmapComponent>(heightMapEntity);

            Matrix worldMatrix = Matrix.CreateTranslation(-heightmapComponent.terrainWidth / 2.0f, 0, heightmapComponent.terrainHeight / 2.0f) * Matrix.CreateRotationY(heightmapComponent.angle);
            heightmapComponent.effect.View = heightmapComponent.viewMatrix;
            heightmapComponent.effect.Projection = heightmapComponent.projectionMatrix;
            heightmapComponent.effect.World = worldMatrix;
            heightmapComponent.effect.VertexColorEnabled = true;

            foreach (EffectPass pass in heightmapComponent.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                heightmapComponent.graphicsDevice.Indices = heightmapComponent.indexBuffer;
                heightmapComponent.graphicsDevice.SetVertexBuffer(heightmapComponent.vertexBuffer);
                heightmapComponent.graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, (int)heightmapComponent.indicesLenDiv3);
            }
        }

        public static float[,] GetHeightData(Texture2D heightMap, ref int terrainWidth, ref int terrainHeight)
        {
            float[,] heightData;
            terrainWidth = heightMap.Width;
            terrainHeight = heightMap.Height;

            Color[] heightMapColors = new Color[terrainWidth * terrainHeight];
            heightMap.GetData(heightMapColors);
            heightData = new float[terrainWidth, terrainHeight];

            for (int x = 0; x < terrainWidth; x++)
            {
                for (int y = 0; y < terrainHeight; y++)
                {
                    heightData[x, y] = heightMapColors[x + y * terrainWidth].R / 4f;
                }
            }
            return heightData;
        }

        public static int[] GetIndices(int terrainWidth, int terrainHeight)
        {
            int[] indices = new int[(terrainWidth - 1) * (terrainHeight - 1) * 6];
            int counter = 0;

            for (int y = 0; y < terrainHeight - 1; y++)
            {
                for (int x = 0; x < terrainWidth - 1; x++)
                {
                    int lowerLeft = x + y * terrainWidth;
                    int lowerRight = (x + 1) + y * terrainWidth;
                    int topLeft = x + (y + 1) * terrainWidth;
                    int topRight = (x + 1) + (y + 1) * terrainWidth;

                    indices[counter++] = topLeft;
                    indices[counter++] = lowerRight;
                    indices[counter++] = lowerLeft;

                    indices[counter++] = topLeft;
                    indices[counter++] = topRight;
                    indices[counter++] = lowerRight;
                }
            }
            return indices;
        }

        public static VertexPositionColorNormal[] GetVertices(int terrainWidth, int terrainHeight, float[,] heightData)
        {
            float minHeight = float.MaxValue;
            float maxHeight = float.MinValue;
            VertexPositionColorNormal[] vertices = new VertexPositionColorNormal[terrainWidth * terrainHeight];

            FindExtremeHeights(ref minHeight, ref maxHeight, terrainWidth, terrainHeight, heightData);

            for (int x = 0; x < terrainWidth; x++)
            {
                for (int y = 0; y < terrainHeight; y++)
                {
                    vertices[x + y * terrainWidth].Position = new Vector3(x, heightData[x, y], -y);

                    if (heightData[x, y] < minHeight + (maxHeight - minHeight) / 4)
                        vertices[x + y * terrainWidth].Color = Color.SandyBrown;
                    else if (heightData[x, y] < minHeight + (maxHeight - minHeight) * 2 / 4)
                        vertices[x + y * terrainWidth].Color = Color.BurlyWood;
                    else if (heightData[x, y] < minHeight + (maxHeight - minHeight) * 3 / 4)
                        vertices[x + y * terrainWidth].Color = Color.SandyBrown;
                    else
                        vertices[x + y * terrainWidth].Color = Color.Chocolate;
                }
            }
            return vertices;
        }

        public static void TransformToNormals(ref VertexPositionColorNormal[] vertices, int[] indices)
        {
            for (int i = 0; i < vertices.Length; i++)
                vertices[i].Normal = new Vector3(0, 0, 0);

            for (int i = 0; i < indices.Length / 3; i++)
            {
                int index1 = indices[i * 3];
                int index2 = indices[i * 3 + 1];
                int index3 = indices[i * 3 + 2];

                Vector3 side1 = vertices[index1].Position - vertices[index3].Position;
                Vector3 side2 = vertices[index1].Position - vertices[index2].Position;
                Vector3 normal = Vector3.Cross(side1, side2);

                vertices[index1].Normal += normal;
                vertices[index2].Normal += normal;
                vertices[index3].Normal += normal;
            }

            for (int i = 0; i < vertices.Length; i++)
                vertices[i].Normal.Normalize();
        }

        public static void CopyToBuffers(ref VertexBuffer vertexBuffer, ref IndexBuffer indexBuffer, VertexPositionColorNormal[] vertices, int[] indices, GraphicsDevice graphicsDevice)
        {
            vertexBuffer = new VertexBuffer(graphicsDevice, VertexPositionColorNormal.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);

            indexBuffer = new IndexBuffer(graphicsDevice, typeof(int), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }

        private static void FindExtremeHeights(ref float minHeight, ref float maxHeight, int terrainWidth, int terrainHeight, float[,] heightData)
        {
            for (int x = 0; x < terrainWidth; x++)
            {
                for (int y = 0; y < terrainHeight; y++)
                {
                    if (heightData[x, y] < minHeight)
                        minHeight = heightData[x, y];
                    if (heightData[x, y] > maxHeight)
                        maxHeight = heightData[x, y];
                }
            }
        }
    }
}
