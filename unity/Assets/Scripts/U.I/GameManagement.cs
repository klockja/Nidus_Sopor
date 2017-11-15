using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : GenericSingletonClass<GameManagement> {


	public bool isPaused;
	public bool goingBackwards;
	public bool gameover;
	public bool nextScene;
	public bool hasegg;
	public float bulletCount;
	public float unusedBullet;
	public float rockCount;
	public GameObject player;
	public float playerDeathNumber;

	private string currentScene;
	private string newScene;
	//private float defaultBulletCount;
	//private float defaultUnusedBullet;
	//private float defaultRockCount;


	// Use this for initialization
	void Start () {
		//ispaused = false;
		player = GameObject.Find("Player");
		bulletCount = 6;
		unusedBullet = 12;
		rockCount = 1;
		playerDeathNumber = 0;
		currentScene = SceneManager.GetActiveScene().name;
	

	}



	// Update is called once per frame
	void Update () {
		//Debug.Log (ispaused);
		//Debug.Log(isbackward);
		newScene = SceneManager.GetActiveScene().name;

		if (currentScene != newScene || playerDeathNumber > 0) 
		{
			GrabPlayer ();
			currentScene = newScene;
		}

		if (currentScene == "Title Scene" || currentScene == "TItle Scene") {
			playerDeathNumber = 0;
		}

		if (nextScene == true) {
			unusedBullet = GameObject.Find("Player").GetComponent<PlayerController>().unusedBulletNum;
			bulletCount = GameObject.Find("Player").GetComponent<PlayerController>().bulletNum;
			rockCount = GameObject.Find("Player").GetComponent<PlayerController>().rockNum;
			nextScene = false;
		}

		if (gameover == true) 
		{
			GameObject.Find("Player").GetComponent<PlayerController>().unusedBulletNum = unusedBullet;
			GameObject.Find("Player").GetComponent<PlayerController>().bulletNum = bulletCount;
			GameObject.Find("Player").GetComponent<PlayerController>().rockNum = rockCount;

			gameover = false;

		}

		if (goingBackwards == true && SceneManager.GetActiveScene ().name == "Cave") 
		{
			//GameObject.Find ("ForwardPortal").SetActive (false);
			GameObject.Find ("BackwardPortal").SetActive (true);
		}
			
	}

	public void GrabPlayer ()
	{
		player = GameObject.Find ("Player");
	}
}
