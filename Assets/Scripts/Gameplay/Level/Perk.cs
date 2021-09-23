using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perk : MonoBehaviour
{

    public int perkId;

    [Header("Attacking")]
 //   public float m_shootingSpeed;
    public int m_bulletAmount;
    public int m_damageBase;
 //   public float m_damageRange;
  //  [Range(0,1)] public float m_criticalHit; //range 0-1
 //   [Range(0, 1)] public float m_penetration;
    public bool m_slowEnemy;
    //  public bool m_constantDamage;
    //   public float m_constantDamageBase;


    [Header("Defence")]
 //   public float m_resistanceMultiplier;
    public float m_movingSpeed;
    public float m_healthUpperLimit;

    [Header("Drop")]
    //  public float m_enemyDropMultiplier;
    //  public bool m_enemyDropCoin;
    public int m_addedLevelPrice;

    //manager


    public void AddToPerk(Perk _perk)
    {
        m_bulletAmount += m_bulletAmount;
        m_damageBase += _perk.m_damageBase;
        m_slowEnemy = _perk.m_slowEnemy;
        m_movingSpeed += _perk.m_movingSpeed;
        m_healthUpperLimit +=_perk.m_healthUpperLimit;
        m_addedLevelPrice += _perk.m_addedLevelPrice;
    }

}
