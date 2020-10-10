using _3DModelThingy.Entities;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DModelThingy.Toolbox
{
    public class Maths
    {
        public static Matrix4 CreateTransformationMatrix(Vector3 translation, float rx, float ry, float rz, float scale)
        {
            Matrix4 matrix = new Matrix4();
            matrix = Matrix4.Identity;
            
            var translationM = Matrix4.CreateTranslation(translation);
            var rxM = Matrix4.CreateRotationX(rx);
            var ryM = Matrix4.CreateRotationY(ry);
            var rzM = Matrix4.CreateRotationZ(rz);
            var scaleM = Matrix4.CreateScale(scale);

            matrix = rxM * ryM * rzM * translationM *  scaleM;

            return matrix;
        }

        public static Matrix4 CreateViewMatrix(Camera camera)
        {
            Matrix4 viewMatrix = new Matrix4();
            viewMatrix = Matrix4.Identity;

            var pitchM = Matrix4.CreateRotationX(camera.pitch);
            var yawM = Matrix4.CreateRotationY(camera.yaw);

            var negCameraPos = new Vector3(-camera.position.X, -camera.position.Y, -camera.position.Z);

            var translateM = Matrix4.CreateTranslation(negCameraPos);

            viewMatrix = pitchM * yawM * translateM;

            return viewMatrix;
        }
    }
}
