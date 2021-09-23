using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour
{
    private Character m_Character; // A reference to the ThirdPersonCharacter on the object
    Animator m_animator;
    private Vector3 m_Move;
    float h, v;
    bool dash;
    float m_fireTimer = 0f;
    Vector3 mousePos;
    Camera cam;
    Vector3 faceDir;
    Weapon m_currentWeapon;

    private void Start()
    {
        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<Character>();
        m_animator = GetComponent<Animator>();
        //the default weapon
        m_currentWeapon = GetComponentInChildren<Weapon>();

        cam = Camera.main;
    }


    private void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        mousePos = Input.mousePosition;
        m_Move = v * Vector3.forward + h * Vector3.right;
        //Fire 
        if (Input.GetMouseButton(0))
        {
            if (m_fireTimer < m_currentWeapon.timeBetweenShots)
            {
                m_fireTimer += Time.deltaTime;
            }
            else
            {
                m_currentWeapon.Shooting();
                m_animator.SetTrigger("Attack");
                m_fireTimer = 0f;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            //reset timer
            m_fireTimer = 0f;
            m_currentWeapon.StopShooting();
        }


        //dash -->space
        dash = Input.GetKeyDown(KeyCode.Space);
        if (dash)
        {
            m_Character.Dash(m_Move);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_animator.SetTrigger("Pickup");
        }

    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // we use world-relative directions in the case of no main camera
       
        m_Character.Move(m_Move);
        faceDir = GetFacingDir();
        m_Character.Rotation(faceDir,m_Move);

        
    }

    Vector3 GetFacingDir()
    {
        Vector3 dir = Vector3.zero;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            Transform tileTrans = hit.transform;

            dir = tileTrans.position - transform.position;
        }
        return dir;
    }

    void CollectEnemyDrop()
    {
        //quest list check
    }

}

