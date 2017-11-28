using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellTriggerScript : MonoBehaviour 
{
	public float destructionDelay;

	void Update()
	{
		destructionDelay = destructionDelay - Time.deltaTime;

		if (destructionDelay <= 0)
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag != "Untagged" && col.tag != "Sound" && col.tag != "Player")
		{
			Debug.Log ("Collided with " + col.name);
			Destroy (gameObject);
		}
	}
}
