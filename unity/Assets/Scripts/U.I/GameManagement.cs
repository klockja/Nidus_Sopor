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
	public float unusedBullet;
	public float rockCount;
	private float defaultBulletCount;
	private float defaultUnusedBullet;
	private float defaultRockCount;
	public AudioSource musicPlayer;
	public AudioClip titleMusic;
	public AudioClip beachMusic;
	public AudioClip forestMusic;
	public AudioClip caveMusic;

	void Awake() {
		bulletCount = 6;
		unusedBullet = 12;
		defaultRockCount = 1;
		DontDestroyOnLoad (gameObject);
	}
	// Use this for initialization
	void Start () {
		//ispaused = false;
		musicPlayer.PlayOneShot(titleMusic);
		rockCount = defaultRockCount;
	

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (ispaused);
		//Debug.Log(isbackward);

		if (nextScene == true) {
			unusedBullet = GameObject.Find("Player").GetComponent<PlayerController>().unusedBulletNum;
			bulletCount = GameObject.Find("Player").GetComponent<PlayerController>().bulletNum;
			defaultRockCount = rockCount;
			nextScene = false;
		}

		if (gameover == true) 
		{
			GameObject.Find("Player").GetComponent<PlayerController>().unusedBulletNum = unusedBullet;
			GameObject.Find("Player").GetComponent<PlayerController>().bulletNum = bulletCount;
			rockCount = defaultRockCount;

			gameover = false;

		}

		if (goingBackwards == true && SceneManager.GetActiveScene ().name == "Cave") 
		{
			//GameObject.Find ("ForwardPortal").SetActive (false);
			GameObject.Find ("BackwardPortal").SetActive (true);
		}

		if (SceneManager.GetActiveScene ().name == "Beach" || SceneManager.GetActiveScene ().name == "Beach2") 
		{
			musicPlayer.PlayOneShot(beachMusic);
		}	else if (SceneManager.GetActiveScene ().name == "Forest" || SceneManager.GetActiveScene ().name == "Forest2") 
		{
			musicPlayer.PlayOneShot(forestMusic);
		}	else if (SceneManager.GetActiveScene ().name == "Cave" || SceneManager.GetActiveScene ().name == "Cave2") 
		{
			musicPlayer.PlayOneShot(caveMusic);
		}
	}
}
