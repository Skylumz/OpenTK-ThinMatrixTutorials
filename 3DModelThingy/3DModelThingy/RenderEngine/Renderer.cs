using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace _3DModelThingy.RenderEngine
{
    public class Renderer
    {
        public void Prepare(int w, int h)
        {
            GL.Viewport(0, 0, w, h);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.Red);
        }

        public void Render(RawModel model)
        {
            GL.BindVertexArray(model.vaoID);
            GL.EnableVertexArrayAttrib(model.vaoID, 0);
            GL.DrawElements(BeginMode.Triangles, model.vertexCount, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexArrayAttrib(model.vaoID, 0);
            GL.BindVertexArray(0);
        }
    }
}
