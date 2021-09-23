using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButton : Interactable
{
    public int _enemyToSpawn;
    public int _amountToSpawn;

    bool _readyToEngage = false;
    private void Start()
    {
        OnInteract += DebugInteract;
        OnEngage += SpawnEnemy;
    }

    //on interact,
    //1. alert panel shows alert message: "press E to Interact", at the same time set interactable ready for press E
    //
    //2. After press E, call corresponding function (? how to recognize this 
    //   eg. if it is enemy button 

    //problem"
    //how to set each spawner to the right enemy
    
    //call on press this button

    public void AboutToEngageThisButton()
    {
        _readyToEngage = true;
    }

    public void SpawnEnemy()
    {
        m_gameManager.TriggerEnemyWave(_enemyToSpawn, _amountToSpawn);
        _readyToEngage = false;
    }

    
    public override void DebugInteract()
    {
        base.DebugInteract();
        Debug.Log("enemy button interact?");

    }
}
