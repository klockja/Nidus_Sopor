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
		if (col.gameObject.name == "Scent")
		{
			_enemyController.CharacterDetected = col.gameObject;
			_enemyController.playerDetected = true;
		}
	}
}
