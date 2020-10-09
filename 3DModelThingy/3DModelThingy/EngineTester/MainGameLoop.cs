using _3DModelThingy.Models;
using _3DModelThingy.RenderEngine;
using _3DModelThingy.Shaders;
using _3DModelThingy.Textures;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DModelThingy.EngineTester
{
    public class MainGameLoop : GameWindow
    {
        public static readonly int WIDTH = 1280;
        public static readonly int HEIGHT = 720;
        public static readonly int FPS_CAP = 120;

        private Loader loader;
        private Renderer renderer;
        private StaticShader shader;

        float[] vertices =
        {
            -0.5f, 0.5f, 0f, //V0
            -0.5f, -0.5f, 0f, //V1
            0.5f, -0.5f, 0f, //V2
            0.5f, 0.5f, 0f, //V3
        };

        int[] indicies =
        {
            0, 1, 3, //top left triangle
            3, 1, 2 //bottom right triangle
        };

        float[] textureCoords =
        {
            0, 0, //V0
            0, 1, //V1
            1, 1, //V2 
            1, 0 //V3
        };

        RawModel model;
        ModelTexture modelTexture;
        TexturedModel texturedModel;

        public MainGameLoop(string t)
        {
            Title = t;
            Height = HEIGHT;
            Width = WIDTH;

            Closing += (s, e) => CleanUp();
        }

        public void Start()
        {
            loader = new Loader();
            renderer = new Renderer();
            shader = new StaticShader();
            
            model = loader.LoadToVAO(vertices, textureCoords, indicies);
            modelTexture = new ModelTexture(loader.LoadTexture("Resources\\image.jpg"));
            texturedModel = new TexturedModel(model, modelTexture);

            Run(FPS_CAP);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            renderer.Prepare(WIDTH, HEIGHT);

            shader.Start();
            renderer.RenderTexturedModel(texturedModel);
            shader.Stop();

            SwapBuffers();
        }

        private void CleanUp()
        {
            loader.CleanUp();
            shader.CleanUp();
        }
    }
}
