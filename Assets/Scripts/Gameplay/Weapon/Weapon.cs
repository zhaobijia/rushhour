using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Melee,
    Range
}
public class Weapon : MonoBehaviour
{

    [SerializeField] ProjectileEmitter m_emitter;
    [HideInInspector]public float timeBetweenShots;

    private void Start()
    {
        timeBetweenShots = m_emitter.timeBetweenShots;
    }



    public void Shooting()
    {
        m_emitter.Fire();
    }

    public void StopShooting()
    {
        Debug.Log("stop shooting");

        //call at mouse button up
       
        //stop coroutine and stuff
        m_emitter.StopFire();
    }
}
