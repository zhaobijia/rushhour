using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    PlayerController m_playerController;
    Character m_character;

    Vector3 m_weaponPosition;

    ProjectileEmitter m_emitter;

    [SerializeField] Perk m_accumulatedPerk; //initialize it to a base Perk, add the new perk on to this and then apply it to current setting.

    Perk m_choosingAddOnPerk;

    private void Awake()
    {
        m_playerController = GetComponent<PlayerController>();
        m_character = GetComponent<Character>();

        m_emitter = GetComponentInChildren<ProjectileEmitter>();

        m_accumulatedPerk = GetComponent<Perk>();//base perk
    }


    public void AddCurrentPerk(Perk newPerk)
    {
        m_accumulatedPerk.AddToPerk(newPerk);
        ApplyPerkToCharacter();
    }


    void ApplyPerkToCharacter()
    {
        //bullet

        //damage
        m_emitter.m_damage = m_accumulatedPerk.m_damageBase;
        //moving speed
        m_character.m_MoveSpeedMultiplier = m_accumulatedPerk.m_movingSpeed;
        //slow enemy ?

        //health
        m_character.m_health = m_accumulatedPerk.m_healthUpperLimit;
        //price
        
        

    }

    
    

    public void ChangeWeapon()
    {
        //update the perks
    }
}
