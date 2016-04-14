using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class TerrainComponent : IComponent
    {
        public VertexBuffer vBuffer { get; set; }
        public IndexBuffer iBuffer { get; set; }
        public BasicEffect effect { get; set; }

        public int indicesLenDiv3;
        public int width { get; set; }
        public int height { get; set; }

        public float[,] heightInfo;

        Texture2D terrainMap { get; set; }

        public VertexPositionColorNormal[] vertices { get; set; }
        public int[] indices { get; set; }

        public TerrainComponent(GraphicsDevice graphicsDevice, Texture2D terrainMap)
        {
            effect = new BasicEffect(graphicsDevice);
            LoadHighmap(terrainMap);

            effect.FogEnabled = true;
            effect.FogStart = 10f;
            effect.FogColor = Color.LightGray.ToVector3();
            effect.FogEnd = 400f;
            InitVertices();
            InitIndices();
            InitNormals();
            PrepareBuffers(graphicsDevice);
            effect.VertexColorEnabled = true;
        }

        private void LoadHighmap(Texture2D terrainMap)
        {
            this.terrainMap = terrainMap;

            width = terrainMap.Width;
            height = terrainMap.Height;

            //get the pixels from the terrain map
            Color[] colors = new Color[width * height];
            terrainMap.GetData(colors);

            heightInfo = new float[width, height];
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    heightInfo[x, y] = colors[x + y * width].R / 4f;
                }
            }
        }

        private void InitIndices()
        {
            indices = new int[(width - 1) * (height - 1) * 6];
            int indicesCount = 0; ;

            for (int y = 0; y < height-1;++y)
            {
                for(int x=0;x<width -1; ++x)
                {
                    int botLeft = x + y * width;
                    int botRight = (x + 1) + y * width;
                    int topLeft = x + (y + 1) * width;
                    int topRight = (x + 1) + (y + 1) * width;

                    indices[indicesCount++] = topLeft;
                    indices[indicesCount++] = botRight;
                    indices[indicesCount++] = botLeft;

                    indices[indicesCount++] = topLeft;
                    indices[indicesCount++] = topRight;
                    indices[indicesCount++] = botRight;
                }
            }

            indicesLenDiv3 = indices.Length / 3;
        }
        private void InitNormals()
        {
            int indicesLen = indices.Length / 3;
            for(int i=0;i< vertices.Length;++i)
            {
                vertices[i].Normal = new Vector3(0f, 0f, 0f);
            }

            for(int i=0;i< indicesLen; ++i)
            {
                //get indices indexes
                int i1 = indices[i * 3];
                int i2 = indices[i * 3 + 1];
                int i3 = indices[i * 3 + 2];

                //get the two faces
                Vector3 face1 = vertices[i1].Position - vertices[i3].Position;
                Vector3 face2 = vertices[i1].Position - vertices[i2].Position;

                //get the cross product between them
                Vector3 normal = Vector3.Cross(face1, face2);

                //update the normal
                vertices[i1].Normal += normal;
                vertices[i2].Normal += normal;
                vertices[i3].Normal += normal;
            }
        }

        private void InitVertices()
        {
            float minH = float.MaxValue;
            float maxH = float.MinValue;

            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    if (heightInfo[x, y] < minH)
                    {
                        minH = heightInfo[x, y];
                    }
                    if (heightInfo[x, y] > maxH)
                    {
                        maxH = heightInfo[x, y];
                    }
                }
            }

            vertices = new VertexPositionColorNormal[width * height];

            //this codes creates the nice canyon look! :)
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    vertices[x + y * width].Position = new Vector3(x, heightInfo[x, y], y);

                    if (heightInfo[x, y] < minH + (maxH - minH) / 4)
                    {
                        vertices[x + y * width].Color = Color.SandyBrown;
                    }
                    else if (heightInfo[x, y] < minH + (maxH - minH) * 2 / 4)
                    {
                        vertices[x + y * width].Color = Color.BurlyWood;
                    }
                    else if (heightInfo[x, y] < minH + (maxH - minH) * 3 / 4)
                    {
                        vertices[x + y * width].Color = Color.SandyBrown;
                    }
                    else
                    {
                        vertices[x + y * width].Color = Color.Chocolate;
                    }
                }
            }
        }

        private void PrepareBuffers(GraphicsDevice graphicsDevice)
        {
            iBuffer = new IndexBuffer(graphicsDevice, typeof(int), indices.Length, BufferUsage.WriteOnly);
            iBuffer.SetData(indices);
            vBuffer = new VertexBuffer(graphicsDevice, VertexPositionColorNormal.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            vBuffer.SetData(vertices);
        }
    }
}
