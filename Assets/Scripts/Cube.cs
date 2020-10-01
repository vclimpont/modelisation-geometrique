using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube
{
    private Vector3 center;
    private float offset;
    private List<Vector3> vertices;
    private bool active;

    public Cube(Vector3 center, float offset)
    {
        this.center = center;
        this.offset = offset;
        active = false;

        vertices = new List<Vector3>();
    }

    bool IsSuperiorToMin(Vector3 vertice)
    {
        Vector3 min = center - new Vector3(offset, offset, offset);
        return vertice.x >= min.x && vertice.y >= min.y && vertice.z >= min.z;
    }

    bool IsInferiorToMax(Vector3 vertice)
    {
        Vector3 max = center + new Vector3(offset, offset, offset);
        return vertice.x <= max.x && vertice.y <= max.y && vertice.z <= max.z;
    }

    public bool ContainsVertice(Vector3 vertice)
    {
        return IsSuperiorToMin(vertice) && IsInferiorToMax(vertice);
    }

    public void AddVertice(Vector3 v)
    {
        vertices.Add(v);
    }

    public void LogVertices()
    {
        string s = "";

        foreach (Vector3 v in vertices)
        {
            s += (v + " ");
        }
        Debug.Log("Cube center : " + center + " | " + s);
    }

    public Vector3 Center
    {
        get { return center; }
        set { center = value; }
    }

    public bool Active
    {
        get { return active; }
        set { active = value; }
    }
}
