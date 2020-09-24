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
    private Vector3[] normales;
    private float[] nbNormales;



    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();          // Creation d'un composant MeshFilter qui peut ensuite être visualisé
        gameObject.AddComponent<MeshRenderer>();

        ReadFile();
        CenterAndNormalizeObject();



        //for (int i = 0; i < vertices.Length; i++)
        //{
        //    Debug.Log(i + " : " + vertices[i]);
        //}

        //for (int i = 0; i < triangles.Length; i++)
        //{
        //    Debug.Log(i + " : " + triangles[i]);
        //}
    }

    void Update()
    {
        CreateNormales();
        AverageNormals();

        Mesh msh = new Mesh();                          // Création et remplissage du Mesh

        //for (int i = 0; i < normales.Length; i++)
        //{
        //    //Debug.Log(i + " : " + normales[i]);
        //    Debug.DrawLine(vertices[i], vertices[i] + normales[i], Color.green);
        //}

        msh.vertices = vertices;
        msh.triangles = triangles;
        msh.normals = normales;

        gameObject.GetComponent<MeshFilter>().mesh = msh;           // Remplissage du Mesh et ajout du matériel
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    void CreateNormales()
    {
        normales = new Vector3[vertices.Length];
        for (int i = 0; i < normales.Length; i++) normales[i] = new Vector3(0, 0, 0);

        nbNormales = new float[vertices.Length];
        for (int i = 0; i < nbNormales.Length; i++) nbNormales[i] = 0f;

        int k = 0;
        for(int i = 0; i < normales.Length; i++)
        {
            int i1, i2, i3;
            i1 = triangles[k];
            i2 = triangles[k + 1];
            i3 = triangles[k + 2];

            Vector3 a = GetVectorFrom(vertices[i1], vertices[i2]);
            Vector3 b = GetVectorFrom(vertices[i1], vertices[i3]);
            Vector3 c = CrossProduct(a, b);

            normales[i1] += c;
            normales[i2] += c;
            normales[i3] += c;
            nbNormales[i1] += 1;
            nbNormales[i2] += 1;
            nbNormales[i3] += 1;
            k += 3;
        }
    }

    void AverageNormals()
    {
        for (int i = 0; i < normales.Length; i++)
        {
            normales[i] = (normales[i] / nbNormales[i]).normalized;
        }
    }

    Vector3 GetVectorFrom(Vector3 p1, Vector3 p2)
    {
        float x, y, z;

        x = p2.x - p1.x;
        y = p2.y - p1.y;
        z = p2.z - p1.z;

        return new Vector3(x, y, z);
    }

    Vector3 CrossProduct(Vector3 a, Vector3 b)
    {
        float x, y, z;

        x = (a.y * b.z)  - (a.z * b.y);
        y = (a.z * b.x) - (a.x * b.z);
        z = (a.x * b.y) - (a.y * b.x);

        Vector3 n = new Vector3(x, y, z);
        return n;
    }

    void CenterAndNormalizeObject()
    {
        Vector3 center = Vector3.zero;

        for(int i = 0; i < vertices.Length; i++)
        {
            center += vertices[i];
        }

        center /= vertices.Length;
        float max = 0;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] -= center;
            Vector3 v = vertices[i];

            if(Mathf.Abs(v.x) > max)
            {
                max = Mathf.Abs(v.x);
            }
            if (Mathf.Abs(v.y) > max)
            {
                max = Mathf.Abs(v.y);
            }
            if (Mathf.Abs(v.z) > max)
            {
                max = Mathf.Abs(v.z);
            }
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] /= max;
        }
        Debug.Log(center);
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
