using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : CharacterState 
{

	private float _shotTime = 0.25f;
	GameObject player = GameObject.Find ("Player");

	private CharacterState _previousState;

	public ShootState(CharacterStateMachine machine, CharacterState previousState):base(machine)
	{
		_previousState = previousState;

//		machine.Controller.anim.SetBool ("isShooting", true);
//		Debug.Log (machine.Controller.anim.GetBool ("isShooting"));

		if (Camera.main.ScreenToWorldPoint(machine.Controller.mousePos).y > machine.Controller.transform.localPosition.y) 
		{
			machine.Controller.anim.SetFloat ("input_y", 1);
			machine.Controller.anim.SetFloat ("input_x", 0);
			machine.Controller.anim.SetBool ("isShooting", true);
		}

		if (Camera.main.ScreenToWorldPoint(machine.Controller.mousePos).y < machine.Controller.transform.localPosition.y) 
		{
			machine.Controller.anim.SetFloat ("input_y", -1);
			machine.Controller.anim.SetFloat ("input_x", 0);
			machine.Controller.anim.SetBool ("isShooting", true);
		}

		if (Camera.main.ScreenToWorldPoint(machine.Controller.mousePos).x > machine.Controller.transform.localPosition.x && Camera.main.ScreenToWorldPoint(machine.Controller.mousePos).y <= machine.Controller.transform.localPosition.y+3 && Camera.main.ScreenToWorldPoint(machine.Controller.mousePos).y >= machine.Controller.transform.localPosition.y-3)
		{
			machine.Controller.anim.SetFloat ("input_y", 0);
			machine.Controller.anim.SetFloat ("input_x", 1);
			machine.Controller.anim.SetBool ("isShooting", true);
		}

		if (Camera.main.ScreenToWorldPoint(machine.Controller.mousePos).x < machine.Controller.transform.localPosition.x && Camera.main.ScreenToWorldPoint(machine.Controller.mousePos).y <= machine.Controller.transform.localPosition.y+3 && Camera.main.ScreenToWorldPoint(machine.Controller.mousePos).y >= machine.Controller.transform.localPosition.y-3)
		{
			machine.Controller.anim.SetFloat ("input_y", 0);
			machine.Controller.anim.SetFloat ("input_x", -1);
			machine.Controller.anim.SetBool ("isShooting", true);
		}

		if (machine.Controller.bulletNum > 0)
		{
			machine.Controller.audioSource.PlayOneShot (machine.Controller.shootingSound);
			machine.Controller.bulletNum -= 1;
			Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
			Vector2 firePosition = new Vector2 (player.transform.position.x, (player.transform.position.y + 0.8f));
			RaycastHit2D hit = Physics2D.Raycast (firePosition, (mousePosition - firePosition), 1000, machine.Controller.shootableMask);

			//GameObject.Find ("Audio Collider").GetComponent<AudioDetectionScript> ().AudioRadius = new Vector3 (100, 100, 1);
			GameObject.Find ("Player").GetComponentInChildren<AudioDetectionScript> ().AudioRadius = new Vector3 (6f, 6f, 1);
			GameObject.Find ("Player").GetComponentInChildren<AudioDetectionScript> ().colliderRadius = 1f;
			m_machine.Controller.gameObject.GetComponentInChildren<CircleCollider2D> ().enabled = true;

			//Debug.DrawLine (firePosition, mousePosition);
			machine.Controller.fireLine.SetPosition (0, firePosition);
			machine.Controller.fireLine.SetPosition (1, hit.point);
			machine.Controller.fireLine.enabled = true;
//			m_machine.Controller.gameObject.GetComponentInChildren<CircleCollider2D> ().enabled = false;
			//Debug.Log (hit.collider.gameObject);
			//if (hit.collider.tag == "Player") 
			{
				//	Debug.Log ("Shot myself");
				if (hit.collider.tag == "Enemy")
				{
					hit.collider.GetComponentInParent <AttackableEnemy> ().Damage (100);
					// Deal Damage to them
					//Debug.Log ("hit Unke");
				} else
				{
				
					//Debug.Log (hit.point);
				}
			}

		}
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

	override public void OnExit()
	{
		m_machine.Controller.anim.SetBool ("isShooting", false);
	}

}
