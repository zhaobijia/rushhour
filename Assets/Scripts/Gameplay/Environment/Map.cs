using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[System.Serializable]
public struct TilePrefabType
{
    public TileType tileType;
    public GameObject tilePrefab;
}

[System.Serializable]
public struct LevelMapGrid
{
    public int level;
    public MapGrid mapGrid;

}

public class Map : MonoBehaviour
{
    /* 
    [SerializeField] TilePrefabType[] m_tilePrefabType;
    [SerializeField] TextAsset mapfile;
    [SerializeField] Vector2 tileSize;
    [SerializeField] GameObject enemySpawnButtonPrefab;
    
    Vector2 m_mapSize;
    string[][] m_mapRead;
    
    Dictionary<TileType, GameObject> m_tilePrefabDict = new Dictionary<TileType, GameObject>();
    List<NavMeshSurface> m_walkableSurfaces = new List<NavMeshSurface>();
    */
    [SerializeField] LevelMapGrid[] levelMapGrids;
    Dictionary<int, MapGrid> mapDict = new Dictionary<int, MapGrid>();

    MapGrid currentLevelMap;
    GameManager m_gameManager;

    [SerializeField] float enemySpawnOffsetY = 1.0f;

    [HideInInspector]public List<Vector3> enemySpawnPositions = new List<Vector3>();


    public Vector3 playerSpawnPosition;

    private void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        LoadMapDict();
    }

    public void LoadMapOnLevel(int level)
    {
        //old ways of map loading:
        // LoadMap();
        // LoadPrefab();

        //new ways:
        //prebaked map now :) ,load map base on level
        currentLevelMap = mapDict[level];
        currentLevelMap.gameObject.SetActive(true);
        SetEnemySpawnPositions();

    }
    public void HideMapOnLevel()
    {
        currentLevelMap.gameObject.SetActive(false);
    }

    void LoadMapDict()
    {
        foreach (LevelMapGrid grid in levelMapGrids)
        {
            mapDict.Add(grid.level, grid.mapGrid);
        }
    }

    void SetEnemySpawnPositions()
    {
        enemySpawnPositions = currentLevelMap.GetEnemySpawnTilePositions();
    }

    /* // Test on position of map
    private void OnDrawGizmos()
    {
        GetTilePositions();
        Gizmos.color = Color.red;
        foreach (Vector3 pos in tilePositions)
        {
            Debug.Log(pos);
            Gizmos.DrawSphere(pos, 1);
        }
        //  Gizmos.DrawSphere(Vector3.zero, 1);
    }
    */
    
 
    /*
    public void MapOnStart()
    {
        CreateMap();
        BuildNavMesh();
    }
    private void LoadMap()
    {      
        string[] rowReads;
        string[] colReads;
        
        if (mapfile != null)
        {           
            rowReads = mapfile.text.Split(new[] { "\r\n", "\r", "\n" },
    StringSplitOptions.None);
            m_mapRead = new string[rowReads.Length][];
            for(int i = 0; i<rowReads.Length; i++)
            {
                colReads = rowReads[i].Split(',');
                m_mapRead[i] = new string[colReads.Length];
                for(int j = 0; j<colReads.Length; j++)
                {                    
                    m_mapRead[i][j] = colReads[j];
                }
            }

            
        }
       
    }
    private void DebugMap()
    {
        int rowLen = m_mapRead.Length;


        for (int i = 0; i < rowLen; i++)
        {
            int colLen = m_mapRead[i].Length;


            for (int j = 0; j < colLen; j++)
            {
                Debug.Log("i,j :" + i + " , " + j);
                Debug.Log(m_mapRead[i][j]);
                
            }
        }
    }
    private void LoadPrefab()
    {
        for(int i = 0; i < m_tilePrefabType.Length; i++)
        {
            m_tilePrefabDict.Add(m_tilePrefabType[i].tileType, m_tilePrefabType[i].tilePrefab);
        }
    }
    private TileType GetTileType(string tileChar)
    {
        TileType tile;
        switch(tileChar){
            case "o":
                tile =  TileType.Normal;
                break;
            case "x":
                tile = TileType.Obstacle;
                break;
            case "d":
                tile = TileType.Destructible;
                break;
            case "b":
                tile = TileType.EnemyButton;
                break;
            case "s":
                tile = TileType.EnemySpawn;
                break;
            case "e":
                tile = TileType.Empty;
                break;
            default:
                tile = TileType.Empty;
                break;
        }
        return tile;
    }
    
    private void BuildNavMesh()
    {
        foreach (NavMeshSurface surface in m_walkableSurfaces)
        {
            surface.BuildNavMesh();
        }
    }
    
    private void CreateMap()
    {
        int rowLen = m_mapRead.Length;
        float rowOffest = ((rowLen-1) * tileSize.y)/2f;
        //for assign enenmy type to enemy button 
        int enemy_button_index = 0;
        
        for(int i = 0; i < rowLen; i++)
        {
            int colLen = m_mapRead[i].Length;          
            float colOffset =  ((colLen-1) * tileSize.x)/2f;
            
            for(int j = 0; j < colLen; j++)
            {
                
                TileType type = GetTileType(m_mapRead[i][j]);
                
                GameObject tilePrb = m_tilePrefabDict[type];
                Vector3 pos = new Vector3(j* tileSize.x - colOffset, -2, i * tileSize.y - rowOffest);

                if (type.Equals(TileType.EnemySpawn))
                {
                    enemySpawnTilePostions.Add(pos);

                }else if (type.Equals(TileType.Destructible))
                {
                    playerSpawnPosition = pos;
                }else if (type.Equals(TileType.EnemyButton))
                {
                    Vector3 offset = new Vector3(0, 1, 0);
                    GameObject buttonObj =  Instantiate(enemySpawnButtonPrefab, pos + offset, Quaternion.identity);
                    EnemyButton ene_button = buttonObj.GetComponent<EnemyButton>();
                    ene_button._enemyToSpawn =  m_gameManager.m_currentLevel.m_levelEnemyIds[enemy_button_index];
                    ene_button._amountToSpawn = 5;
                }

                GameObject tileObj = Instantiate(tilePrb, pos, Quaternion.identity);
                tileObj.transform.SetParent(this.transform);
                
                if (type.Equals(TileType.Normal))
                {
                    NavMeshSurface surface = tileObj.GetComponent<NavMeshSurface>();
                    m_walkableSurfaces.Add( surface);

                }
                

            }
        }
    }
    */


}
