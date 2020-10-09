using System;
using System.Collections.Generic;
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
            GL.ClearColor(new Color4(1, 0, 0, 1));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public void Render(RawModel model)
        {
            GL.BindVertexArray(model.vaoID);
            GL.EnableVertexArrayAttrib(model.vaoID, 0);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, model.vertexCount);
            GL.DisableVertexArrayAttrib(model.vaoID, 0);
            GL.BindVertexArray(0);
        }
    }
}
