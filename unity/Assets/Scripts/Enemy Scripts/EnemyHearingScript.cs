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
			Debug.Log ("Detected Audio Collider from: " + col.transform.parent.transform);
			_enemyController.detectedTransform = col.transform.parent.transform;
			_enemyController.playerSensed = true;
		}
	}
}
