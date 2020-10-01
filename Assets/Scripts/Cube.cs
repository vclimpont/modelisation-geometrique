using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube
{
    private Vector3 center;
    private Vector3[] vertices;

    public Cube(Vector3 center)
    {
        this.center = center;
        SaveVertices(0);
    }

    void SaveVertices(float offset)
    {
        vertices = new Vector3[8];
        float x = center.x;
        float y = center.y;
        float z = center.z;

        vertices[0] = new Vector3(x - offset, y - offset, z - offset);
        vertices[1] = new Vector3(x - offset, y - offset, z - offset);
        vertices[2] = new Vector3(x - offset, y - offset, z - offset);
        vertices[3] = new Vector3(x - offset, y - offset, z - offset);
        vertices[4] = new Vector3(x - offset, y - offset, z - offset);
        vertices[5] = new Vector3(x - offset, y - offset, z - offset);
        vertices[6] = new Vector3(x - offset, y - offset, z - offset);
        vertices[7] = new Vector3(x - offset, y - offset, z - offset);
    }

    public Vector3 Center
    {
        get { return center; }
        set { center = value; }
    }
}
