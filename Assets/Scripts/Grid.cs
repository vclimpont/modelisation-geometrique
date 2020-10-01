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
    private bool drawGrid;

    public Grid(int nbCubesPerRows, Vector3 minPos, Vector3 maxPos)
    {
        this.nbCubesPerRows = nbCubesPerRows;
        this.minPos = minPos;
        this.maxPos = maxPos;
        drawGrid = false;

        cellSize = GetMaxDistFromDimension() / nbCubesPerRows;
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

        float dimSize = (nbCubesPerRows * cellSize) / 2;

        for (float x = minPos.x; x <= maxPos.x; x += cellSize)
        {
            for (float y = minPos.y; y < maxPos.y; y += cellSize)
            {
                for (float z = minPos.z; z < maxPos.z; z += cellSize)
                {
                    Cube c = new Cube(new Vector3(x, y, z) + new Vector3(offset, offset, offset));
                    AddIntoCubesPartition(c, dimSize);
                }
            }
        }
    }

    void AddIntoCubesPartition(Cube c, float dimSize)
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

    public void SetDrawGrid(bool draw)
    {
        drawGrid = draw;
    }

    public bool GetDrawGrid()
    {
        return drawGrid;
    }

    public void DrawGridGizmos()
    {
        foreach (Cube c in cubes)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(c.Center, 0.01f);
        }
    }
}
