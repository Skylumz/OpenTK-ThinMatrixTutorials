using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3DModelThingy.Models;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace _3DModelThingy.RenderEngine
{
    public class Loader
    {
        private List<int> vaos = new List<int>();
        private List<int> vbos = new List<int>();
        private List<int> textures = new List<int>();

        public RawModel LoadToVAO(float[] positions, float[] textureCoords, int[] indicies)
        {
            int vaoID = CreateVAO();
            BindIndiciesBuffer(indicies);
            StoreDataInAttributeList(0, 3, positions);
            StoreDataInAttributeList(1, 2, textureCoords);
            UnbindVAO();
            return new RawModel(vaoID, indicies.Length);
        }

        private int CreateVAO() 
        {
            int vaoID = GL.GenVertexArray();
            vaos.Add(vaoID);
            GL.BindVertexArray(vaoID);
            return vaoID;
        }

        private void StoreDataInAttributeList(int attNumber, int coordinateSize, float[] data)
        {
            int vboID = GL.GenBuffer();
            vbos.Add(vboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData<float>(BufferTarget.ArrayBuffer, (data.Length * sizeof(float)), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attNumber, coordinateSize, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void UnbindVAO() 
        {
            GL.BindVertexArray(0);
        }

        private void BindIndiciesBuffer(int[] indicies)
        {
            int vboID = GL.GenBuffer();
            vbos.Add(vboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboID);
            GL.BufferData<int>(BufferTarget.ElementArrayBuffer, indicies.Length * sizeof(int), indicies, BufferUsageHint.StaticDraw);
        }

        public int loadImage(Bitmap image)
        {
            int texID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texID);
            BitmapData data = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            image.UnlockBits(data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return texID;
        }

        public int LoadTexture(string filename)
        {
            Bitmap file = new Bitmap(filename);
            return loadImage(file);
        }

        public void CleanUp()
        {
            foreach(int vao in vaos)
            {
                GL.DeleteVertexArray(vao);
            }
            foreach(int vbo in vbos)
            {
                GL.DeleteBuffer(vbo);
            }
            foreach(int texture in textures)
            {
                GL.DeleteTexture(texture);
            }
        }
    }
}
