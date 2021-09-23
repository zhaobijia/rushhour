using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum TileType
{
    Normal,
    Obstacle,
    Destructible,
    EnemyButton,
    EnemySpawn,
    Empty,
}

public class Tile : MonoBehaviour
{
    public TileType type;
    Vector3 position;
    
}
