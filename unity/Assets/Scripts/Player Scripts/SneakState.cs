using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakState : MoveState {

	private float time;
	private Vector2 oldPosition;

	public SneakState(CharacterStateMachine machine) : base(machine)
	{
	}

	override public void DesireMove(Vector2 movement)
	{
		oldPosition = m_machine.Controller.Body.position;
		float speed = m_machine.Controller.SneakSpeed * m_machine.Controller.SpeedDecay;

		if (m_machine.Controller.Body.position != oldPosition) {
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().AudioRadius = new Vector3 (.5f, .5f, 1);
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().colliderRadius = 0.5f;

			if (time <= 0) 
			{
				m_machine.Controller.audioSource.PlayOneShot (m_machine.Controller.walkingSound);
				m_machine.Controller.StartCoroutine (Wait(1f));
			}
		}	else {
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().AudioRadius = new Vector3 (0, 0, 1);
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().colliderRadius = 0;
		}

		if (Input.GetAxisRaw ("Horizontal") > 0 && Input.GetAxisRaw ("Vertical") > 0)
		{
			m_machine.Controller.Body.position += (new Vector2 (1, 1) * (speed - (speed / 4)) * Time.deltaTime);
			m_machine.Controller.anim.SetBool ("isMoving", true);
			m_machine.Controller.anim.SetFloat ("input_y", 1);
			m_machine.Controller.anim.SetFloat ("input_x", 0);
		} 
		else if (Input.GetAxisRaw ("Horizontal") < 0 && Input.GetAxisRaw ("Vertical") > 0)
		{
			m_machine.Controller.Body.position += (new Vector2 (-1, 1) * (speed - (speed / 4)) * Time.deltaTime);
			m_machine.Controller.anim.SetBool ("isMoving", true);
			m_machine.Controller.anim.SetFloat ("input_y", 1);
			m_machine.Controller.anim.SetFloat ("input_x", 0);
		} 
		else if (Input.GetAxisRaw ("Horizontal") > 0 && Input.GetAxisRaw ("Vertical") < 0)
		{
			m_machine.Controller.Body.position += (new Vector2 (1, -1) * (speed - (speed/4)) * Time.deltaTime);
			m_machine.Controller.anim.SetBool ("isMoving", true);
			m_machine.Controller.anim.SetFloat ("input_y", -1);
			m_machine.Controller.anim.SetFloat ("input_x", 0);
		}
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
		oldPosition = m_machine.Controller.Body.position;
		m_machine.Controller.fireLine.enabled = false;
	}

	override public void DesireSneak()
	{
		m_machine.CurrentState = new MoveState(m_machine);
		if (m_machine.Controller.anim.GetBool ("isSneaking") == false)
		{
			m_machine.Controller.anim.SetBool ("isSneaking", true);
		}
	}

	IEnumerator Wait(float maxtime)
	{
		time = maxtime;
		while (time > 0) 
		{
			Debug.Log (time);
			yield return new WaitForSeconds (1.0f);
			time--;
		}
	}

	override public void OnExit()
	{
		if (m_machine.Controller.anim.GetBool ("isSneaking") == true)
		{
			m_machine.Controller.anim.SetBool ("isSneaking", false);
		}
	}
}
