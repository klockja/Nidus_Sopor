using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : GenericSingletonClass<GameManagement> {


	public bool isPaused;
	public bool goingBackwards;
	public bool gameover;
	public bool nextScene;
	public float bulletCount;
	public float unusedBullet;
	public float rockCount;
	public GameObject player;
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

	

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (ispaused);
		//Debug.Log(isbackward);

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
}
