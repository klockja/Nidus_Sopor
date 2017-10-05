using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public bool gamePaused = false;

//	private Vector2 position; //The player's position in the world. Used for convienence.

	private Rigidbody2D m_Body; //The player's rigidbody
	public Rigidbody2D Body
	{
		get
		{
			return m_Body;
		}
	}

	[Header ("Player Statistics")]

	public bool canMove = true;

	[SerializeField]
	private float sneakSpeed = 1; //The default speed the player sneaks at.
	public float SneakSpeed
	{
		get
		{
			return sneakSpeed;
		}
	}

	[SerializeField]
	private float runSpeed = 4; //The default speed the player runs at.
	public float RunSpeed
	{
		get
		{
			return runSpeed;
		}
	}

	private float m_speedDecay = 1.0f;
    public float SpeedDecay
    {
        get
        {
            return m_speedDecay;
        }
    }

	private CharacterStateMachine m_stateMachine;
	public CharacterStateMachine StateMachine
	{
		get
		{
			return m_stateMachine;
		}
	}

	void Awake ()
	{
		m_Body = gameObject.GetComponentInChildren <Rigidbody2D> (); //Gets the Rigidbody of the character.

		m_stateMachine = new CharacterStateMachine(this);
		m_stateMachine.CurrentState = new MovementState(m_stateMachine);
	}

	void Start () 
	{
		
	}

	void Update ()
	{
		if (gamePaused == false)
		{
			m_stateMachine.Update();

			float horizontal = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical");

			Vector2 movementDirection = new Vector2(horizontal, vertical);
			movementDirection.Normalize();
			m_stateMachine.CurrentState.DesireMovement(movementDirection);

			if(Input.GetKeyDown(KeyCode.LeftShift))
			{
				m_stateMachine.CurrentState.DesireSneakToggle();
			}	

			if(Input.GetKeyDown(KeyCode.Space))
			{
				m_stateMachine.CurrentState.DesireShoot();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "TallGrass")
		{
			m_speedDecay = 0.5f;
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.tag == "TallGrass")
		{
			m_speedDecay = 1.0f;
		}
	}

	void FixedUpdate () 
	{
		if (gamePaused == false)
		{
			m_stateMachine.FixedUpdate();
		}
	}
}
