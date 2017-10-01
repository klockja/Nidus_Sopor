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

	public float hurtWaitTime;
	public bool isWaiting;

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
		patrolPattern.Add (Vector2.up);
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

	public void addToPatrolPattern(Vector2 direction)
	{
		patrolPattern.Add (direction);
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
