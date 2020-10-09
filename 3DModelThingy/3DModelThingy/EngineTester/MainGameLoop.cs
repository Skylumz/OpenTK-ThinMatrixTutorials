using _3DModelThingy.RenderEngine;
using _3DModelThingy.Shaders;
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

        RawModel model;

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

            model = loader.LoadToVAO(vertices, indicies);

            Run(FPS_CAP);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            renderer.Prepare(WIDTH, HEIGHT);

            shader.Start();
            renderer.Render(model);
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
