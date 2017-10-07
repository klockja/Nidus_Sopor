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
	}

	override public void DesireSneak()
	{
		m_machine.CurrentState = new MoveState(m_machine);
	}
}
