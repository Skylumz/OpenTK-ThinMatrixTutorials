using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace _3DModelThingy.RenderEngine
{
    public class Loader
    {
        private List<int> vaos = new List<int>();
        private List<int> vbos = new List<int>();

        public RawModel LoadToVAO(float[] positions)
        {
            int vaoID = CreateVAO();
            StoreDataInAttributeList(0, positions);
            UnbindVAO();
            return new RawModel(vaoID, positions.Length / 3);
        }

        private int CreateVAO() 
        {
            int vaoID = GL.GenVertexArray();
            vaos.Add(vaoID);
            GL.BindVertexArray(vaoID);
            return vaoID;
        }

        private void StoreDataInAttributeList(int attNumber, float[] data)
        {
            int vboID = GL.GenBuffer();
            vbos.Add(vboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData<float>(BufferTarget.ArrayBuffer, (data.Length * sizeof(float)), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attNumber, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void UnbindVAO() 
        {
            GL.BindVertexArray(0);
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
        }
    }
}
