using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayCanvasScript : GenericSingletonClass<GameplayCanvasScript> {

	private bool canPress = true;

	public int menuID=0;
	public GameObject Panel1;
	public GameObject Panel2;
	public GameObject Panel3;
	public GameObject Panel4;
	public GameObject Panel5;
	public GameObject Panel6;
	public GameObject Panel7;
	public GameObject Panel8;

	[SerializeField]
	private GameObject title;
	[SerializeField]
	private GameObject gameplay;
	[SerializeField]
	private GameObject pause;
	[SerializeField]
	private GameObject background;

	public GameObject BlackPanel;
	public GameObject DefeatPanel;
	public GameObject VictoryPanel;
	public GameObject AreYouSurePanel;
	public GameObject LoadingPanel;

	public Sprite sprite1;
	public Sprite sprite2;

	private string currentScene;
	private string newScene;


	void Start () {

		currentScene = SceneManager.GetActiveScene ().name;
		BlackPanel.SetActive (false);
		AreYouSurePanel.SetActive (false);
		LoadingPanel.SetActive (false);
		//title = GameObject.Find ("Title Panel");
		//gameplay = GameObject.Find ("Gameplay Panel");
		//pause = GameObject.Find ("Pause Panel");
		//background = GameObject.Find ("Background");

		switchToMenu (menuID);
	}

	// Update is called once per frame
	void Update () {
		
		newScene = SceneManager.GetActiveScene ().name;

		if (newScene != currentScene) {
			
			if (SceneManager.GetActiveScene ().name != "Title Scene" && SceneManager.GetActiveScene ().name != "TItle Scene" && SceneManager.GetActiveScene ().name != "Intro Cutscene" && GameManagement.Instance.playerDeathNumber == 0) {
				Panel1 = gameplay;
				Panel2 = pause;
				gameplay.SetActive (false);
				title.SetActive (false);
				background.SetActive (false);

			}

			if (SceneManager.GetActiveScene ().name == "Forest1-1" || SceneManager.GetActiveScene ().name == "Forest1-2" || SceneManager.GetActiveScene ().name == "Forest1-3" || SceneManager.GetActiveScene ().name == "Forest1-4" || SceneManager.GetActiveScene ().name == "Cave" || SceneManager.GetActiveScene ().name == "Cave2" || SceneManager.GetActiveScene ().name == "Forest2" || SceneManager.GetActiveScene ().name == "Beach2" || SceneManager.GetActiveScene ().name == "Beach") {
				Panel1 = gameplay;
				Panel2 = pause;
				gameplay.SetActive (true);
				title.SetActive (false);
				background.SetActive (false);

			}

			if (SceneManager.GetActiveScene ().name == "Title Scene" || SceneManager.GetActiveScene ().name == "TItle Scene" || SceneManager.GetActiveScene().buildIndex == 0) {
				Panel1 = title;
				Panel2 = title;
				background.SetActive (true);
				title.SetActive (true);
				gameplay.SetActive (false);
				pause.SetActive (false);
				DefeatPanel.SetActive (false);
				AreYouSurePanel.SetActive (false);
				VictoryPanel.SetActive (false);
			}
			if (SceneManager.GetActiveScene ().name == "Intro Cutscene") {
				title.SetActive (false);
				background.SetActive (false);
				gameplay.SetActive (false);
				pause.SetActive (false);
				DefeatPanel.SetActive (false);
				AreYouSurePanel.SetActive (false);
				VictoryPanel.SetActive (false);
			}

			currentScene = newScene;
		}

	}

	public void switchToMenu(int menuID) //switching differtn menu for button press
	{

		Panel1.SetActive (false);
		Panel2.SetActive (false);
		Panel3.SetActive (false);
		Panel4.SetActive (false);
		Panel5.SetActive (false);
		Panel6.SetActive (false);
		Panel7.SetActive (false);
		Panel8.SetActive (false);
		DefeatPanel.SetActive (false);
		VictoryPanel.SetActive (false);

		switch (menuID) {
		case 0:
			Panel1.gameObject.SetActive(true);
			break;
		case 1:
			Panel2.gameObject.SetActive(true);
			break;
		case 2:
			Panel3.gameObject.SetActive(true);
			break;
		case 3:
			Panel4.gameObject.SetActive(true);
			break;
		case 4:
			Panel5.gameObject.SetActive(true);
			break;
		case 5:
			Panel6.gameObject.SetActive(true);
			break;
		case 6:
			Panel7.gameObject.SetActive (true);
			break;
		case 7:
			Panel8.gameObject.SetActive(true);
			break;
	//	case 8:
	//		Panel9.gameObject.SetActive(true);
	//		break;
	//	case 9:
	//		Panel10.gameObject.SetActive(true);
	//		break;
		}
	}


	public void PauseScene () //puase the game and bring up the pause menu
	{
		if (GameManagement.Instance.isPaused == false) {
			GameManagement.Instance.isPaused = true;
			Panel2.gameObject.SetActive (true);
			Panel1.gameObject.SetActive (false);
		} else {
			GameManagement.Instance.isPaused = false;
			Panel2.gameObject.SetActive (true);
			Panel1.gameObject.SetActive (false);
		}
	}

	public void UnPauseScene () //unpuase the game and close the pause menu
	{
		if (GameManagement.Instance.isPaused == false) {
			GameManagement.Instance.isPaused = true;
			Panel1.gameObject.SetActive (true);
			Panel2.gameObject.SetActive (false);
		} else {
			GameManagement.Instance.isPaused = false;
			Panel1.gameObject.SetActive (true);
			Panel2.gameObject.SetActive (false);
		}
	}
		
	public void LoadSceneNow(string C) 
	{
		if (canPress == true) {
			StartCoroutine (GameObject.Find ("SceneManager").GetComponent<ScreenManagerScript> ().LoadScene (C));
			GameManagement.Instance.playerDeathNumber = 0;
			Invoke("ResetCanPress",2.0f);
			canPress = false;
		}
	}

	void ResetCanPress(){
		canPress = true;
	}

	public void ReloadScene()
	{
		LoadSceneNow (SceneManager.GetActiveScene ().name);
		GameManagement.Instance.playerDeathNumber += 1;
		//GameManagement.Instance.isPlayerDead = false;
		DefeatPanel.gameObject.SetActive(false);
		AreYouSurePanel.SetActive (false);
	}

	public void Quit() 
	{
		Application.Quit();
	}

	public void SoundControl()
	{
		if (AudioListener.volume != 0) {
			AudioListener.volume = 0f;
		} else {
			AudioListener.volume = 1f;
		}
	}

	public void SpriteChange() //change the spirte icon for sound
	{
		
		if (GameObject.Find("Sound Button Image").GetComponent<Image>().sprite == sprite1) 
		{
			GameObject.Find("Sound Button Image").GetComponent<Image>().sprite = sprite2;
		} else if (GameObject.Find("Sound Button Image").GetComponent<Image>().sprite == sprite2) 
		{
			GameObject.Find("Sound Button Image").GetComponent<Image>().sprite = sprite1;
		}

	}
		
	public void AreYouSure() //just there to ask if the player really want to quit game
	{
		if (GameManagement.Instance.isPlayerDead == false) {
			Panel2.SetActive (false);
			Panel1.SetActive (true);
			AreYouSurePanel.SetActive (true);
			GameManagement.Instance.isPaused = false;
		}

		if (GameManagement.Instance.isPlayerDead == true) {
			AreYouSurePanel.SetActive (true);
		}
	}

	public void TurnOnBlackScreen() // type 1 of turn the screen in the background black
	{
		if (BlackPanel.activeSelf == false) {
			BlackPanel.SetActive (true);
	
		} else if (BlackPanel.activeSelf == true) {
			BlackPanel.SetActive (false);
		}
	}

	public void TurnOnBlackScreen2() // type 2 of turning the screen in the background black use for title scene only
	{
		if (BlackPanel.activeSelf == false && (SceneManager.GetActiveScene().name == "Title Scene" || SceneManager.GetActiveScene().name == "TItle Scene" ))
			BlackPanel.SetActive (true);
		
		else if (BlackPanel.activeSelf == true && (SceneManager.GetActiveScene().name == "Title Scene" || SceneManager.GetActiveScene().name == "TItle Scene" )) 
			BlackPanel.SetActive (false);

	}


}
	