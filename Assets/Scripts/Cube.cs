using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Cube
{
    private Vector3 center;
    private float offset;
    private Dictionary<int, Vector3> vertices;
    private bool active;
    private Vector3 avgVertice;
    private int idVertice;

    public Cube(Vector3 center, float offset)
    {
        this.center = center;
        this.offset = offset;
        active = false;
        vertices = new Dictionary<int, Vector3>();
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

    public void SetAvgVertice()
    {
        Assert.IsTrue(active);
        avgVertice = Vector3.zero;

        foreach(KeyValuePair<int, Vector3> v in vertices)
        {
            avgVertice += v.Value;
            SetIdVertice(v.Key);
        }
        avgVertice /= vertices.Count;

    }

    public void SetIdVertice(int idVertice)
    {
        this.idVertice = idVertice;
    }

    public bool ContainsVertice(Vector3 vertice)
    {
        return IsSuperiorToMin(vertice) && IsInferiorToMax(vertice);
    }

    public void AddVertice(int id, Vector3 v)
    {
        vertices.Add(id, v);
    }

    public void LogVertices()
    {
        string s = "";

        foreach (KeyValuePair<int, Vector3> v in vertices)
        {
            s += (v.Value + " ");
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

    public int IdVertice
    {
        get { return idVertice; }
    }

    public Vector3 AvgVertice
    {
        get { return avgVertice; }
    }

    public Dictionary<int, Vector3> GetVertices()
    {
        return vertices;
    }
}
