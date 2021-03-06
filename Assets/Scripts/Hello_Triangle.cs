﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hello_Triangle : MonoBehaviour
{

    public Material mat;
    public int columns = 0;
    public int rows = 0;

    private Vector3[] vertices;
    private int[] triangles;

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();          // Creation d'un composant MeshFilter qui peut ensuite être visualisé
        gameObject.AddComponent<MeshRenderer>();

        int v = (columns + 1) * (rows + 1); // nb of vertices
        int t = columns * rows * 6; // nb of points used to build triangles
        vertices = new Vector3[v];
        triangles = new int[t];

        BuildVertices(v);

        for(int j = 0; j < vertices.Length; j++)
        {
            Debug.Log(vertices[j]);
        }

        int k = 0;
        int f1 = 0;
        for(int i = 0; i < v-(columns+1); i++)
        {
            if(f1 < columns)
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
        for (int i = 1; i < v-(columns+1); i++)
        {
            if(f2 < columns)
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

    void BuildVertices(int v)
    {
        int x = 0;
        int y = 0;
        for (int i = 0; i < v; i++) // build vertices
        {
            if (i != 0 && i % (columns + 1) == 0)
            {
                x = 0;
                y++;
            }

            vertices[i] = new Vector3(x, y, 0);
            x++;
        }
    }
}