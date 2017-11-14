using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AmmoSpriteScript : MonoBehaviour {

	public Sprite Ammo0;
	public Sprite Ammo1;
	public Sprite Ammo2;
	public Sprite Ammo3;
	public Sprite Ammo4;
	public Sprite Ammo5;
	public Sprite Ammo6;

	private string currentScene;
	private string newScene;
	private GameObject player;

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player");
		currentScene = SceneManager.GetActiveScene().name;
	}
	
	// Update is called once per frame
	void Update () {
		//newScene = SceneManager.GetActiveScene().name;

		//if (currentScene != newScene) 
		//{
		//	player = GameObject.Find ("Player");
		//	currentScene = newScene;
		//}

		//if(currentScene == "Beach" || currentScene == "Forest" || currentScene == "Cave" || currentScene == "Forest2" || currentScene == "Beach2")
		//	AmmoSpriteChange ();

		if (SceneManager.GetActiveScene ().name != "LaodingScreen" || SceneManager.GetActiveScene ().name != "Title Scene" || SceneManager.GetActiveScene ().name != "TItle Scene") {
			player = GameObject.Find ("Player");
			AmmoSpriteChange ();
		}
	}

	private void AmmoSpriteChange()
	{
		if (player.GetComponent<PlayerController> ().bulletNum == 0) {
			gameObject.GetComponent<Image> ().sprite = Ammo0;
		}

		else if (player.GetComponent<PlayerController> ().bulletNum == 1) {
			gameObject.GetComponent<Image> ().sprite = Ammo1;
		}
			
		else if (player.GetComponent<PlayerController> ().bulletNum == 2) {
			gameObject.GetComponent<Image> ().sprite = Ammo2;
		}

		else if (player.GetComponent<PlayerController> ().bulletNum == 3) {
			gameObject.GetComponent<Image> ().sprite = Ammo3;
		}

		else if (player.GetComponent<PlayerController> ().bulletNum == 4) {
			gameObject.GetComponent<Image> ().sprite = Ammo4;
		}

		else if (player.GetComponent<PlayerController> ().bulletNum == 5) {
			gameObject.GetComponent<Image> ().sprite = Ammo5;
		}

		else if (player.GetComponent<PlayerController> ().bulletNum == 6) {
			gameObject.GetComponent<Image> ().sprite = Ammo6;
		}
		
	}
}
