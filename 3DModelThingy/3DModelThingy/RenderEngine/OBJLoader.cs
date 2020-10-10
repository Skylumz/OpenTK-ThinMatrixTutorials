using _3DModelThingy.Models;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DModelThingy.RenderEngine
{
    public class OBJLoader
    {
        public static RawModel LoadObjFile(string path, Loader loader)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> textures = new List<Vector2>();
            List<Vector3> normals = new List<Vector3>();
            List<int> indices = new List<int>();
            float[] verticesArray = null;
            float[] texturesArray = null;
            float[] normalsArray = null;
            int[] indiciesArray = null;

            try
            {
                StreamReader reader = new StreamReader(path);
                while (true)
                {
                    string line = reader.ReadLine();
                    string[] currentLine = line.Split(' ');
                    if (line.StartsWith("v "))
                    {
                        Vector3 vert = new Vector3(float.Parse(currentLine[1]), float.Parse(currentLine[2]), float.Parse(currentLine[3]));
                        vertices.Add(vert);
                    }
                    else if (line.StartsWith("vt "))
                    {
                        Vector2 tc = new Vector2(float.Parse(currentLine[1]), float.Parse(currentLine[2]));
                        textures.Add(tc);
                    }
                    else if (line.StartsWith("vn "))
                    {
                        Vector3 normal = new Vector3(float.Parse(currentLine[1]), float.Parse(currentLine[2]), float.Parse(currentLine[3]));
                        normals.Add(normal);
                    }
                    else if (line.StartsWith("f "))
                    {
                        texturesArray = new float[vertices.Count * 2];
                        normalsArray = new float[vertices.Count * 3];
                        break;
                    }
                }

                string l;
                while ((l = reader.ReadLine()) != null)
                {
                    string[] currentLine = l.Split(' ');
                    string[] vertex1 = currentLine[1].Split('/');
                    string[] vertex2 = currentLine[2].Split('/');
                    string[] vertex3 = currentLine[3].Split('/');

                    processVertex(vertex1, indices, textures, normals, texturesArray, normalsArray);
                    processVertex(vertex2, indices, textures, normals, texturesArray, normalsArray);
                    processVertex(vertex3, indices, textures, normals, texturesArray, normalsArray);
                }

                reader.Close();
            }
            catch { return null; }

            verticesArray = new float[vertices.Count * 3];
            indiciesArray = new int[indices.Count];
            int vertexPointer = 0;
            foreach (Vector3 v in vertices)
            {
                verticesArray[vertexPointer++] = v.X;
                verticesArray[vertexPointer++] = v.Y;
                verticesArray[vertexPointer++] = v.Z;
            }

            for (int i = 0; i < indices.Count; i++)
            {
                indiciesArray[i] = indices[i];
            }

            return loader.LoadToVAO(verticesArray, texturesArray, indiciesArray);
        }

        private static void processVertex(string[] data, List<int> indicies, List<Vector2> textures, List<Vector3> normals, float[] texArray, float[] nrmArray)
        {
            int currentVPointer = int.Parse(data[0]) - 1;
            indicies.Add(currentVPointer);
            Vector2 currentTex = textures[int.Parse(data[1]) - 1];
            texArray[currentVPointer * 2] = currentTex.X;
            texArray[currentVPointer * 2 + 1] = currentTex.Y;
            Vector3 currentNormal = normals[int.Parse(data[2]) - 1];
            nrmArray[currentVPointer * 3] = currentNormal.X;
            nrmArray[currentVPointer * 3 + 1] = currentNormal.Y;
            nrmArray[currentVPointer * 3 + 2] = currentNormal.Y;
        }
    }
}
