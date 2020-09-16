using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public GameObject point;
    public Material mat;
    public int meridians = 0;
    public int parallels = 0;
    public int ray = 0;
    public int meridianTroncStart = 0;
    public int meridianTroncEnd = 0;

    private Vector3[] vertices;
    private int[] triangles;

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();          // Creation d'un composant MeshFilter qui peut ensuite être visualisé
        gameObject.AddComponent<MeshRenderer>();

        int v = parallels + (meridians * (parallels - 2)); // nb of vertices
        int t = ((meridians * 6) * (parallels - 2)) + (meridians * 6); // nb of points used to build triangles 
               // triangles of faces + triangles of faces to poles

        vertices = new Vector3[v];
        triangles = new int[t];

        BuildParallels();

        for (int j = 0; j < vertices.Length; j++)
        {
            Debug.Log(j + " : " + vertices[j]);
        }


        int k = 1;
        int q = 0;
        for(int j = 0; j < parallels - 3; j++)
        {
            for (int i = k; i < k + meridians; i++)
            {
                if(i % (meridians + 1) == meridianTroncStart)
                {
                    triangles[q] = i;
                    triangles[q + 1] = i - meridianTroncStart;
                    triangles[q + 2] = i - (meridians + 1) - meridianTroncStart; 

                    triangles[q + 3] = i;
                    triangles[q + 4] = i - (meridians + 1) - meridianTroncStart;
                    triangles[q + 5] = i - (meridians + 1);

                    i += (meridianTroncEnd - meridianTroncStart);
                    q += 6;
                }
                else
                {
                    if (i == k + meridians - 1)
                    {
                        triangles[q] = i;
                        triangles[q + 1] = i + meridians + 1;
                        triangles[q + 2] = i - (meridians - 1);

                        triangles[q + 3] = i - (meridians - 1);
                        triangles[q + 4] = i + meridians + 1;
                        triangles[q + 5] = i + 2;
                    }
                    else
                    {
                        triangles[q] = i;
                        triangles[q + 1] = i + meridians + 1;
                        triangles[q + 2] = i + 1;

                        triangles[q + 3] = i + 1;
                        triangles[q + 4] = i + meridians + 1;
                        triangles[q + 5] = i + meridians + 2;
                    }
                    q += 6;
                }
            }
            k += (meridians + 1);
        }


        for(int i = 1; i < meridians + 1; i++)
        {
            if(i == meridians)
            {
                triangles[q] = (meridians * (parallels - 2)) + (parallels - 2);
                triangles[q + 1] = i;
                triangles[q + 2] = i - (meridians - 1);
            }
            else
            {
                triangles[q] = (meridians * (parallels - 2)) + (parallels - 2);
                triangles[q + 1] = i;
                triangles[q + 2] = i + 1;
            }

            q += 6;
        }

        int k2 = (meridians + 1) * (parallels - 3) + 1;
        for (int i = k; i < k2 + meridians; i++)
        {
            if (i == k2 + meridians - 1)
            {
                triangles[q] = i;
                triangles[q + 1] = k2 + meridians + 1;
                triangles[q + 2] = i - (meridians - 1);
            }
            else
            {
                triangles[q] = i;
                triangles[q + 1] = k2 + meridians + 1;
                triangles[q + 2] = i + 1;
            }

            q += 6;
        }


        Mesh msh = new Mesh();                          // Création et remplissage du Mesh

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;           // Remplissage du Mesh et ajout du matériel
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    void BuildParallels()
    {
        int k = 0;
        for(int i = 1; i < parallels - 1; i++)
        {
            float phi = (Mathf.PI * i) / (parallels - 1);
            Debug.Log("phi : " + phi);
            k = BuildMeridian(k, phi);
            Debug.Log("k : " + k);
        }

        vertices[k] = new Vector3(0, 0, ray);       // build poles
        vertices[k + 1] = new Vector3(0, 0, -ray);
        k += 2;
    }

    int BuildMeridian(int k, float phi)
    {
        float teta = (2f * Mathf.PI) / meridians;
        Debug.Log("teta : " + teta);

        float z = ray * Mathf.Cos(phi);

        vertices[k] = new Vector3(0, 0, phi);
        k++;
        for (int i = 0; i < meridians; i++) // build vertices
        {
            float teta_i = teta * i;
            float x = ray * Mathf.Cos(teta_i) * Mathf.Sin(phi);
            float y = ray * Mathf.Sin(teta_i) * Mathf.Sin(phi);

            vertices[k] = new Vector3(x, y, z);
            k++;
        }

        return k;
    }
}
