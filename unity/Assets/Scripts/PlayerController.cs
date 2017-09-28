using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{

	private Vector2 position; //The player's position in the world. Used for convienence.

	private Rigidbody2D m_Body;

	[Header ("Player Statistics")]

	public bool canMove = true;
	private bool isAttacking = false;
	[SerializeField]
	private bool isSneaking = true;
	[SerializeField]
	private float currentSpeed = 2; //The current speed the player has while moving.
	[SerializeField]
	private float sneakSpeed = 2; //The default speed the player sneaks at.
	[SerializeField]
	private float runSpeed = 5; //The default speed the player runs at.

	void Awake()
	{
		m_Body = gameObject.GetComponentInChildren <Rigidbody2D> (); //Get's the Rigidbody of the character.
	}

	void Start () 
	{
		
	}

	void Update () 
	{
		PlayerMovement ();	
	}

	private void PlayerMovement() //Handles the player's movement.
	{
		position = transform.position; //Sets the position variable as the players position.

		if (canMove && isAttacking == false)
		{
			if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift))
			{
				if (isSneaking == true)
				{
					isSneaking = false;
				} 
				else
				{
					isSneaking = true;
				}
				
			}

			if (isSneaking == true)
			{
				currentSpeed = sneakSpeed;
			} 
			else
			{
				currentSpeed = runSpeed;
			}

			//up right
			if (Input.GetAxisRaw ("Horizontal") > 0 && Input.GetAxisRaw ("Vertical") > 0)
			{
//				transform.Translate (new Vector2 (1, 1) * (currentSpeed - (currentSpeed / 4)) * Time.deltaTime);
				m_Body.position += (new Vector2 (1, 1) * (currentSpeed - (currentSpeed / 4)) * Time.deltaTime);
//				anim.SetBool ("isMoving", true);
			}
			//up left
			else if (Input.GetAxisRaw ("Horizontal") < 0 && Input.GetAxisRaw ("Vertical") > 0)
			{
//				transform.Translate (new Vector2 (-1, 1) * (currentSpeed - (currentSpeed / 4)) * Time.deltaTime);
				m_Body.position += (new Vector2 (-1, 1) * (currentSpeed - (currentSpeed / 4)) * Time.deltaTime);
//				anim.SetBool ("isMoving", true);
			}
			//down right
			else if (Input.GetAxisRaw ("Horizontal") > 0 && Input.GetAxisRaw ("Vertical") < 0)
			{
//				transform.Translate (new Vector2 (1, -1) * (currentSpeed - (currentSpeed / 4)) * Time.deltaTime);
				m_Body.position += (new Vector2 (1, -1) * (currentSpeed - (currentSpeed / 4)) * Time.deltaTime);
//				anim.SetBool ("isMoving", true);
			}
			//down left
			else if (Input.GetAxisRaw ("Horizontal") < 0 && Input.GetAxisRaw ("Vertical") < 0)
			{
//				transform.Translate (new Vector2 (-1, -1) * (currentSpeed - (currentSpeed / 4)) * Time.deltaTime);
				m_Body.position += (new Vector2 (-1, -1) * (currentSpeed - (currentSpeed / 4)) * Time.deltaTime);
//				anim.SetBool ("isMoving", true);
			}
			//up
			else if (Input.GetAxisRaw ("Vertical") > 0)
			{
//				transform.Translate (Vector2.up * currentSpeed * Time.deltaTime);
				m_Body.position += (Vector2.up * currentSpeed * Time.deltaTime);

//				anim.SetBool ("isMoving", true);
//				anim.SetFloat ("input_y", 1);
//				anim.SetFloat ("input_x", 0);
//
//				facingUp = true;
//				facingDown = false;
//				facingLeft = false;
//				facingRight = false;
			}
			//down
			else if (Input.GetAxisRaw ("Vertical") < 0)
			{
//				transform.Translate (Vector2.down * currentSpeed * Time.deltaTime);
				m_Body.position += (Vector2.down * currentSpeed * Time.deltaTime);

//				anim.SetBool ("isMoving", true);
//				anim.SetFloat ("input_y", -1);
//				anim.SetFloat ("input_x", 0);
//
//				facingUp = false;
//				facingDown = true;
//				facingLeft = false;
//				facingRight = false;
			}
			//left
			else if (Input.GetAxisRaw ("Horizontal") < 0)
			{
//				transform.Translate (Vector2.left * currentSpeed * Time.deltaTime);
				m_Body.position += (Vector2.left * currentSpeed * Time.deltaTime);

//				anim.SetBool ("isMoving", true);
//				anim.SetFloat ("input_x", -1);
//				anim.SetFloat ("input_y", 0);
//
//				facingUp = false;
//				facingDown = false;
//				facingLeft = true;
//				facingRight = false;
			}
			//right
			else if (Input.GetAxisRaw ("Horizontal") > 0)
			{
//				transform.Translate (Vector2.right * currentSpeed * Time.deltaTime);
				m_Body.position += (Vector2.right * currentSpeed * Time.deltaTime);

//				anim.SetBool ("isMoving", true);
//				anim.SetFloat ("input_x", 1);
//				anim.SetFloat ("input_y", 0);
//
//				facingUp = false;
//				facingDown = false;
//				facingLeft = false;
//				facingRight = true;
//			} else {
//				anim.SetBool ("isMoving", false);
//			}
			}
		}
	}
}
