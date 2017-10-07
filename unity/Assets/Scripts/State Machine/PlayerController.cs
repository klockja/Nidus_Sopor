using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public bool gamePaused = false;

//	private Vector2 position; //The player's position in the world. Used for convienence.


	public GameObject Rock; 


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
		m_stateMachine.CurrentState = new MoveState(m_stateMachine);
	}

	void Start () 
	{
		
	}

	void Update ()
	{
		Debug.Log (m_stateMachine.CurrentState);

		if (gamePaused == false)
		{
			m_stateMachine.Update();

			float horizontal = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical");

			Vector2 movementDirection = new Vector2(horizontal, vertical);
//			movementDirection.Normalize();
			m_stateMachine.CurrentState.DesireMove(movementDirection);


			if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift))
			{
				Debug.Log ("Player Entered Sneak Mode");
				m_stateMachine.CurrentState.DesireSneak ();
			}

			if ((Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift)))
			{
				Debug.Log ("Player Exited Sneak Mode");
				m_stateMachine.CurrentState = new MoveState (m_stateMachine);
			}

			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				m_stateMachine.CurrentState.DesireShoot();
			}

			if(Input.GetKeyDown(KeyCode.Mouse1))
			{
				
				m_stateMachine.CurrentState.DesireThrowRock(Rock);
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
