using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public List<LevelData> levels = new List<LevelData>();

    private void Start()
    {
    }

    private void StartLevel()
    {
        
    }

}
public struct LevelData
{
    public int levelNumber;
    public int gridWidth;
    public int gridHeight;
    public PlayerStartPostion playerStart;
    public List<WallData> walls;
    public List<LavaData> lavas;
}
public struct PlayerStartPostion
{
    public int x;
    public int y;
}

public struct WallData
{
    public int x;
    public int y;
}
public struct LavaData
{
    public int x;
    public int y;
}
