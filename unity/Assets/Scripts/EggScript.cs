using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			Destroy (gameObject);
			GameObject.Find ("managementObject").GetComponent<GameManagement> ().isbackward = true;
			Debug.Log (GameObject.Find ("managementObject").GetComponent<GameManagement> ().isbackward);
		}
	}
}
