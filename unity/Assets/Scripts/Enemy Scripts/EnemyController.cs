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

	public Animator anim; // The Animator of the enemy

	public AttackableEnemy AttackableEnemy;
	public AILerp AILerp;

	public bool canAttack;
	public bool canBeHurt;
	public bool canMove = true;

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
	public float walkSpeed;
	[SerializeField][Range (0f, 10f)]
	public float runSpeed;

	[Header("Sight")]
	public Transform EnemyHead;
	public LayerMask targetMask;
	public LayerMask obstacleMask;

	[SerializeField]
	public bool playerSensed = false;
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
		anim = GetComponentInChildren <Animator> ();

		AttackableEnemy = GetComponentInChildren <AttackableEnemy> (); // Gets the AttackableEnemy script
		AttackableEnemy.SetMaxHealth (MaxHealth);
		AILerp = GetComponent <AILerp> ();

		canMove = true;
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
		if (Input.GetKeyDown (KeyCode.O))
		{
			StopAllCoroutines ();
			m_stateMachine.CurrentState = new EnemyPatrolState (m_stateMachine);
		}

		currentHealth = AttackableEnemy.GetHealth ();
		if (currentHealth <= 0 && canMove)
		{
			StopAllCoroutines ();
			AILerp.canMove = false;
			canMove = false;
			audioSource.PlayOneShot (takeDamage);
			anim.SetBool ("isDead", true);
			StartCoroutine (Die ());
		}

		if ((gamePaused == false && canMove) || (currentHealth > 0 && canMove))
		{
			//if the player can be sensed, the player is detected, then pursue

			if (playerSensed)
			{
				playerDetected = true;
				m_stateMachine.CurrentState.OnExit ();
				audioSource.PlayOneShot (growl);
				StartCoroutine (Pursue ());
				LastSightingSpot = detectedTransform.position;
			} //if the player can't be sensed anymore, but the player is detected, look around for a few seconds
			else if (playerSensed == false && playerDetected == true)
			{
				StopCoroutine (Pursue ());
				StartCoroutine (Search (LastSightingSpot)); //while player isn't sensed, search for a few seconds, then give up and return
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
		if (gamePaused == false)
		{
			UpdateDirection ();
		}
	}

	void UpdateDirection()
	{
		Vector2 newPosition = transform.position;
		if (newPosition.normalized != lastPosition.normalized)
		{
			anim.SetBool ("isMoving", true);
		} else
		{
			anim.SetBool ("isMoving", false);
		}

		directionFacing = (newPosition - lastPosition);
		directionFacing.Normalize ();
		if (directionFacing.y >= 0.7f)
		{
			EnemyHead.rotation = Quaternion.Euler (EnemyHead.eulerAngles.x, EnemyHead.eulerAngles.y, 0f); 
			anim.SetFloat ("input_x", 0);
			anim.SetFloat ("input_y", 1);
		}
		else if (directionFacing.y <= -0.7f)
		{
			EnemyHead.rotation = Quaternion.Euler (EnemyHead.eulerAngles.x, EnemyHead.eulerAngles.y, 180f);
			anim.SetFloat ("input_x", 0);
			anim.SetFloat ("input_y", -1);
		}
		else if (directionFacing.x < 0f)
		{
			EnemyHead.rotation = Quaternion.Euler (EnemyHead.eulerAngles.x, EnemyHead.eulerAngles.y, 90f);
			anim.SetFloat ("input_x", -1);
			anim.SetFloat ("input_y", 0);
		}
		else if (directionFacing.x > 0f)
		{
			EnemyHead.rotation = Quaternion.Euler (EnemyHead.eulerAngles.x, EnemyHead.eulerAngles.y, 270f); 
			anim.SetFloat ("input_x", 1);
			anim.SetFloat ("input_y", 0);
		}
		lastPosition = transform.position;
	}

	public IEnumerator Pursue()
	{
		if (detectedTransform != null)
		{
			Debug.Log ("Unke is pursuing the player");
			anim.SetBool ("playerDetected", true);
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
		AILerp.target = null;
		if (position.x - m_Body.position.x <= 1 || position.y - m_Body.position.y <= 1)
		{
			AILerp.target = null;
			Debug.Log ("Unke got near the last sighting of the player.");
			yield return new WaitForSeconds (Random.Range (1f, 2f));
			m_Body.position = Vector2.MoveTowards (transform.position, new Vector2 (m_Body.position.x + 1, m_Body.position.x + Random.Range (0, 1)), runSpeed * Time.deltaTime);
			yield return new WaitForSeconds (Random.Range (1f, 2f));
			m_Body.position = Vector2.MoveTowards (transform.position, new Vector2 (m_Body.position.x + Random.Range (0, 1), m_Body.position.x + Random.Range (0, 1)), runSpeed * Time.deltaTime);
			yield return new WaitForSeconds (Random.Range (1f, 2f));
			m_Body.position = Vector2.MoveTowards (transform.position, new Vector2 (m_Body.position.x + Random.Range (0, 1), m_Body.position.x + Random.Range (0, 1)), runSpeed * Time.deltaTime);
			yield return new WaitForSeconds (Random.Range (1f, 2f));
			m_Body.position = Vector2.MoveTowards (transform.position, new Vector2 (m_Body.position.x + Random.Range (0, 1), m_Body.position.x + Random.Range (0, 1)), runSpeed * Time.deltaTime);
			yield return new WaitForSeconds (Random.Range (1f, 2f));
			playerDetected = false;
		}
		yield return new WaitForSeconds (3f);
		Debug.Log ("Unke lost the player");
		playerDetected = false;
		StartCoroutine (Return ());
		yield return null;
	}

	public IEnumerator Return()
	{
		Debug.Log ("Unke returning to original position");
		AILerp.target = originalPosition;
		AILerp.speed = walkSpeed;
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

	public IEnumerator Die()
	{
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
		yield return null;
	}
}