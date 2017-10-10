using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
	public bool gamePaused;

	private Rigidbody2D m_Body; //The enemy's rigidbody

//	public Animator anim; // The Animator of the enemy

	public AttackableEnemy AttackableEnemy;

	public bool canAttack;
	public bool canBeHurt;
	public bool canMove;

	[SerializeField]
	public bool playerDetected = false;
	public GameObject CharacterDetected;

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
//	private Vector3 previousHeadPosition;
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
	public GameObject EnemyHead;
	[Range(0, 100)]
	public float viewRadius;
	[Range(0, 360)]
	public float viewAngle;
	public LayerMask targetMask;
	public LayerMask obstacleMask;
	public List<Transform> visibleTargets = new List<Transform> ();

	[Header ("Enemy's Behavior")]
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
//		previousHeadPosition = EnemyHead.transform.position;
//		anim = GetComponentInChildren <Animator> (); // Gets the animator of the enemy
		m_Body = GetComponent <Rigidbody2D> ();
		AttackableEnemy = GetComponentInChildren <AttackableEnemy> (); // Gets the AttackableEnemy script

		AttackableEnemy.SetMaxHealth (MaxHealth);
	}

	void OnDrawGizmos()
	{
		Vector2 startPosition = PatrolPath.GetChild (0).position;
		Vector2 previousPosition = startPosition;
		foreach (Transform waypoint in PatrolPath)
		{
			Gizmos.DrawIcon (waypoint.position, "check-x-gizmo.png", true);
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
		StartCoroutine (FindTargetWithDelay(0.2f));

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
			UpdatePushTime ();
			UpdateHit ();

			CurrentHealth = AttackableEnemy.GetHealth ();
			if (AttackableEnemy.GetHealth () <= 0) 
			{
				canMove = false;
			}
			UpdateDirection ();

			if (playerDetected)
			{
				m_stateMachine.CurrentState.OnExit ();
				PursuePlayer ();
//				m_stateMachine.CurrentState = new EnemyPursueState(m_stateMachine, CharacterDetected.transform);
			}
		}
	}

	void FixedUpdate()
	{
		if (gamePaused == false)
		{
			
		}

	}

	void EnemyMovement()
	{
		if (isBeingPushed () == true) 
		{
			transform.Translate (m_PushDirection * Time.deltaTime);
			return;
		}
	}

	void UpdateDirection()
	{
//		Debug.Log (previousHeadPosition + " " + EnemyHead.transform.position);
//
//		Vector2 direction = Camera.main.ScreenToWorldPoint (Input.mousePosition) - EnemyHead.transform.position;
//		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
//		Quaternion rotation = Quaternion.AngleAxis (angle, Vector3.forward);
////		EnemyHead.transform.rotation = rotation;
//		EnemyHead.transform.rotation = Quaternion.Slerp (rotation, EnemyHead.transform.rotation, 2 * Time.deltaTime);
//		if (transform.position.x > previousPosition.x)
//		{
//			transform.rotation = rotation;
//		}
//
//		if (transform.position.x < previousPosition.x)
//		{
//			transform.rotation = rotation;		
//		}
//
//		if (transform.position.y > previousPosition.y)
//		{
//			transform.rotation = rotation;		
//		}
//
//		if (transform.position.y < previousPosition.y)
//		{
//			transform.rotation = rotation;
//		}
//		previousHeadPosition = EnemyHead.transform.position;
	}

	IEnumerator FindTargetWithDelay(float delay) 
	{
		while (true) {
			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		}
	}

	//Finds targets inside field of view not blocked by walls
	void FindVisibleTargets() 
	{
		visibleTargets.Clear();
		//Adds targets in view radius to an array
		Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(EnemyHead.transform.position, viewRadius, targetMask);
		//For every targetsInViewRadius it checks if they are inside the field of view
		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - EnemyHead.transform.position).normalized;
			if (Vector3.Angle(EnemyHead.transform.up, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance(EnemyHead.transform.position, target.position);
				//If line draw from object to target is not interrupted by wall, add target to list of visible targets
				if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) 
				{
					visibleTargets.Add(target);
					if (playerDetected == false)
					{
						CharacterDetected = target.gameObject;
						playerDetected = true;
//						m_stateMachine.CurrentState = new EnemyPursueState (m_stateMachine, target);
					}
				}
			}
		}
	}

	public void PursuePlayer()
	{
		transform.position = Vector2.MoveTowards (transform.position, CharacterDetected.transform.position, RunSpeed * Time.deltaTime);
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal) 
		{
			angleInDegrees -= transform.eulerAngles.z;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
	}

	void UpdateHit()
	{
//		anim.SetBool ("isHurt", isBeingPushed ());
	}

	void UpdatePushTime()
	{
		if (m_PushTime > 0) 
		{
			m_PushTime = m_PushTime - Time.deltaTime;
		}
	}

	public void PushCharacter(Vector2 pushDirection, float pushTime)
	{
		m_PushDirection = pushDirection;
		m_PushTime = pushTime;
	}

	public bool isBeingPushed()
	{
		return m_PushTime > 0;
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
