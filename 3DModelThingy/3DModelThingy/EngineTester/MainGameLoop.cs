using _3DModelThingy.Entities;
using _3DModelThingy.Models;
using _3DModelThingy.RenderEngine;
using _3DModelThingy.Shaders;
using _3DModelThingy.Textures;
using OpenTK;
using OpenTK.Input;
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
        private Camera cam;

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
        Entity entity;

        //todo : CHANGE INPUT MANAGEMENT terrible way to manage keyboard input but it works for now 
        Key currentKeyDown;

        public MainGameLoop(string t)
        {
            Title = t;
            Height = HEIGHT;
            Width = WIDTH;

            KeyDown += MainGameLoop_KeyDown;
            KeyUp += MainGameLoop_KeyUp;
            Closing += (s, e) => CleanUp();
        }

        private void MainGameLoop_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            currentKeyDown = Key.Unknown;
        }

        private void MainGameLoop_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            currentKeyDown = e.Key;
        }
        
        public void Start()
        {
            loader = new Loader();
            shader = new StaticShader();
            cam = new Camera();
            renderer = new Renderer(WIDTH, HEIGHT, shader);

            model = OBJLoader.LoadObjFile("Resources\\stall.obj", loader);
            //model = loader.LoadToVAO(vertices, textureCoords, indicies);
            modelTexture = new ModelTexture(loader.LoadTexture("Resources\\stallimage.jpg"));
            texturedModel = new TexturedModel(model, modelTexture);
            
            entity = new Entity(texturedModel, new Vector3(0, 0, -25), 0, 0, 0, 1);

            Run(FPS_CAP);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);  
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            entity.IncreaseRotation(0, .01f, 0);
            cam.Move(currentKeyDown);

            renderer.Prepare(WIDTH, HEIGHT);

            shader.Start();
            shader.LoadViewMatrix(cam);
            renderer.RenderEntity(entity, shader);
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
