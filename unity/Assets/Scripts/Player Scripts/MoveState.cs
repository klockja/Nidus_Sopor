using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : CharacterState { 

	private float timer;
	private Vector2 oldPosition;

	public MoveState(CharacterStateMachine machine):base(machine)
	{
	}

	override public void DesireMove(Vector2 movement)
	{
		oldPosition = m_machine.Controller.Body.position;
		float speed = m_machine.Controller.RunSpeed * m_machine.Controller.SpeedDecay;
//		m_machine.Controller.Body.velocity = (movement * speed * Time.deltaTime);
		//up right
		if (Input.GetAxisRaw ("Horizontal") > 0 && Input.GetAxisRaw ("Vertical") > 0)
		{
			m_machine.Controller.Body.position += (new Vector2 (1, 1) * (speed - (speed / 4)) * Time.deltaTime);
			m_machine.Controller.anim.SetBool ("isMoving", true);
			m_machine.Controller.anim.SetFloat ("input_y", 1);
			m_machine.Controller.anim.SetFloat ("input_x", 0);
		} 
		//up left
		else if (Input.GetAxisRaw ("Horizontal") < 0 && Input.GetAxisRaw ("Vertical") > 0)
		{
			m_machine.Controller.Body.position += (new Vector2 (-1, 1) * (speed - (speed / 4)) * Time.deltaTime);
			m_machine.Controller.anim.SetBool ("isMoving", true);
			m_machine.Controller.anim.SetFloat ("input_y", 1);
			m_machine.Controller.anim.SetFloat ("input_x", 0);
		} 
		//down right
		else if (Input.GetAxisRaw ("Horizontal") > 0 && Input.GetAxisRaw ("Vertical") < 0)
		{
			m_machine.Controller.Body.position += (new Vector2 (1, -1) * (speed - (speed/4)) * Time.deltaTime);
			m_machine.Controller.anim.SetBool ("isMoving", true);
			m_machine.Controller.anim.SetFloat ("input_y", -1);
			m_machine.Controller.anim.SetFloat ("input_x", 0);
		}
		//down left
		else if (Input.GetAxisRaw ("Horizontal") < 0 && Input.GetAxisRaw ("Vertical") < 0)
		{
			m_machine.Controller.Body.position += (new Vector2 (-1, -1) * (speed - (speed/4)) * Time.deltaTime);
			m_machine.Controller.anim.SetBool ("isMoving", true);
			m_machine.Controller.anim.SetFloat ("input_y", -1);
			m_machine.Controller.anim.SetFloat ("input_x", 0);
		}
		//up
		else if (Input.GetAxisRaw ("Vertical") > 0) 
		{
			m_machine.Controller.Body.position += (Vector2.up * speed * Time.deltaTime);
			m_machine.Controller.anim.SetBool ("isMoving", true);
			m_machine.Controller.anim.SetFloat ("input_y", 1);
			m_machine.Controller.anim.SetFloat ("input_x", 0);
		}
		//down
		else if (Input.GetAxisRaw ("Vertical") < 0) {
			m_machine.Controller.Body.position += (Vector2.down * speed * Time.deltaTime);
			m_machine.Controller.anim.SetBool ("isMoving", true);
			m_machine.Controller.anim.SetFloat ("input_y", -1);
			m_machine.Controller.anim.SetFloat ("input_x", 0);
		}
		//left
		else if (Input.GetAxisRaw ("Horizontal") < 0) {
			m_machine.Controller.Body.position += (Vector2.left * speed * Time.deltaTime);
			m_machine.Controller.anim.SetBool ("isMoving", true);
			m_machine.Controller.anim.SetFloat ("input_y", 0);
			m_machine.Controller.anim.SetFloat ("input_x", -1);
		}
		//right
		else if (Input.GetAxisRaw ("Horizontal") > 0) {
			m_machine.Controller.Body.position += (Vector2.right * speed * Time.deltaTime);
			m_machine.Controller.anim.SetBool ("isMoving", true);
			m_machine.Controller.anim.SetFloat ("input_y", 0);
			m_machine.Controller.anim.SetFloat ("input_x", 1);
		} else {
			m_machine.Controller.anim.SetBool ("isMoving", false);
		}


//		if (m_machine.Controller.Body.velocity.magnitude > 1)
//		{
//			m_machine.Controller.anim.SetBool ("isMoving", true);
//		} else
//		{
//			m_machine.Controller.anim.SetBool ("isMoving", false);
//		}

		if (m_machine.Controller.Body.position != oldPosition) {
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().AudioRadius = new Vector3 (1, 1, 1);
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().colliderRadius = 2f;

			if (timer <= 0) {
				m_machine.Controller.audioSource.PlayOneShot (m_machine.Controller.runningSound);
				timer = .25f;
				m_machine.Controller.PlaySound (m_machine.Controller.runningSound, 2f);
			}

		} else {
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().AudioRadius = new Vector3 (0, 0, 0);
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().colliderRadius = 0;
		}

		m_machine.Controller.fireLine.enabled = false;
		timer = timer - Time.deltaTime;
		oldPosition = m_machine.Controller.Body.position;
	}

	override public void DesireSneak()
	{
		m_machine.CurrentState = new SneakState(m_machine);
	}

	override public void DesireShoot()
	{
		m_machine.CurrentState = new ShootState(m_machine, this);
	}

	override public void DesireThrowRock(GameObject Rock)
	{
		m_machine.CurrentState = new DistractState(m_machine, this, Rock);
	}
		
}
