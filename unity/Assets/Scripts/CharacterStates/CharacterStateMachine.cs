using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine 
{
    private PlayerController _playerController;
    public PlayerController Controller
    {
        get
        {
            return _playerController;
        }
    }

    private CharacterState _currentState;
    public CharacterState CurrentState
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

	public CharacterStateMachine(PlayerController playerController)
    {
        _playerController = playerController;
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
