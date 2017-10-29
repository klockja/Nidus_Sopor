using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractState : CharacterState {

	private float _throwTime = 0.25f;
	GameObject player = GameObject.Find ("Player");
	GameObject _rock;
	private float speed = 5;



	private CharacterState _previousState;
	public DistractState(CharacterStateMachine machine, CharacterState previousState, GameObject Rock):base(machine)
	{
		_previousState = previousState;

		if (GameObject.Find ("GameManager").GetComponent<GameManagement> ().rockCount > 0) 
		{

			Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
			Vector2 throwPosition = new Vector2 (player.transform.position.x, player.transform.position.y);
			Vector2 direction = mousePosition - throwPosition;
			direction.Normalize ();
			_rock = GameObject.Instantiate (Rock, throwPosition, player.transform.rotation) as GameObject;
			_rock.GetComponent<Rigidbody2D> ().velocity = direction * speed;

			//_rock.GetComponent<Rigidbody2D> ().AddForce (direction*speed, ForceMode2D.Impulse);
			machine.Controller.rockNum -= 1;
		}
	}

	override public void Update()
	{
		base.Update();
		_throwTime -= Time.deltaTime;
		if(_throwTime <= 0.0f)
		{
			m_machine.CurrentState = _previousState;
		}
	}

}