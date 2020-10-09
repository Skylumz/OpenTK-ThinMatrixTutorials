using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3DModelThingy.Models;
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

        public void RenderModel(RawModel model)
        {
            GL.BindVertexArray(model.vaoID);
            GL.EnableVertexArrayAttrib(model.vaoID, 0);
            GL.DrawElements(BeginMode.Triangles, model.vertexCount, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexArrayAttrib(model.vaoID, 0);
            GL.BindVertexArray(0);
        }

        //he changes the render function but Im going to make 2 seperate ones
        public void RenderTexturedModel(TexturedModel texturedModel)
        {
            RawModel model = texturedModel.Model;
            GL.BindVertexArray(model.vaoID);
            GL.EnableVertexArrayAttrib(model.vaoID, 0);
            GL.EnableVertexArrayAttrib(model.vaoID, 1);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texturedModel.ModelTexture.TextureID);
            GL.DrawElements(BeginMode.Triangles, model.vertexCount, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexArrayAttrib(model.vaoID, 0);
            GL.DisableVertexArrayAttrib(model.vaoID, 1);
            GL.BindVertexArray(0);
        }
    }
}
