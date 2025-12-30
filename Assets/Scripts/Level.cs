using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public int id;
    public List<Vector2> walls=new List<Vector2>();
    public List<Vector2> lava=new List<Vector2>();
}

