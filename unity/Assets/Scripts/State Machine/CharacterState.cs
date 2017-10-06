using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState {

	protected CharacterStateMachine m_machine;

	public CharacterState(CharacterStateMachine machine)
	{
		m_machine = machine;
	}

	public virtual void OnEnter()
	{
	}

	public virtual void OnExit()
	{
	}

	public virtual void Update()
	{
	}

	public virtual void FixedUpdate()
	{
		
	}

	public virtual void DesireMove(Vector2 movementDirection)
	{

	}

	public virtual void DesireSneak()
	{

	}

	public virtual void DesireShoot()
	{

	}


}
