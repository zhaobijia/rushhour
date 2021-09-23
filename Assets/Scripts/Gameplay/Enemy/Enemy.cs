using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Character))]
public class Enemy : MonoBehaviour
{
    
    public int enemyId;
    NavMeshAgent m_agent;
    Character m_character;

    GameManager m_gameManager;
    Character m_player; //get from gamemanager
    bool hasDrop;
    //Detecting
    [Header("Detecting")]
    [SerializeField] float m_sightRange, m_attackRange;
    [SerializeField] LayerMask m_playerMask;
    bool m_playerInSightRange, m_playerInAttackRange;
    //Chasing
    [Header("Chasing:")]

    //Attacking
    [Header("Attacking:")]
    [SerializeField] float m_timeBetweenAttacks;
    [SerializeField] ProjectileEmitter m_emitter;
    [SerializeField] float m_damagePower;
    bool isPlayingAttackingAnim;

    //Drop
    [Header(" Item Drop:")]
    [SerializeField] EnemyDrop m_drop;
    [SerializeField] CharacterIcon m_dropIcon;
    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_character = GetComponent<Character>();
        m_gameManager = FindObjectOfType<GameManager>();
        m_player = m_gameManager.MainChar;
        m_dropIcon = GetComponentInChildren<CharacterIcon>();

        m_dropIcon.gameObject.SetActive(false);
    }
    private void Update()
    {
        //playerInSightRange= Physics.CheckSphere(transform.position, m_sightRange);
        if (!m_character.isDead)
        {
            m_playerInAttackRange = Physics.CheckSphere(transform.position, m_attackRange, m_playerMask);

            if (m_playerInAttackRange)
            {
                
                //stop and attack
                AttackPlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        //m_agent.SetDestination(m_player.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, m_player.transform.position, 1f*Time.deltaTime);
        //
        transform.rotation = Quaternion.LookRotation(m_player.transform.position - transform.position);
    }

    private void AttackPlayer()
    {
        //play attack animation at at certain frame call damage player
        //add attacking timer
        if (!isPlayingAttackingAnim)
        {
            Debug.Log("Attack player anim?");
            isPlayingAttackingAnim = true;
            m_character.m_Animator.SetTrigger("Attack");
        }
      
    }
    public void EndOfAttackingAnim()
    {
        isPlayingAttackingAnim = false;
    }

    public void DamagePlayer()
    {
        Debug.Log("damageplayer");
        // check again
        m_playerInAttackRange = Physics.CheckSphere(transform.position, m_attackRange, m_playerMask);
        m_player.TakeDamage(m_damagePower);
    }

    public void TakeDamage(float _damage)
    {
        Debug.Log("take damage : " + this.name);
        m_character.TakeDamage(_damage);

        if (m_character.isDead)
        {
            //1. enemy dead animation
            Debug.Log("enemy dead");
            //2. ragdoll
            RagdollOnDeath();
            //3. dropreward
            DropReward();
        }
    }

    void RagdollOnDeath()
    {

        // transform.position -= new Vector3(0,0.5f,0);
        // transform.Rotate(80f, 0, 0);  
        // GetComponent<NavMeshAgent>().enabled = false;
        //GetComponent<Collider>().enabled = false;
        m_character.ragdollController.TurnOnRagdoll();
    }

    public void DropReward()
    {
        //show ui
        //ui button calls grabdrop unction of enemydrop instance
        m_dropIcon.gameObject.SetActive(true);
        EnemyDrop drop = GetComponent<EnemyDrop>();
        drop.enabled = true;
        drop.InitializeInteractables();

    }
}
