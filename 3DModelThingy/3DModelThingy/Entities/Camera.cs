using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DModelThingy.Entities
{
    public class Camera
    {
        public Vector3 position;
        public  float pitch { get; private set; }
        public float yaw { get; private set; }
        public float roll { get; private set; }

        public Camera()
        {
            position = new Vector3();
        }

        public void Move(Key e)
        {
            if(e == Key.W)
            {
                position.Z -= 0.02f;
            }

            if (e == Key.D)
            {
                position.X += 0.02f;
            }

            if (e == Key.A)
            {
                position.X -= 0.02f;
            }

            if (e == Key.S)
            {
                position.Z += 0.02f;
            }
        }

    }
}
