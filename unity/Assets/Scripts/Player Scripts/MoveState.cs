using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : CharacterState { 

	public MoveState(CharacterStateMachine machine):base(machine)
	{
	}

	override public void DesireMove(Vector2 movement)
	{
		float speed = m_machine.Controller.RunSpeed * m_machine.Controller.SpeedDecay;
		m_machine.Controller.Body.velocity = (movement * speed * Time.deltaTime);

		if (m_machine.Controller.Body.velocity != new Vector2 (0, 0)) {
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().AudioRadius = new Vector3 (10, 10, 1);
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().colliderRadius = 0.1f;

			m_machine.Controller.audio.PlayOneShot (m_machine.Controller.runningSound);

		} else {
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().AudioRadius = new Vector3 (0, 0, 1);
			m_machine.Controller.gameObject.GetComponentInChildren<AudioDetectionScript> ().colliderRadius = 0;
		}

		m_machine.Controller.fireLine.enabled = false;
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
