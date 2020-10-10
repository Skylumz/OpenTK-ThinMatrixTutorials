using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3DModelThingy.Entities;
using _3DModelThingy.Models;
using _3DModelThingy.Shaders;
using _3DModelThingy.Toolbox;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace _3DModelThingy.RenderEngine
{
    public class Renderer
    {
        private static readonly float FOV = 70;
        private static readonly float NEAR_PLANE = 0.1f;
        private static readonly float FAR_PLANE = 1000;

        private Matrix4 projectionMatrix;

        private int Width;
        private int Height;

        public Renderer(int w, int h, StaticShader shader)
        {
            Width = w;
            Height = h;

            CreateProjectionMatrix();
            shader.Start();
            shader.LoadProjectionMatrix(projectionMatrix);
            shader.Stop();
        }

        public void Prepare(int w, int h)
        {
            Width = w;
            Height = h;

            GL.Viewport(0, 0, Width, Height);
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.Blue);
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

        public void RenderEntity(Entity ent, StaticShader shader)
        {
            TexturedModel texturedModel = ent.Model;
            RawModel model = texturedModel.Model;
            GL.BindVertexArray(model.vaoID);
            GL.EnableVertexArrayAttrib(model.vaoID, 0);
            GL.EnableVertexArrayAttrib(model.vaoID, 1);

            Matrix4 transformationMatrix = Maths.CreateTransformationMatrix(ent.Position, ent.RotationX, ent.RotationY, ent.RotationZ, ent.Scale);
            shader.LoadTransformationMatrix(transformationMatrix);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texturedModel.ModelTexture.TextureID);
            GL.DrawElements(BeginMode.Triangles, model.vertexCount, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexArrayAttrib(model.vaoID, 0);
            GL.DisableVertexArrayAttrib(model.vaoID, 1);
            GL.BindVertexArray(0);
        }

        private void CreateProjectionMatrix()
        {
            float aspectRatio = (Width / Height);
            float yScale = Convert.ToSingle(1f / Math.Tan(MathHelper.DegreesToRadians(FOV / 2f)) * aspectRatio);
            float xScale = yScale / aspectRatio;
            float frustrumLength = FAR_PLANE - NEAR_PLANE;

            projectionMatrix = new Matrix4();
            projectionMatrix.M11 = xScale;
            projectionMatrix.M22 = yScale;
            projectionMatrix.M33 = -((FAR_PLANE + NEAR_PLANE)) / frustrumLength;
            projectionMatrix.M34 = -1;
            projectionMatrix.M43 = -((2 * NEAR_PLANE * FAR_PLANE) / frustrumLength);
            projectionMatrix.M44 = 0;
        }
    }
}
