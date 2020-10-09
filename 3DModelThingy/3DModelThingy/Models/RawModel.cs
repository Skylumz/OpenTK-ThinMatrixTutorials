using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DModelThingy.Models 
{
    public class RawModel
    {
        public int vaoID { get; private set; }
        public int vertexCount { get; private set; }

        public RawModel(int vID, int vCount)
        {
            vaoID = vID;
            vertexCount = vCount;
        }
    }
}
