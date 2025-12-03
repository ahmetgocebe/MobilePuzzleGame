using System;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject cam;
    public Dictionary<(int, int), GridCell> gridCells = new Dictionary<(int, int), GridCell>();
    public GameObject cellPrefab;
    private void Start()
    {
        CreateGrid(5, 9);
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
                GridCell gridCell = cellObject.AddComponent<GridCell>();
                gridCell.InitCell(x, y);
                gridCells[(x, y)] = gridCell;
            }
        }
        FindMiddle(width,height);
    }

    private void FindMiddle(int width, int height)
    {
        float middleX = (width - 1) / 2f;
        float middleY = (height - 1) / 2f;
        cam.transform.position = new Vector3(middleX, middleY, -10);

    }
}
