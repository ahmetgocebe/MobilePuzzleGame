using System;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public int x;
    public int y;

    public void InitCell(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    private bool _isWall;

    public bool IsWall
    {
        get { return _isWall; }
        set { _isWall = value; }
    }
    private bool _isLava;

    public bool IsLava
    {
        get { return _isLava; }
        set { _isLava = value; }
    }


}
