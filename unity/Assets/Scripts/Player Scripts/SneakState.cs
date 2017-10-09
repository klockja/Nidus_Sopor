using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakState : MoveState {

	public SneakState(CharacterStateMachine machine) : base(machine)
	{
	}

	override public void DesireMove(Vector2 movement)
	{
		float speed = m_machine.Controller.SneakSpeed * m_machine.Controller.SpeedDecay;

		m_machine.Controller.Body.velocity = (movement * speed * Time.deltaTime);

		if (m_machine.Controller.Body.velocity != new Vector2 (0, 0)) {
			GameObject.Find ("Audio Collider").GetComponent<AudioDetectionScript> ().AudioRadius = new Vector3 (5, 5, 1);
			GameObject.Find ("Audio Collider").GetComponent<AudioDetectionScript> ().colliderRadius = 0.05f;
		}	else {
			GameObject.Find ("Audio Collider").GetComponent<AudioDetectionScript> ().AudioRadius = new Vector3 (0, 0, 1);
			GameObject.Find ("Audio Collider").GetComponent<AudioDetectionScript> ().colliderRadius = 0;
		}
	}

	override public void DesireSneak()
	{
		m_machine.CurrentState = new MoveState(m_machine);
	}
}
