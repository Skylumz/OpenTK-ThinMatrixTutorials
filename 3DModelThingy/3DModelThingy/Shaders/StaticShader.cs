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

        public StaticShader() : base(VERTEXFILEPATH, FRAGMENTFILEPATH) { }

        protected override void BindAttributes()
        {
            BindAttribute(0, "position");
        }
    }
}
