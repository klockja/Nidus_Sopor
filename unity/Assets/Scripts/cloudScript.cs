using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudScript : MonoBehaviour {

	float timer = 90f;

	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector2 (transform.position.x + Random.Range (0f, 1f) * Time.deltaTime, transform.position.y); 

		if (timer <= 0)
		{
			Destroy (gameObject);
		}

		timer -= Time.deltaTime;
	}
}
