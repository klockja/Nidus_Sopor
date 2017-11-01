using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakState : MoveState {

	private float time;

	public SneakState(CharacterStateMachine machine) : base(machine)
	{
	}

	override public void DesireMove(Vector2 movement)
	{
		float speed = m_machine.Controller.SneakSpeed * m_machine.Controller.SpeedDecay;

		m_machine.Controller.Body.velocity = (movement * speed * Time.deltaTime);

		if (m_machine.Controller.Body.velocity != new Vector2 (0, 0)) {
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().AudioRadius = new Vector3 (5, 5, 1);
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().colliderRadius = 0.05f;

			if (time <= 0) 
			{
				m_machine.Controller.audioSource.PlayOneShot (m_machine.Controller.walkingSound);
				m_machine.Controller.StartCoroutine (Wait(1f));
			}
		}	else {
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().AudioRadius = new Vector3 (0, 0, 1);
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().colliderRadius = 0;
		}

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
