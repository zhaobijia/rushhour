using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] float m_interactRadius = 3f;
    protected GameManager m_gameManager;
    Transform m_playerTransform;
    bool m_isInteracting;

    public delegate void OnInteractEventHandler();

    public event OnInteractEventHandler OnInteract;
    public event OnInteractEventHandler OnEngage;

    
    private void Awake()
    {
        InitializeInteractables();
    }
    private void Start()
    {
        OnInteract += DebugInteract;
    }
    private void Update()
    {
        float distance = Vector3.Distance(m_playerTransform.position, transform.position);
        if (distance < m_interactRadius)
        {
            if (!m_isInteracting)
            {
                m_isInteracting = true;
                OnInteract.Invoke();
            }
        }
        else
        {
            m_isInteracting = false;
        }
    }

    

    public void InitializeInteractables()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_playerTransform = m_gameManager.mainChar.transform;
    }

    public virtual void DebugInteract()
    {
        Debug.Log("interact?");

        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_interactRadius);
    }
}
