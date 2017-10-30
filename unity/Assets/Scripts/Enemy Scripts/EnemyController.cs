using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
	public bool gamePaused;

	[Header ("Enemy Sounds")]
	public AudioSource audioSource;
	public AudioClip takeDamage;
	public AudioClip growl;
	public AudioClip move;
	public AudioClip attack;
	public AudioClip sleep;

	private Rigidbody2D m_Body; //The enemy's rigidbody
	public Rigidbody2D M_Body
	{
		get
		{
			return m_Body;
		}
	}

	//	public Animator anim; // The Animator of the enemy

	public AttackableEnemy AttackableEnemy;
	public AILerp AILerp;

	public bool canAttack;
	public bool canBeHurt;
	public bool canMove;

	private Vector2 directionFacing;
	private Vector2 m_PushDirection;
	private float m_PushTime;

	[Header ("Enemy Statistics")]
	[Header ("Health")]
	[SerializeField][Range (0f, 250f)]
	private float maxHealth;
	public float MaxHealth
	{
		get
		{
			return maxHealth;
		}
	}
	[SerializeField][Range (0f, 250f)]
	private float currentHealth;
	public float CurrentHealth
	{
		get
		{
			return currentHealth;
		}
		set
		{
			currentHealth = CurrentHealth;
		}
	}

	[Header("Movement")]
	private Vector2 lastPosition;
	private Vector2 newPosition;
	[SerializeField][Range (0f, 10f)]
	private float walkSpeed;
	public float WalkSpeed
	{
		get
		{
			return walkSpeed;
		}
	}
	[SerializeField][Range (0f, 10f)]
	private float runSpeed;
	public float RunSpeed
	{
		get
		{
			return runSpeed;
		}
	}

	[Header("Sight")]
	public Transform EnemyHead;
	public LayerMask targetMask;
	public LayerMask obstacleMask;

	[SerializeField]
	public bool playerDetected = false;
	public bool playerInSight = false;
//	public bool isSearching = false;
	public Transform detectedTransform;
	public Vector2 LastSightingSpot;

	[Header ("Enemy's Behavior")]
	public Transform originalPosition;
	public Behavior SetBehavior;
	public enum Behavior {Patrols, Sleeps, Wanders}
	public Transform PatrolPath;
	public bool isPatrolling;
	public bool isSleeping;
	public bool isWandering;
	public bool isPursuing;

	public float patrolWaitTime;
	public float hurtWaitTime;
	public bool isWaiting;

	private EnemyStateMachine m_stateMachine;
	public EnemyStateMachine StateMachine
	{
		get
		{
			return m_stateMachine;
		}
	}

	void Awake ()
	{
		m_stateMachine = new EnemyStateMachine(this);
		//		anim = GetComponentInChildren <Animator> (); // Gets the animator of the enemy
		m_Body = GetComponent <Rigidbody2D> ();
		audioSource = GetComponent <AudioSource> ();

		AttackableEnemy = GetComponentInChildren <AttackableEnemy> (); // Gets the AttackableEnemy script
		AttackableEnemy.SetMaxHealth (MaxHealth);
		AILerp = GetComponent <AILerp> ();
	}

	void OnDrawGizmos()
	{
		Vector2 startPosition = PatrolPath.GetChild (0).position;
		Vector2 previousPosition = startPosition;
		foreach (Transform waypoint in PatrolPath)
		{
			Gizmos.DrawIcon (waypoint.position, "x-gizmo.png", true);
			Gizmos.DrawLine (previousPosition, waypoint.position);
			//			Gizmos.DrawCube (waypoint.position, new Vector2 (.5f,.5f));
			//			Gizmos.DrawSphere (waypoint.position, .5f);
			previousPosition = waypoint.position;
		}
		Gizmos.DrawLine (previousPosition, startPosition);
	}

	// Use this for initialization
	void Start () 
	{
		if (SetBehavior == Behavior.Patrols && PatrolPath != null)
		{
			m_stateMachine.CurrentState = new EnemyPatrolState (m_stateMachine);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (gamePaused == false)
		{
			currentHealth = AttackableEnemy.GetHealth ();
			if (currentHealth <= 0)
			{
				StopAllCoroutines ();
				canMove = false;
				audioSource.clip = takeDamage;
				audioSource.Play ();
			}

			if (playerInSight && detectedTransform != null)
			{
				Debug.Log ("The player is in sight of the Unke");
//				m_stateMachine.CurrentState.OnExit ();
				if (playerDetected != true)
				{
//					m_stateMachine.CurrentState = new EnemyPursueState (m_stateMachine);
					audioSource.PlayOneShot (growl);
					StartCoroutine (Pursue ());
					playerDetected = true;
				}
				LastSightingSpot = detectedTransform.position;
			}

			if (playerInSight == false && detectedTransform != null && playerDetected == true)
			{
				Debug.Log ("Unke lost sight of the player");
				StopCoroutine (Pursue ());
				StartCoroutine (Search (LastSightingSpot));
				detectedTransform = null;
			}
		}
	}

	void FixedUpdate ()
	{
		if (gamePaused == false)
		{

		}

	}

	void LateUpdate ()
	{
		UpdateDirection ();
	}

	void UpdateDirection()
	{
		Vector2 newPosition = transform.position;
		directionFacing = (newPosition - lastPosition);
		directionFacing.Normalize ();
		if (directionFacing.y >= 0.7f)
		{
			EnemyHead.rotation = Quaternion.Euler (EnemyHead.eulerAngles.x, EnemyHead.eulerAngles.y, 0f); 
		}
		else if (directionFacing.y <= -0.7f)
		{
			EnemyHead.rotation = Quaternion.Euler (EnemyHead.eulerAngles.x, EnemyHead.eulerAngles.y, 180f); 
		}
		else if (directionFacing.x < 0f)
		{
			EnemyHead.rotation = Quaternion.Euler (EnemyHead.eulerAngles.x, EnemyHead.eulerAngles.y, 90f); 
		}
		else if (directionFacing.x > 0f)
		{
			EnemyHead.rotation = Quaternion.Euler (EnemyHead.eulerAngles.x, EnemyHead.eulerAngles.y, 270f); 
		}
		lastPosition = transform.position;
	}

	public IEnumerator Pursue()
	{
		if (detectedTransform != null)
		{
			Debug.Log ("Unke is pursuing the player");
	//		m_Body.position = Vector2.MoveTowards (transform.position, position, RunSpeed * Time.deltaTime);
			AILerp.speed = runSpeed;
			AILerp.target = detectedTransform;
			AILerp.TrySearchPath ();
	//		AILerp.
	//		yield return new WaitForSeconds(.5f);
		}
		yield return null;
	}

	public IEnumerator Search(Vector2 position)
	{
		Debug.Log ("Unke is searching for player");
		if (position.x - m_Body.position.x <= 1 || position.y - m_Body.position.y <= 1)
		{
			AILerp.target = null;
			Debug.Log ("Unke got near the last sighting of the player.");
			yield return new WaitForSeconds (Random.Range (1f, 2f));
			m_Body.position = Vector2.MoveTowards (transform.position, new Vector2 (m_Body.position.x + 1, m_Body.position.x + Random.Range (0, 1)), RunSpeed * Time.deltaTime);
			yield return new WaitForSeconds (Random.Range (1f, 2f));
			m_Body.position = Vector2.MoveTowards (transform.position, new Vector2 (m_Body.position.x + Random.Range (0, 1), m_Body.position.x + Random.Range (0, 1)), RunSpeed * Time.deltaTime);
			yield return new WaitForSeconds (Random.Range (1f, 2f));
			m_Body.position = Vector2.MoveTowards (transform.position, new Vector2 (m_Body.position.x + Random.Range (0, 1), m_Body.position.x + Random.Range (0, 1)), RunSpeed * Time.deltaTime);
			yield return new WaitForSeconds (Random.Range (1f, 2f));
			m_Body.position = Vector2.MoveTowards (transform.position, new Vector2 (m_Body.position.x + Random.Range (0, 1), m_Body.position.x + Random.Range (0, 1)), RunSpeed * Time.deltaTime);
			yield return new WaitForSeconds (Random.Range (1f, 2f));
			playerDetected = false;
		}
		yield return new WaitForSeconds (3f);
		Debug.Log ("Unke lost the player");
		StartCoroutine (Return ()); 
		yield return null;
	}

	public IEnumerator Return()
	{
		Debug.Log ("Unke returning to original position");
		AILerp.target = originalPosition;
		AILerp.SearchPath ();

		if (originalPosition.position.x - transform.position.x < 1)
		{
			AILerp.target = null;
			Debug.Log ("Unke has returned to original position");
			m_stateMachine.CurrentState = new EnemyPatrolState (m_stateMachine);
			m_stateMachine.CurrentState.OnEnter ();
			yield return null;
		}
	}

	public IEnumerator WaitForSeconds (float seconds)
	{
		yield return new WaitForSeconds (seconds);
		Debug.Log (gameObject.name + " is done waiting.");
		yield return null;
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal) 
		{
			angleInDegrees -= transform.eulerAngles.z;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
	}

	public void StartChildCoroutine(IEnumerator coroutineMethod)
	{
		StartCoroutine (coroutineMethod);
	}

	public void StopChildCoroutine(IEnumerator coroutineMethod)
	{
		StopCoroutine (coroutineMethod);
	}

	void Die()
	{
		Destroy (gameObject);
	}
}