using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hello_Triangle : MonoBehaviour
{

    public Material mat;
    public int columns = 0;
    public int rows = 0;

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();          // Creation d'un composant MeshFilter qui peut ensuite être visualisé
        gameObject.AddComponent<MeshRenderer>();

        int v = (columns + 1) * (rows + 1);
        int t = columns * rows * 6;
        Vector3[] vertices = new Vector3[v];
        int[] triangles = new int[t];

        int x = 0;
        int y = 0;
        for(int i = 0; i < v; i++)
        {
            if (i != 0 && i % (columns + 1) == 0)
            {
                x = 0;
                y++;
            }

            vertices[i] = new Vector3(x, y, 0);
            x++;
        }

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

        //int k = columns * rows * 6;
        //Vector3[] vertices = new Vector3[k];            // Création des structures de données qui accueilleront sommets et  triangles
        //int[] triangles = new int[k];

        //int id = 0;
        //if(columns == 0 || rows == 0)
        //{
        //    return;
        //}

        //for(int i = 0; i < columns; i++)
        //{
        //    for(int j = 0; j < rows; j++)
        //    {
        //        vertices[id] = new Vector3(i, j, 0);            // Remplissage de la structure sommet 
        //        vertices[id+1] = new Vector3(i, j+1, 0);
        //        vertices[id+2] = new Vector3(i+1, j, 0);
        //        vertices[id+3] = new Vector3(i+1, j, 0);            // Remplissage de la structure sommet 
        //        vertices[id+4] = new Vector3(i, j+1, 0);
        //        vertices[id+5] = new Vector3(i+1, j+1, 0);

        //        triangles[id] = id;                               // Remplissage de la structure triangle. Les sommets sont représentés par leurs indices
        //        triangles[id+1] = id+1;                               // les triangles sont représentés par trois indices (et sont mis bout à bout)
        //        triangles[id+2] = id+2;
        //        triangles[id+3] = id+3;                               // Remplissage de la structure triangle. Les sommets sont représentés par leurs indices
        //        triangles[id+4] = id+4;                               // les triangles sont représentés par trois indices (et sont mis bout à bout)
        //        triangles[id+5] = id+5;

        //        id += 6;
        //    }
        //}

        Mesh msh = new Mesh();                          // Création et remplissage du Mesh

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;           // Remplissage du Mesh et ajout du matériel
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }
}