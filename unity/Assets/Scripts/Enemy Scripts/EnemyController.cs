using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour 
{
	public bool gamePaused;

	[Header ("Enemy Sounds")]
	public AudioSource audioSource;
	public AudioClip takeDamage;
	public AudioClip growl;
	public AudioClip move;

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
	[SerializeField]
	private Vector2 upHeadPosition, downHeadPosition, leftHeadPosition, rightHeadPosition;
	[SerializeField][Range (0f, 10f)]
	public float walkSpeed;
	[SerializeField][Range (0f, 10f)]
	public float runSpeed;

	[Header("Sight")]
	public Transform EnemyHead;
	public LayerMask targetMask;
	public LayerMask obstacleMask;

	[SerializeField]

	[Header ("Enemy's Behavior")]
	private Vector2 originalPosition;
	public Behavior SetBehavior;
	public enum Behavior {Patrols, Sleeps}

	public float chaseWaitTime;
	public float patrolWaitTime;
	public Transform PatrolPath;
	Vector2[] waypoints;
	private int waypointIndex;
	public Vector2 lastSensedPlayerPosition;

	private int currentWaypoint;
	public bool isWaiting;

	public bool playerSensed;

	void Awake ()
	{
		m_Body = GetComponent <Rigidbody2D> ();
		audioSource = GetComponent <AudioSource> ();
		anim = GetComponentInChildren <Animator> ();

		AttackableEnemy = GetComponentInChildren <AttackableEnemy> (); // Gets the AttackableEnemy script
		AttackableEnemy.SetMaxHealth (MaxHealth);
		AILerp = GetComponent <AILerp> ();

		canMove = true;

		waypoints = new Vector2[PatrolPath.childCount];
		for (int i = 0; i < waypoints.Length; i++)
		{
			waypoints [i] = PatrolPath.GetChild (i).position;
		}

		if (waypoints.Length >= 1)
		{
			transform.position = waypoints [0];
		}
		originalPosition = transform.position;
		AILerp.destination = originalPosition;

	}

	void OnDrawGizmos()
	{
		if (PatrolPath != null)
		{
			Vector2 startPosition = PatrolPath.GetChild (0).position;
			Vector2 previousPosition = startPosition;
			foreach (Transform waypoint in PatrolPath)
			{
				Gizmos.DrawIcon (waypoint.position, "x-gizmo.png", true);
				Gizmos.DrawLine (previousPosition, waypoint.position);
				//            Gizmos.DrawCube (waypoint.position, new Vector2 (.5f,.5f));
				//            Gizmos.DrawSphere (waypoint.position, .5f);
				previousPosition = waypoint.position;
			}
			Gizmos.DrawLine (previousPosition, startPosition);
		}
	}

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
		gamePaused = GameManagement.Instance.isPaused;
		currentHealth = AttackableEnemy.GetHealth ();
		if (currentHealth <= 0 && canMove)
		{
			if (takeDamage != null)
			{
				audioSource.PlayOneShot (takeDamage);
			}
			StartCoroutine ("Die");
			canMove = false;
		}

		if (GameManagement.Instance.isPlayerDead)
		{
			canMove = false;
			AILerp.destination = transform.position;
			AILerp.canMove = false;
		}

		if (gamePaused == true)
		{
			canMove = false;
			AILerp.canMove = false;
		}
		if (gamePaused == false)
		{
			canMove = true;
			AILerp.canMove = true;
		}
		//Decide what to do
		if ((gamePaused == false && canMove && currentHealth > 0))
		{
			if (playerSensed && GameManagement.Instance.isPlayerDead == false)
			{
				ChasePlayer ();
			} 
			else if (playerSensed == false && GameManagement.Instance.isPlayerDead == false)
			{
				if (SetBehavior == Behavior.Patrols)
				{
					Patrol ();
				}

				if (SetBehavior == Behavior.Sleeps)
				{
					
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.O))
		{
			AILerp.destination = new Vector3 (5, 5, transform.position.z);
		}
	}

	void ChasePlayer()
	{
		Vector2 lastSensedDeltaPos = lastSensedPlayerPosition - new Vector2(transform.position.x, transform.position.y);
		if (lastSensedDeltaPos.sqrMagnitude > 1f)
		{
			AILerp.destination = lastSensedPlayerPosition;
			AILerp.speed = runSpeed;
		}
		if (lastSensedDeltaPos.sqrMagnitude < .25f)
		{
			playerSensed = false;
			AILerp.speed = walkSpeed;
		}
	}

	void Patrol()
	{
		if ((Vector2)transform.position != waypoints [waypointIndex])
		{
			AILerp.destination = waypoints [waypointIndex];
			AILerp.speed = walkSpeed;
		} 
		else
		{
			if(waypointIndex == waypoints.Length - 1)
			{
				waypointIndex = 0;
			}
			else
			{
				waypointIndex += 1;
			}
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
		if (Vector2.Distance (newPosition, lastPosition) > 0)
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
			EnemyHead.localPosition = upHeadPosition;
			anim.SetFloat ("input_x", 0);
			anim.SetFloat ("input_y", 1);
		}
		else if (directionFacing.y <= -0.7f)
		{
			EnemyHead.rotation = Quaternion.Euler (EnemyHead.eulerAngles.x, EnemyHead.eulerAngles.y, 180f);
			EnemyHead.localPosition = downHeadPosition;
			anim.SetFloat ("input_x", 0);
			anim.SetFloat ("input_y", -1);
		}
		else if (directionFacing.x < 0f)
		{
			EnemyHead.rotation = Quaternion.Euler (EnemyHead.eulerAngles.x, EnemyHead.eulerAngles.y, 90f);
			EnemyHead.localPosition = leftHeadPosition;
			anim.SetFloat ("input_x", -1);
			anim.SetFloat ("input_y", 0);
		}
		else if (directionFacing.x > 0f)
		{
			EnemyHead.rotation = Quaternion.Euler (EnemyHead.eulerAngles.x, EnemyHead.eulerAngles.y, 270f); 
			EnemyHead.localPosition = rightHeadPosition;
			anim.SetFloat ("input_x", 1);
			anim.SetFloat ("input_y", 0);
		}
		lastPosition = transform.position;
	}
		
	public IEnumerator PlaySound(AudioClip clip, float delay)
	{
		yield return new WaitForSeconds (delay);
		audioSource.PlayOneShot (clip);
		yield return null;
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
		AILerp.canMove = false;
		canMove = false;
		anim.SetBool ("isDead", true);
		yield return new WaitForSeconds (0.5f);
		Destroy (gameObject);
		yield return null;
	}
}