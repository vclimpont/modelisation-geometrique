using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSimplifier
{
    private Vector3[] vertices;
    private Grid grid;

    public MeshSimplifier(Vector3[] vertices, Grid grid)
    {
        this.vertices = vertices;
        this.grid = grid;
    }

    public void PartitionVerticesInGrid()
    {
        foreach(Vector3 v in vertices)
        {
            grid.FindCubeOfVertice(v).AddVertice(v);
        }
    }
}
