using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]

public class Projectile : MonoBehaviour
{
    public Rigidbody m_rigidbody;
    public SphereCollider m_collider;
    
    int m_damage;

    [SerializeField] Mesh m_bulletMesh;
    [SerializeField] ProjectileStats m_projectileStats;

    string m_shaderName;
    Color m_color;
    Texture m_texture;
    Material m_material;
    MeshFilter m_meshFilter;
    MeshRenderer m_meshRenderer;

    float m_spawnTime;
    float m_liveTime;
    float m_allowedTime;
    private void Awake()
    {
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_rigidbody = GetComponent<Rigidbody>();

        m_shaderName = m_projectileStats.m_shaderName;
        m_color = m_projectileStats.m_color;
        m_texture = m_projectileStats.m_texture;
        m_material = m_projectileStats.m_material;
        m_spawnTime = m_projectileStats.m_spawnTime;
        m_liveTime = m_projectileStats.m_liveTime;
        m_allowedTime = m_projectileStats.m_allowedTime;
    }
    private void Start()
    {
        Initialize();
    }
    private void Update()
    {
        if (m_liveTime < m_allowedTime)
        {
            CalculateLiveTime();
        }
        else
        {
            Recycle();
        }
    }

    void Initialize()
    {
        m_meshFilter.mesh = m_bulletMesh;
        m_material = new Material(Shader.Find(m_shaderName));
        m_material.SetColor("_Color", m_color);
        m_material.SetTexture("_MainTex", m_texture);
        m_meshRenderer.material = m_material;
      
    }

    public void SetDamage(int _damage)
    {
        m_damage = _damage;
        
    }
    public void Spawn(float timeToLive)
    {
        gameObject.SetActive(true);
        m_allowedTime = timeToLive;
        m_spawnTime = Time.time;
        
    }

    void CalculateLiveTime()
    {
        m_liveTime = Time.time - m_spawnTime;
    }

    void Recycle()
    {
        m_liveTime = 0;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
           // Debug.Log(m_damage);
            enemy.TakeDamage(m_damage);
        }
    }
}
