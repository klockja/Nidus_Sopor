using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour {


	public bool isPaused;
	public bool goingBackwards;
	public float bulletCount;
	public float rockCount;
	public AudioSource musicPlayer;
	public AudioClip backgroundMusic;

	void Awake() {
		bulletCount = 6;
		rockCount = 10;
		DontDestroyOnLoad (gameObject);
	}
	// Use this for initialization
	void Start () {
		//ispaused = false;
		musicPlayer.PlayOneShot(backgroundMusic);

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (ispaused);
		//Debug.Log(isbackward);
	}
}
