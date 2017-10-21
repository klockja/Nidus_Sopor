using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingRockScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponentInChildren<AudioDetectionScript> ().AudioRadius = new Vector3 (10, 10, 1);
		gameObject.GetComponentInChildren<AudioDetectionScript> ().colliderRadius = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponentInChildren<AudioDetectionScript> ().AudioRadius = new Vector3 (10, 10, 1);
		gameObject.GetComponentInChildren<AudioDetectionScript> ().colliderRadius = 0.1f;
	}
}
