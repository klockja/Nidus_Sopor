using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : CharacterState { 

	public MovementState(CharacterStateMachine machine):base(machine)
	{
	}

	override public void DesireMovement(Vector2 movement)
	{
		float speed = m_machine.Controller.RunSpeed * m_machine.Controller.SpeedDecay;

		m_machine.Controller.Body.velocity = (movement * speed * Time.deltaTime);
	}

	override public void DesireSneakToggle()
	{
		m_machine.CurrentState = new SneakState(m_machine);
	}

	override public void DesireShoot()
	{
		m_machine.CurrentState = new AttackState(m_machine, this);
	}
}
