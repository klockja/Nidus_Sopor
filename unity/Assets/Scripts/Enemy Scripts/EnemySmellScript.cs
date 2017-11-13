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

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "PlayerSmell")
		{
			_enemyController.playerSensed = true;
			_enemyController.detectedTransform = GameManagement.Instance.player.transform;
		}
	}
}
