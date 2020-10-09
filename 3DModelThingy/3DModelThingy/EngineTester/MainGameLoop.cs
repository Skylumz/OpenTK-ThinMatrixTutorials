using _3DModelThingy.RenderEngine;
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

        private Loader loader = new Loader();
        private Renderer renderer = new Renderer();

        float[] vertices =
        {
            //left bottom triangle
            -0.5f, 0.5f, 0f,
            -0.5f, -0.5f, 0f,
            0.5f, -0.5f, 0f,
            //right top triangle
            0.5f, -0.5f, 0f,
            0.5f, 0.5f, 0f,
            -0.5f, 0.5f, 0f,
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
            model = loader.LoadToVAO(vertices);

            Run(FPS_CAP);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            renderer.Prepare(WIDTH, HEIGHT);
            renderer.Render(model);

            SwapBuffers();
        }

        private void CleanUp()
        {
            loader.CleanUp();
        }
    }
}
