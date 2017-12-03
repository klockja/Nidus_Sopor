using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMoveDown : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector2.MoveTowards (transform.position, new Vector2 (0, -24), 1 * Time.deltaTime);

		if (transform.position.y < -12) {
			transform.position = new Vector2 (0,35);

		}
	}
}


