using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DModelThingy.Textures
{
    public class ModelTexture
    {
        public int TextureID { get; private set; }

        public ModelTexture(int texID)
        {
            TextureID = texID;
        }
    }
}
