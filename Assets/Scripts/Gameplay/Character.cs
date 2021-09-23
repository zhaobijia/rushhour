using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
	
	[SerializeField] float m_StationaryTurnSpeed = 1;
	[SerializeField] public float m_MoveSpeedMultiplier = 1f;
	[SerializeField] float m_DashDistance = 1f;

	public RagdollController ragdollController;


	Rigidbody m_Rigidbody;
	[HideInInspector] public Animator m_Animator;
	public Vector3 m_currentDir;
	CapsuleCollider m_Capsule;

	

	public float m_health;
	public float m_fullHealth;

	public bool isDead;
	void Start()
	{
		m_Animator = GetComponent<Animator>();
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Capsule = GetComponent<CapsuleCollider>();
		ragdollController = GetComponentInChildren<RagdollController>();


		//m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		m_currentDir = Vector3.forward;
		m_health = m_fullHealth;

		ragdollController.TurnOffRagdoll();
	}


    #region Movements
    public void Move(Vector3 move)
	{

		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (move.magnitude > 1f) move.Normalize();
		move = move * m_MoveSpeedMultiplier;

		//m_TurnAmount = Mathf.Atan2(move.x, move.z);// Returns the angle in radians whose Tan is y/x.
		m_Rigidbody.velocity = move;
		m_Animator.SetFloat("Speed", move.magnitude);
		
		
		
	}

	public void Rotation(Vector3 dir, Vector3 move)
	{
		//rotation default : 0,0,0
		if (dir != Vector3.zero)
		{
			dir = new Vector3(dir.x, 0, dir.z);
			m_currentDir = dir;
        }
        else if (move!=Vector3.zero)
        {
			dir = new Vector3(move.x, 0, move.z);
			m_currentDir = dir;
		}
        else
        {
			if (m_currentDir != Vector3.zero)
			{
				dir = m_currentDir;
            }
            else
            {
				dir = Vector3.forward; 
			}
        }
		Quaternion goalQuat = Quaternion.LookRotation(dir, Vector3.up);

		float turnSpeed = m_StationaryTurnSpeed;

		transform.rotation = Quaternion.Lerp(transform.rotation, goalQuat, turnSpeed * Time.deltaTime);

		//transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}

	public void Dash(Vector3 move)
    {
		if (move.magnitude > 1f) move.Normalize();
		

		//m_TurnAmount = Mathf.Atan2(move.x, move.z);// Returns the angle in radians whose Tan is y/x.
		m_Rigidbody.MovePosition(transform.position+move*m_DashDistance);
	}
    #endregion

    #region Damages

	public void TakeDamage(float _damage)
    {
		
        if (m_health > 0)
        {
			m_health -= _damage;
			m_Animator.SetTrigger("TakeDmg");
        }
        else
        {
			isDead = true;
			m_Animator.SetTrigger("Death");
			//at end frame of death turn on ragdoll
        }
    }

	
    #endregion


}

