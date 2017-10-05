using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakState : MovementState {

	public SneakState(CharacterStateMachine machine) : base(machine)
	{
	}

	override public void DesireMovement(Vector2 movement)
	{
		float speed = m_machine.Controller.SneakSpeed * m_machine.Controller.SpeedDecay;

		m_machine.Controller.Body.velocity = (movement * speed * Time.deltaTime);
	}

	override public void DesireSneakToggle()
	{
		m_machine.CurrentState = new MovementState(m_machine);
	}
}
