using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour 
{
	private EnemyController _enemyController;
	public EnemyController Controller
	{
		get
		{
			return _enemyController;
		}
	}

	private EnemyState _currentState;
	public EnemyState CurrentState
	{
		get
		{
			return _currentState;
		}
		set
		{
			if(_currentState != null)
			{
				_currentState.OnExit();
			}
			_currentState = value;
			_currentState.OnEnter();
		}
	}

	public EnemyStateMachine(EnemyController enemyController)
	{
		_enemyController = enemyController;
	}

	public void Update()
	{
		if(_currentState != null)
		{
			_currentState.Update();
		}
	}

	public void FixedUpdate()
	{
		if(_currentState != null)
		{
			_currentState.FixedUpdate();
		}
	}



}

