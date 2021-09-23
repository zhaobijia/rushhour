using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : Interactable
{
    public int dropedEnemyId;
    GameManager gameManager;



    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        OnEngage += GrabDrop;
        OnInteract += DropAlert;
    }
    public void GrabDrop()
    {
        //player pick up this drop
        //call at  ui click
        

        //gamme manager check if this drop 
        if (gameManager.CheckQuest(dropedEnemyId))
        {
            //if check true then grab the drop from ground
            //succeed grab

        }
        else
        {
            //play animation for fail grab
        }
        //destroy enemy gameobject and this icon ui

    }

    public void DropAlert()
    {
        string alertMessage = "Press E to Grab";
        gameManager.uiManager.ShowAlert(alertMessage);
    }

    public override void DebugInteract()
    {
        base.DebugInteract();
        Debug.Log("enemy drop interact?");

    }
}
