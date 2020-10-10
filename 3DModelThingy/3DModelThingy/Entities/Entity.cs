using _3DModelThingy.Models;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DModelThingy.Entities
{
    public class Entity
    {
        public TexturedModel Model;
        public Vector3 Position;
        public float RotationX;
        public float RotationY;
        public float RotationZ;
        public float Scale;

        public Entity(TexturedModel model, Vector3 position, float rotX, float rotY, float rotZ, float scale)
        {
            Model = model;
            Position = position;
            RotationX = rotX;
            RotationY = rotY;
            RotationZ = rotZ;
            Scale = scale;
        }

        public void IncreasePosition(float dx, float dy, float dz)
        {
            Position.X += dx;
            Position.Y += dy;
            Position.Z += dz;
        }

        public void IncreaseRotation(float dx, float dy, float dz)
        {
            RotationX += dx;
            RotationY += dy;
            RotationZ += dz;
        }
    }
}
