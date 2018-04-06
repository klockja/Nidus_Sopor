using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmellScript : MonoBehaviour 
{
	EnemyController _enemyController;
	// Use this for initialization
	void Start () 
	{
		_enemyController = GetComponentInParent <EnemyController>();
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "PlayerSmell")
		{
			_enemyController.lastSensedPlayerPosition = col.transform.position;
			if (_enemyController.playerSensed == false)
			{
				StartCoroutine(_enemyController.PlaySound (_enemyController.growl, 0f));
				_enemyController.playerSensed = true;
			}
		}
	}
}
