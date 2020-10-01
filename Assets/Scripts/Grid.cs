using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private List<Cube> cubesLeftBot;
    private List<Cube> cubesRightBot;
    private List<Cube> cubesLeftTop;
    private List<Cube> cubesRightTop;
    private int nbCubesPerRows;
    private Vector3 minPos;
    private Vector3 maxPos;

    private float offset;
    private float cellSize;
    private float dimSize;
    private bool drawGrid;

    public Grid(int nbCubesPerRows, Vector3 minPos, Vector3 maxPos)
    {
        this.nbCubesPerRows = nbCubesPerRows;
        this.minPos = minPos;
        this.maxPos = maxPos;
        drawGrid = false;

        cellSize = GetMaxDistFromDimension() / nbCubesPerRows;
        dimSize = (nbCubesPerRows * cellSize) / 2;
        offset = cellSize / 2f;

        CreateCubes();
    }

    void InitCubes()
    {
        cubesLeftBot = new List<Cube>();
        cubesRightBot = new List<Cube>();
        cubesLeftTop = new List<Cube>();
        cubesRightTop = new List<Cube>();
    }

    void CreateCubes()
    {
        InitCubes();

        for (float x = minPos.x; x <= maxPos.x; x += cellSize)
        {
            for (float y = minPos.y; y < maxPos.y; y += cellSize)
            {
                for (float z = minPos.z; z < maxPos.z; z += cellSize)
                {
                    Cube c = new Cube(new Vector3(x, y, z) + new Vector3(offset, offset, offset), offset);
                    AddIntoCubesPartition(c);
                }
            }
        }
    }

    void AddIntoCubesPartition(Cube c)
    {
        if(c.Center.x < dimSize)
        {
            if(c.Center.y < dimSize)
            {
                cubesLeftBot.Add(c);
            }
            else
            {
                cubesLeftTop.Add(c);
            }
        }
        else
        {
            if (c.Center.y < dimSize)
            {
                cubesRightBot.Add(c);
            }
            else
            {
                cubesRightTop.Add(c);
            }
        }
    }

    float GetMaxDistFromDimension()
    {
        return Mathf.Max(maxPos.x - minPos.x, maxPos.y - minPos.y, maxPos.z - minPos.z);
    }

    Cube FindCubeOfVertice(Vector3 vertice, List<Cube> cubesPartition)
    {
        foreach(Cube c in cubesPartition)
        {
            if(c.ContainsVertice(vertice))
            {
                c.Active = true;
                return c;
            }
        }

        return null;
    }

    public Cube FindCubeOfVertice(Vector3 vertice)
    {
        if (vertice.x < dimSize)
        {
            if (vertice.y < dimSize)
            {
                return FindCubeOfVertice(vertice, cubesLeftBot);
            }
            else
            {
                return FindCubeOfVertice(vertice, cubesLeftTop);
            }
        }
        else
        {
            if (vertice.y < dimSize)
            {
                return FindCubeOfVertice(vertice, cubesRightBot);
            }
            else
            {
                return FindCubeOfVertice(vertice, cubesRightTop);
            }
        }
    }

    void DrawPartitionCubesGizmos(List<Cube> partCubes)
    {
        foreach (Cube c in partCubes)
        {
            if(c.Active)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(c.Center, new Vector3(cellSize, cellSize, cellSize));
            }
        }
    }

    public void DrawGridGizmos()
    {
        DrawPartitionCubesGizmos(cubesLeftBot);
        DrawPartitionCubesGizmos(cubesRightBot);
        DrawPartitionCubesGizmos(cubesLeftTop);
        DrawPartitionCubesGizmos(cubesRightTop);
    }

    void LogVerticesOfPartitionCubes(List<Cube> partCube)
    {
        foreach (Cube c in partCube)
        {
            if(c.Active)
            {
                c.LogVertices();
            }
        }
    }

    public void LogVerticesOfCubes()
    {
        LogVerticesOfPartitionCubes(cubesLeftBot);
        LogVerticesOfPartitionCubes(cubesRightBot);
        LogVerticesOfPartitionCubes(cubesLeftTop);
        LogVerticesOfPartitionCubes(cubesRightTop);
    }

    public void SetDrawGrid(bool draw)
    {
        drawGrid = draw;
    }

    public bool GetDrawGrid()
    {
        return drawGrid;
    }

}
