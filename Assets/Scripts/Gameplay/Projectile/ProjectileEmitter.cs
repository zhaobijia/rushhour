using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEmitter : MonoBehaviour
{
    [Header("Position: ")]
    [SerializeField] Transform weaponTransform;

    [Header("Bullet Projectile: ")]
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] Vector3 m_scale = Vector3.one; //scale of bullet

    [Header("Shooting: ")]
    [SerializeField] float m_speed = 1.0f; //speed of bullet
    [SerializeField] float m_acceleration = 1.0f;    //bullet
    [SerializeField] float m_timeToLive = 1.0f;
    

    [SerializeField] int m_bulletPoolSize = 15;

    public float timeBetweenShots = 0.1f;

    [Header("Damage")]
    [SerializeField] public int m_damage;

    public bool isSplitted;//perk


    Vector3 m_velocity;
    Transform m_target;
    GameManager m_gamemanager;
    Projectile m_bullet;
    Queue<Projectile> m_bulletPool;


    bool isShooting;

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        
    }



    void Initialize()
    {
        m_gamemanager = FindObjectOfType<GameManager>();

       
        m_bulletPrefab.transform.localScale = m_scale;
        m_bulletPool = new Queue<Projectile>();


    }

    void UpdateVelocityDir()
    {
        m_velocity = m_gamemanager.MainChar.m_currentDir*m_speed;
    }

    void AddVelocity(Projectile bullet)
    {
        Rigidbody bullet_rb = bullet.m_rigidbody;
        Vector3 dir = m_velocity.normalized;
        bullet_rb.AddForce(dir *  m_acceleration);
        bullet_rb.velocity = m_velocity;

    }

    public void StopFire()
    {
        

    }

    public void Fire()
    {

        UpdateVelocityDir();
        if (!isSplitted)
        {

            //check pool:
            if (m_bulletPool.Count < m_bulletPoolSize)
            {

                //add new instantiated bullets into bullet pool
                
                GameObject bullet_obj = Instantiate(m_bulletPrefab, weaponTransform.position, Quaternion.identity);
                
                bullet_obj.transform.parent = null;
                m_bullet = bullet_obj.GetComponent<Projectile>();
                m_bullet.SetDamage(m_damage);

            }
            else
            {
                //recycle bullets from pool
                m_bullet = m_bulletPool.Dequeue();
                m_bullet.transform.position = weaponTransform.position;


            }

            m_bullet.Spawn(m_timeToLive);
            m_bulletPool.Enqueue(m_bullet);
            AddVelocity(m_bullet);
        }
        else
        {
            //Split();
        }




    }




}
