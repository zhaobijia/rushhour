using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGrid : MonoBehaviour
{
    public Tilemap walkableMap;
    public Tilemap enemySpawnMap;
    public Transform playerSpawnTransform;

    List<Vector3> GetTilePositions(Tilemap map)
    {
        List<Vector3> tilePositions = new List<Vector3>();

        Tile[] tiles = map.GetComponentsInChildren<Tile>();
       
        foreach (Tile tile in tiles)
        {
            Vector3 pos = tile.transform.position;

            tilePositions.Add(pos);
        }

        return tilePositions;

    }

    public List<Vector3> GetEnemySpawnTilePositions()
    {
        return GetTilePositions(enemySpawnMap);
    }
}
