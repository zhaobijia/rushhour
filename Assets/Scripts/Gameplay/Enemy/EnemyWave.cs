using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    //attach on spawn tile
    
    public float m_spawnTimeInBetween = 2.0f;

    List<GameObject> m_enemyWaveList = new List<GameObject>();
    int m_waveAmount;

    EnemyManager m_enemyManager;
    GameObject m_enemyPrefab;

    private void Awake()
    {
        m_enemyManager = FindObjectOfType<EnemyManager>();
    }
    public void StartWave(List<Vector3> _positions, int _enemyId, int _waveAmount)
    {
        
        m_enemyPrefab = m_enemyManager.GetEnemyPrefabById(_enemyId);
        m_waveAmount = _waveAmount;

        foreach(Vector3 pos in _positions)
        {
            StartCoroutine(GenerateWaveAtPos(pos));
        }
    }
    void Spawn(Vector3 pos)
    {
        //instantiate enemy gameobject on tile
        GameObject enemyObj = Instantiate(m_enemyPrefab, pos, Quaternion.identity);
        Debug.Log(enemyObj);
        m_enemyWaveList.Add(enemyObj);

    }

    IEnumerator GenerateWaveAtPos(Vector3 pos)
    {

        int amount = m_waveAmount;

        while (amount>0)
        {
            Spawn(pos);
            amount--;
            yield return new WaitForSeconds(m_spawnTimeInBetween);

        }

        yield break;//does it work???
    }

}
