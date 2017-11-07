using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTriggerScript : MonoBehaviour {

	[SerializeField]
	private Vector2 destination;

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.name == "Player")
		{
			col.transform.position = destination;
		}

	}
}
