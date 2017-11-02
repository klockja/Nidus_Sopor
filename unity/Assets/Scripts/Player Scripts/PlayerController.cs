using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public Animator anim;

	public bool gamePaused = false;
	public bool hasEgg;
	public bool reloading;
	public float unusedBulletNum;
	public float bulletNum;
	public float rockNum;

	public float time;

	public LineRenderer fireLine;
	public float fireWidth = 0.1f;
	public float fireMaxLenght = 5f;

	public LayerMask shootableMask;

	// Audio Stuff
	public AudioSource audioSource;
	public AudioClip shootingSound;
	public AudioClip throwingSound;
	public AudioClip walkingSound;
	public AudioClip runningSound;
	public AudioClip dyingSound;


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
		GameObject.Find ("GameManager").GetComponent <GameManagement> ().isPaused = false;
		anim = GetComponentInChildren <Animator> ();
		m_Body = gameObject.GetComponent <Rigidbody2D> (); //Gets the Rigidbody of the character.
		audioSource = GetComponent <AudioSource>();

		m_stateMachine = new CharacterStateMachine(this);
		m_stateMachine.CurrentState = new MoveState(m_stateMachine);

		Vector3[] initFirePosition = new Vector3[2] { Vector3.zero, Vector3.zero };
		fireLine.SetPositions (initFirePosition); 
		fireLine.startWidth = fireWidth;
		fireLine.endWidth = fireWidth;

		bulletNum = GameObject.Find ("GameManager").GetComponent<GameManagement> ().bulletCount;
		rockNum = GameObject.Find ("GameManager").GetComponent<GameManagement> ().rockCount;
		unusedBulletNum = GameObject.Find ("GameManager").GetComponent<GameManagement> ().unusedBullet;

	}

	void Start () 
	{
		anim.SetFloat ("input_y", -1);
		anim.SetFloat ("input_x", 0);

		hasEgg = GameObject.Find ("GameManager").GetComponent<GameManagement> ().goingBackwards;
		if (hasEgg == false) 
		{
		} else if (hasEgg == true) 
		{
		}
	}

	void Update ()
	{
		//Debug.Log (m_stateMachine.CurrentState);
		gamePaused = GameObject.Find ("GameManager").GetComponent<GameManagement> ().isPaused;

		if (gamePaused == true)
			Body.velocity = Vector2.zero;

		if (gamePaused == false)
		{
			m_stateMachine.Update();

			float horizontal = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical");

			Vector2 movementDirection = new Vector2(horizontal, vertical);

			if (vertical > 0.7f)
			{
				anim.SetFloat ("input_y", 1);
				anim.SetFloat ("input_x", 0);
			}

			if (vertical < -0.7f)
			{
				anim.SetFloat ("input_y", -1);
				anim.SetFloat ("input_x", 0);
			}

			if (horizontal > 0.7f)
			{
				anim.SetFloat ("input_y", 0);
				anim.SetFloat ("input_x", 1);
			}

			if (horizontal < -0.7f)
			{
				anim.SetFloat ("input_y", 0);
				anim.SetFloat ("input_x", -1);
			}
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
				if (reloading == false) {
					m_stateMachine.CurrentState.DesireShoot ();
				}
			}

			if (Input.GetKeyDown (KeyCode.R)) 
			{
				Reload ();
			}

			if(Input.GetKeyDown(KeyCode.Mouse1))
			{
				if (GameObject.Find ("GameManager").GetComponent<GameManagement> ().rockCount > 0 && time == 0) 
				{
					m_stateMachine.CurrentState.DesireThrowRock (Rock);
					GameObject.Find ("GameManager").GetComponent<GameManagement> ().rockCount -= 1;
					StartCoroutine(Delay(2));
				}
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

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy") 
		{
			Debug.Log ("hit player");
			GameObject.Find ("GameManager").GetComponent<GameManagement> ().gameover = true;
			GameObject.Find ("GameManager").GetComponent<GameManagement> ().isPaused = true;
			GameObject.Find ("Canvas").GetComponent<ui> ().Panel8.SetActive (true);
		}
	}

	public IEnumerator Delay(float maxtime)
	{
		time = maxtime;
		while (time > 0) {
			Debug.Log (time);
			yield return new WaitForSeconds (1.0f);
			time--;
		}
	}

	public IEnumerator PlaySound(AudioClip clip, float delay)
	{
		yield return new WaitForSeconds (delay);
		audioSource.PlayOneShot (clip);
		yield return null;
	}

	void Reload ()
	{
		if (unusedBulletNum != 0 && bulletNum != 6) {
			while (bulletNum < 6 && unusedBulletNum != 0) {
				bulletNum += 1;
				unusedBulletNum -= 1;
			}
		} else
			Debug.Log ("Error with reoloading");
	}

	void FixedUpdate () 
	{
		if (gamePaused == false)
		{
			m_stateMachine.FixedUpdate();
		}
	}
}