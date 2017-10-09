using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : CharacterState {

	private float _shotTime = 0.25f;
	GameObject player = GameObject.Find ("Player");

	private CharacterState _previousState;
	public ShootState(CharacterStateMachine machine, CharacterState previousState):base(machine)
	{
		_previousState = previousState;

		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePosition = new Vector2 (player.transform.position.x, player.transform.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePosition, (mousePosition - firePosition), 100);
		if (hit.collider.tag == "Enemy") {
			// Deal Damage to them
			Debug.Log("hit Unke");
		}

		GameObject.Find ("Audio Collider").GetComponent<AudioDetectionScript> ().AudioRadius = new Vector3 (100, 100, 1);
		GameObject.Find ("Audio Collider").GetComponent<AudioDetectionScript> ().colliderRadius = 1f;

		Debug.DrawLine (firePosition, mousePosition);
		Debug.Log ("shoot");
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
