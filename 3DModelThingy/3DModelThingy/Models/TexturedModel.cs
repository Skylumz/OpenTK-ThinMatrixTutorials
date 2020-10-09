using _3DModelThingy.Textures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DModelThingy.Models
{
    public class TexturedModel
    {
        public RawModel Model { get; private set; }
        public ModelTexture ModelTexture { get; private set; }

        public TexturedModel(RawModel m, ModelTexture tex)
        {
            Model = m;
            ModelTexture = tex;
        }
    }
}
