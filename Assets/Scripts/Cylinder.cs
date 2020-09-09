using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{

    public Material mat;
    public int meridians = 0;
    public int height = 0;
    public int ray = 0;

    public int columns;
    public int rows;

    private Vector3[] vertices;
    private int[] triangles;

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();          // Creation d'un composant MeshFilter qui peut ensuite être visualisé
        gameObject.AddComponent<MeshRenderer>();

        int v = (meridians * 2) + 2; // nb of vertices
        int t = meridians * 12; // nb of points used to build triangles
        vertices = new Vector3[v];
        triangles = new int[t];

        BuildMeridian(0, height/2);
        BuildMeridian(meridians + 1, -height/2);

        for (int j = 0; j < vertices.Length; j++)
        {
            Debug.Log(vertices[j]);
        }

        int k = 0;
        int f1 = 0;
        for (int i = 0; i < v - (columns + 1); i++)
        {
            if (f1 < columns)
            {
                triangles[k] = i;
                triangles[k + 1] = i + (columns + 1);
                triangles[k + 2] = i + 1;
                k += 3;
                f1++;
            }
            else
            {
                f1 = 0;
            }

        }

        int k2 = rows * columns * 3;
        int f2 = 0;
        for (int i = 1; i < v - (columns + 1); i++)
        {
            if (f2 < columns)
            {
                triangles[k2] = i;
                triangles[k2 + 1] = i + columns;
                triangles[k2 + 2] = i + (columns + 1);
                k2 += 3;
                f2++;
            }
            else
            {
                f2 = 0;
            }
        }

        Mesh msh = new Mesh();                          // Création et remplissage du Mesh

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;           // Remplissage du Mesh et ajout du matériel
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    void BuildMeridian(int k, float h)
    {
        float teta = (2f * Mathf.PI) / meridians;
        Debug.Log("teta : " + teta);

        vertices[k] = new Vector3(0, 0, h);
        k++;
        for (int i = 0; i < meridians; i++) // build vertices
        {
            float teta_i = teta * i;
            vertices[k] = new Vector3(ray * Mathf.Cos(teta_i), ray * Mathf.Sin(teta_i), h);
            k++;
        }
    }
}