using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject cam;
    public Dictionary<(int, int), GridCell> gridCells = new Dictionary<(int, int), GridCell>();
    public GameObject cellPrefab;
    private bool isGridReady = false;
    private void Awake()
    {
        //TODO: load level from GameManager
        StartCoroutine(StartLevel());
    }
    IEnumerator StartLevel()
    {
        CreateGrid(5, 9);

        yield return new WaitUntil(() => isGridReady);
        //dummies
        CreateWall(3, 7);
        CreateWall(2, 4);

        CreateLava(4, 2);
        CreateLava(3, 8);

        CreateBouncer(2, 4);
        CreateBouncer(3, 3);
    }

    private void CreateBouncer(int v1, int v2)
    {
        if (gridCells.ContainsKey((v1, v2)))
        {
            gridCells[(v1, v2)].IsBouncer = true;
        }
        else
        {
            Debug.LogWarning($"No cell found at ({v1}, {v2}) to set as bouncer.");
        }
    }

    public void ClearGrid()
    {
        foreach (var cell in gridCells.Values)
        {
            cell.IsLava = false;
            cell.IsWall = false;
        }
    }

    private void CreateLava(int v1, int v2)
    {
        if (gridCells.ContainsKey((v1, v2)))
        {
            gridCells[(v1, v2)].IsLava = true;
        }
        else
        {
            Debug.LogWarning($"No cell found at ({v1}, {v2}) to set as lava.");
        }
    }

    private void CreateWall(int v1, int v2)
    {
        if (gridCells.ContainsKey((v1, v2)))
        {
            gridCells[(v1, v2)].IsWall = true;
        }
        else
        {
            Debug.LogWarning($"No cell found at ({v1}, {v2}) to set as wall.");
        }
    }

    public void CreateGrid(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject cellObject = Instantiate(cellPrefab);
                cellObject.transform.name = $"Cell_{x}_{y}";
                cellObject.transform.position = new Vector3(x, y, 0);
                cellObject.transform.parent = this.transform;
                cellObject.transform.localScale = Vector3.one * 0.9f;
                GridCell gridCell = cellObject.GetComponent<GridCell>();
                gridCell.InitCell(x, y);
                gridCells[(x, y)] = gridCell;
            }
        }
        isGridReady = true;
        FindMiddle(width, height);
    }

    private void FindMiddle(int width, int height)
    {
        float middleX = (width - 1) / 2f;
        float middleY = (height - 1) / 2f;
        cam.transform.position = new Vector3(middleX, middleY, -10);
    }
}