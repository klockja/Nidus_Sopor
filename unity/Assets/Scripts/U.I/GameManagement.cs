using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour {


	public bool isPaused;
	public bool goingBackwards;
	public bool gameover;
	public bool nextScene;
	public float bulletCount;
	public float rockCount;
	private float defaultBulletCount;
	private float defaultRockCount;
	public AudioSource musicPlayer;
	public AudioClip backgroundMusic;

	void Awake() {
		defaultBulletCount = 6;
		defaultRockCount = 10;
		DontDestroyOnLoad (gameObject);
	}
	// Use this for initialization
	void Start () {
		//ispaused = false;
		musicPlayer.PlayOneShot(backgroundMusic);
		rockCount = defaultRockCount;
		bulletCount = defaultBulletCount;

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (ispaused);
		//Debug.Log(isbackward);

		if (nextScene == true) {
			defaultBulletCount = bulletCount;
			defaultRockCount = rockCount;
			nextScene = false;
		}

		if (gameover == true) 
		{
			bulletCount = defaultBulletCount;
			rockCount = defaultRockCount;
			gameover = false;

		}

		if (goingBackwards == true && SceneManager.GetActiveScene ().name == "Cave") 
		{
			GameObject.Find ("ForwardPortal").SetActive (false);
			GameObject.Find ("BackwardPortal").SetActive (true);
		}
	}
}
