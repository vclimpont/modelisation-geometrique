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
        for(int i = 0; i < vertices.Length; i++)
        {
            grid.FindCubeOfVertice(vertices[i]).AddVertice(i, vertices[i]);
        }
    }

    public void AverageVerticesInCubes()
    {
        foreach(Cube c in grid.GetActiveCubes())
        {
            c.SetAvgVertice();
        }
    }
}
