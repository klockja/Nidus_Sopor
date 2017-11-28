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
	private Image image;

	private PlayerController m_playerController;

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player");
		currentScene = SceneManager.GetActiveScene().name;
		image = this.gameObject.GetComponent<Image>();
	}

	private void SearchForPlayer()
	{
		GameObject gameObject = GameObject.Find ("Player");
		if(gameObject != null)
		{
			m_playerController = gameObject.GetComponent<PlayerController>();
		}
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
			
			if(m_playerController == null)
			{
				SearchForPlayer();
				return;
			}
			AmmoSpriteChange ();
		}
	}

	private void AmmoSpriteChange()
	{

		if (m_playerController.bulletNum == 0) {
			image.sprite = Ammo0;
		}

		else if (m_playerController.bulletNum == 1) {
			image.sprite = Ammo1;
		}
			
		else if (m_playerController.bulletNum == 2) {
			image.sprite = Ammo2;
		}

		else if (m_playerController.bulletNum == 3) {
			image.sprite = Ammo3;
		}

		else if (m_playerController.bulletNum == 4) {
			image.sprite = Ammo4;
		}

		else if (m_playerController.bulletNum == 5) {
			image.sprite = Ammo5;
		}

		else if (m_playerController.bulletNum == 6) {
			image.sprite = Ammo6;
		}
		
	}
}
