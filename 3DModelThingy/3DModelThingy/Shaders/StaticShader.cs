using _3DModelThingy.Entities;
using _3DModelThingy.Toolbox;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DModelThingy.Shaders
{
    public class StaticShader : ShaderProgram
    {
        private static readonly string VERTEXFILEPATH = "Shaders\\vertexShader.txt";
        private static readonly string FRAGMENTFILEPATH = "Shaders\\fragmentShader.txt";

        private int locationTransformationMatrix;
        private int locationProjectionMatrix;
        private int locationViewMatrix;

        public StaticShader() : base(VERTEXFILEPATH, FRAGMENTFILEPATH) { }

        protected override void BindAttributes()
        {
            BindAttribute(0, "position");
            BindAttribute(1, "textureCoords");
        }

        protected override void GetAllUniformLocations()
        {
            locationTransformationMatrix = GetUniformLocation("transformationMatrix");
            locationProjectionMatrix = GetUniformLocation("projectionMatrix");
            locationViewMatrix = GetUniformLocation("viewMatrix");
        }

        public void LoadTransformationMatrix(Matrix4 matrix)
        {
            LoadUniformMatrix(locationTransformationMatrix, matrix);
        }

        public void LoadProjectionMatrix(Matrix4 matrix)
        {
            LoadUniformMatrix(locationProjectionMatrix, matrix);
        }

        public void LoadViewMatrix(Camera cam)
        {
            Matrix4 viewMatrix = Maths.CreateViewMatrix(cam);
            LoadUniformMatrix(locationViewMatrix, viewMatrix);
        }
    }
}
