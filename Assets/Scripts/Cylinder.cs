﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{

    public Material mat;
    public int meridians = 0;
    public int height = 0;
    public int ray = 0;

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
            Debug.Log(j + " : " + vertices[j]);
        }

        int k = 0; // couvercle haut
        for (int i = 1; i <= meridians; i++)
        {
            triangles[k] = 0;
            triangles[k + 1] = i;

            if (i == meridians)
            {
                triangles[k + 2] = i - (meridians - 1);
            }
            else
            {
                triangles[k + 2] = i + 1;
            }

            k += 3;
        }

        int k2 = meridians * 3; // couvercle bas 
        for (int i = meridians + 2; i <= meridians * 2 + 1; i++)
        {
            triangles[k2] = meridians + 1;
            triangles[k2 + 1] = i;

            if (i == meridians + 2)
            {
                triangles[k2 + 2] = i + (meridians - 1);
            }
            else
            {
                triangles[k2 + 2] = i - 1;
            }

            k2 += 3;
        }

        int k3 = meridians * 6; // faces triangles 1
        for (int i = 1; i <= meridians; i++)
        {
            triangles[k3] = i;
            triangles[k3 + 1] = i + meridians + 1;

            if (i == meridians)
            {
                triangles[k3 + 2] = i + 2;
            }
            else
            {
                triangles[k3 + 2] = i + meridians + 2;
            }

            k3 += 3;
        }

        int k4 = meridians * 9; // faces triangles 2
        for (int i = 1; i <= meridians; i++)
        {
            triangles[k4] = i;

            if (i == meridians)
            {
                triangles[k4 + 1] = meridians + 2;
                triangles[k4 + 2] = i - (meridians - 1);
            }
            else
            {
                triangles[k4 + 1] = i + meridians + 2;
                triangles[k4 + 2] = i + 1;
            }

            k4 += 3;
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