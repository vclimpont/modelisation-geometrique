using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Loader
{
    public static int ReadFile(string obj, Vector3[] vertices, int[] triangles, int[] facettes)
    {
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader("Assets/Resources/" + obj + ".off"))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.

                int k = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] lines = line.Split(' ');

                    if(k == 0)
                    {
                        k++;
                        continue;
                    }
                    if (k == 1)
                    {
                        vertices = new Vector3[int.Parse(lines[0])];
                        facettes = new int[int.Parse(lines[1])];
                        triangles = new int[int.Parse(lines[2])];
                    }
                    else if (k < vertices.Length + 2)
                    {
                        vertices[k - 2] = new Vector3(float.Parse(lines[0]), float.Parse(lines[1]), float.Parse(lines[2]));
                    }
                    else
                    {
                        int i = (k - (vertices.Length + 2)) * 3;

                        triangles[i] = int.Parse(lines[1]);
                        triangles[i + 1] = int.Parse(lines[2]);
                        triangles[i + 2] = int.Parse(lines[3]);
                    }

                    k++;
                }
            }

            return 1;
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Debug.Log("The file could not be read:");
            Debug.Log(e.Message);

            return -1;
        }
    }
}
