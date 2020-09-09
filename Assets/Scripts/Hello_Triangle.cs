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

        int k = columns * rows * 6;
        Vector3[] vertices = new Vector3[k];            // Création des structures de données qui accueilleront sommets et  triangles
        int[] triangles = new int[k];

        int id = 0;
        if(columns == 0 || rows == 0)
        {
            return;
        }

        for(int i = 0; i < columns; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                vertices[id] = new Vector3(i, j, 0);            // Remplissage de la structure sommet 
                vertices[id+1] = new Vector3(i, j+1, 0);
                vertices[id+2] = new Vector3(i+1, j, 0);
                vertices[id+3] = new Vector3(i+1, j, 0);            // Remplissage de la structure sommet 
                vertices[id+4] = new Vector3(i, j+1, 0);
                vertices[id+5] = new Vector3(i+1, j+1, 0);

                triangles[id] = id;                               // Remplissage de la structure triangle. Les sommets sont représentés par leurs indices
                triangles[id+1] = id+1;                               // les triangles sont représentés par trois indices (et sont mis bout à bout)
                triangles[id+2] = id+2;
                triangles[id+3] = id+3;                               // Remplissage de la structure triangle. Les sommets sont représentés par leurs indices
                triangles[id+4] = id+4;                               // les triangles sont représentés par trois indices (et sont mis bout à bout)
                triangles[id+5] = id+5;

                id += 6;
            }
        }

        Mesh msh = new Mesh();                          // Création et remplissage du Mesh

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;           // Remplissage du Mesh et ajout du matériel
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    void DrawSquares(int id, int i, int j)
    {
        for(int p = id; p < 6; p++)
        {

        }
    }
}