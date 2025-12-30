using System;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    //texture yerine 3d de olabilir
    public Material cellTexture;
    public Material wallTexture;
    public Material lavaTexture;
    public Material bouncerTexture;
    public int x;
    public int y;

    public void InitCell(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    [SerializeField] private bool _isWall;

    public bool IsWall
    {
        get { return _isWall; }
        set { _isWall = value; SetTexture(); }
    }

    [SerializeField] private bool _isLava;

    public bool IsLava
    {
        get { return _isLava; }
        set { _isLava = value; SetTexture();}
    }

   [SerializeField] private bool _isBouncer;

    public bool IsBouncer
    {
        get { return _isBouncer; }
        set { _isBouncer = value; SetTexture(); }
    }


    private void SetTexture()
    {
        if (_isLava && _isWall)
        {
            Debug.LogError("A cell cannot be both lava and wall at the same time.");
            GetComponent<Renderer>().material = cellTexture;
        }
        else if (_isWall)
        {
            GetComponent<Renderer>().material = wallTexture;
        }
        else if (_isLava)
        {
            GetComponent<Renderer>().material = lavaTexture;
        }
        else if (_isBouncer)
        {
            GetComponent<Renderer>().material = bouncerTexture;
        }
        else
        {
            GetComponent<Renderer>().material = cellTexture;
        }
    }

}
