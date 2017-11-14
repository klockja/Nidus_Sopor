using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
			GameManagement.Instance.hasegg = true;
			GameManagement.Instance.goingBackwards = true;
			MusicManagerScript.Instance.musicPlayer.clip = MusicManagerScript.Instance.escape;
			MusicManagerScript.Instance.musicPlayer.Play();
			Destroy (gameObject);
			//GameManagement.Instance.goingBackwards = true;
			//Debug.Log (GameObject.Find ("managementObject").GetComponent<GameManagement> ().goingBackwards);
		}
	}
}
