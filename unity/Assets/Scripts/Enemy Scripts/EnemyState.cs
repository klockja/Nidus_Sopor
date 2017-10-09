using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState {

	protected EnemyStateMachine m_machine;

	public EnemyState(EnemyStateMachine machine)
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

	public virtual void Patrol()
	{

	}

}
