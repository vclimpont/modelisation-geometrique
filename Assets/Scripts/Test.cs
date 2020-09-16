using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class Test : MonoBehaviour
{

    [SerializeField] private string objName = "";
    [SerializeField] private Material mat = null;

    private Vector3[] vertices;
    private int[] triangles;
    private int[] facettes;



    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();          // Creation d'un composant MeshFilter qui peut ensuite être visualisé
        gameObject.AddComponent<MeshRenderer>();

        ReadFile();

        


        //for (int i = 0; i < vertices.Length; i++)
        //{
        //    Debug.Log(i + " : " + vertices[i]);
        //}

        //for (int i = 0; i < triangles.Length; i++)
        //{
        //    Debug.Log(i + " : " + triangles[i]);
        //}


        Mesh msh = new Mesh();                          // Création et remplissage du Mesh

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;           // Remplissage du Mesh et ajout du matériel
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    void CenterObject()
    {

    }

    void ReadFile()
    {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader("Assets/Resources/" + objName + ".off"))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.

                int k = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] lines = line.Split(' ');

                    if (k == 0)
                    {
                        k++;
                        continue;
                    }
                    if (k == 1)
                    {
                        vertices = new Vector3[int.Parse(lines[0])];
                        facettes = new int[int.Parse(lines[1])];

                        int t = int.Parse(lines[2]);
                        if(t == 0)
                        {
                            t = int.Parse(lines[1]) * 3;
                        }
                        triangles = new int[t];
                    }
                    else if (k < vertices.Length + 2)
                    {
                        var fmt = new NumberFormatInfo();
                        fmt.NegativeSign = "-";
                        vertices[k - 2] = new Vector3(float.Parse(lines[0], fmt), float.Parse(lines[1], fmt), float.Parse(lines[2], fmt));
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
    }


}
