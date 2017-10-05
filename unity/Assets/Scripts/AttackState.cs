using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CharacterState {

	private float _shotTime = 0.25f;

	private CharacterState _previousState;
	public AttackState(CharacterStateMachine machine, CharacterState previousState):base(machine)
	{
		_previousState = previousState;
	}

	override public void Update()
	{
		base.Update();
		_shotTime -= Time.deltaTime;
		if(_shotTime <= 0.0f)
		{
			m_machine.CurrentState = _previousState;
		}
	}

}
