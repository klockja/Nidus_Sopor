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
	static private bool playerDetected = false;
	GameObject CharacterDetected;

	private Vector2 m_PushDirection;
	private float m_PushTime;

	[Header ("Enemy Statistics")]

	[SerializeField][Range (0f, 250f)]
	private float MaxHealth;
	[SerializeField][Range (0f, 250f)]
	private float CurrentHealth;
	[SerializeField][Range (0f, 10f)]
	private float walkSpeed;
	[SerializeField][Range (0f, 10f)]
	private float runSpeed;

	[Header ("Enemy's Behavior")]
	public Behavior SetBehavior;
	public enum Behavior {Patrols, Sleeps, Wanders}
	public bool isPatrolling;
	public bool isSleeping;
	public bool isWandering;
	public bool isPursuing;

	[SerializeField]
	public List<Vector2> patrolPattern;
	[SerializeField]
	private Vector2 lastPatrolPosition;
	[SerializeField]
	private int lastPatrolPatternOrder;

	public float patrolWaitTime;
	public float hurtWaitTime;
	public bool isWaiting;

	public Transform pathHolder;

	void OnDrawGizmos()
	{
		Vector2 startPosition = pathHolder.GetChild (0).position;
		Vector2 previousPosition = startPosition;
		foreach (Transform waypoint in pathHolder)
		{
			Gizmos.DrawIcon (waypoint.position, "check-x-gizmo.png", true);
			Gizmos.DrawLine (previousPosition, waypoint.position);
//			Gizmos.DrawCube (waypoint.position, new Vector2 (.5f,.5f));
			previousPosition = waypoint.position;
		}
		Gizmos.DrawLine (previousPosition, startPosition);
	}

	void Awake ()
	{
//		anim = GetComponentInChildren <Animator> (); // Gets the animator of the enemy
		m_Body = GetComponent <Rigidbody2D> ();
		AttackableEnemy = GetComponentInChildren <AttackableEnemy> (); // Gets the AttackableEnemy script

		AttackableEnemy.SetMaxHealth (MaxHealth);
	}

	// Use this for initialization
	void Start () 
	{
		Vector2[] waypoints = new Vector2[pathHolder.childCount];
		for (int i = 0; i < waypoints.Length; i++)
		{
			waypoints [i] = pathHolder.GetChild (i).position;
		}

		StartCoroutine (FollowPath (waypoints));
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

	IEnumerator FollowPath(Vector2[] waypoints)
	{
		transform.position = waypoints [0];

		int targetWaypointIndex = 1;
		Vector3 targetWaypoint = waypoints [targetWaypointIndex];

		while (true)
		{
			transform.position = Vector2.MoveTowards (transform.position, targetWaypoint, walkSpeed * Time.deltaTime);
			if (transform.position == targetWaypoint)
			{
				targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
				targetWaypoint = waypoints [targetWaypointIndex];
				yield return new WaitForSeconds (patrolWaitTime);
			}
			yield return null;
		}
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

	void Die()
	{
		Destroy (gameObject);
	}
}
