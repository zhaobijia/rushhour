using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_enemyPrefabs;
   // public Dictionary<int, Enemy> m_enemyDict = new Dictionary<int, Enemy>();
    public Dictionary<int, GameObject> m_enemyPrefabDict = new Dictionary<int, GameObject>();

    private void Awake()
    {
        InitializeEnemyDictionary();
    }
    void InitializeEnemyDictionary()
    {
        foreach(GameObject enemyPrefab in m_enemyPrefabs)
        {
            Enemy enemy = enemyPrefab.GetComponent<Enemy>();
            if (enemy != null)
            {
                // m_enemyDict.Add(ene.m_id, ene.m_enemy);
                m_enemyPrefabDict.Add(enemy.enemyId, enemyPrefab);
            }
        }
    }

    public GameObject GetEnemyPrefabById(int id)
    {
        if (m_enemyPrefabDict.ContainsKey(id))
        {
            return m_enemyPrefabDict[id];
        }
        else
        {
            Debug.LogError("there is no matching Enemy!");

            return m_enemyPrefabDict[0];
        }
    }

}

