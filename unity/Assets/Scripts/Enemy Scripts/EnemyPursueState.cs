using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPursueState : EnemyState
{
	Transform player;

	public EnemyPursueState(EnemyStateMachine machine):base(machine)
	{
	}

	override public void OnEnter()
	{
		Debug.Log ("Entered Pursue State");
		Update ();
	}

	override public void Update()
	{
//		base.Update();
//		Debug.Log ("Update");
//		m_machine.Controller.transform.position = Vector2.MoveTowards (m_machine.Controller.transform.position, player.position, m_machine.Controller.RunSpeed * Time.deltaTime);
	}

	override public void FixedUpdate()
	{

	}

	override public void DesireMove(Vector2 movementDirection)
	{

	}

	override public void Patrol()
	{

	}

	override public void OnExit()
	{
		Debug.Log ("Exited Pursue State");
	}

}
