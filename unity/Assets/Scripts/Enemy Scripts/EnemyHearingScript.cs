using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHearingScript : MonoBehaviour 
{
	EnemyController _enemyController;
	// Use this for initialization
	void Start () 
	{
		_enemyController = GetComponentInParent <EnemyController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Sound")
		{
			if (col.gameObject.transform.parent.name == "Player")
			{
				Debug.Log ("Unke detected the player.");
				Debug.Log ("The audio position was: " + col.transform.parent.transform);
				_enemyController.lastSensedPlayerPosition = col.transform.parent.transform.position;
			} 
			else
			{
				Debug.Log ("Detected Audio Collider from: " + col.transform.parent.transform);
				Debug.Log ("The audio position was: " + col.transform.parent.transform);
				_enemyController.lastSensedPlayerPosition = col.transform.position;
			}

			if (_enemyController.playerSensed == false)
			{
				StartCoroutine(_enemyController.PlaySound (_enemyController.growl, 0f));
				_enemyController.playerSensed = true;
			}
		}
	}
}
