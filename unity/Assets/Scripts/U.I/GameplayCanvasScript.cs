using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayCanvasScript : GenericSingletonClass<GameplayCanvasScript> {

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
	public GameManagement GM;

	public Sprite sprite1;
	public Sprite sprite2;

	private float currentDeathNumber;
	private float newDeathNumber;



	void Start () {
		currentDeathNumber = GameManagement.Instance.playerDeathNumber;

		BlackPanel.SetActive (false);
		AreYouSurePanel.SetActive (false);
		LoadingPanel.SetActive (false);
		//title = GameObject.Find ("Title Panel");
		//gameplay = GameObject.Find ("Gameplay Panel");
		//pause = GameObject.Find ("Pause Panel");
		//background = GameObject.Find ("Background");

		switchToMenu (menuID);

		GameObject GameManager = GameObject.Find ("GameManager");
		GM = GameManager.GetComponent<GameManagement>();
	}

	// Update is called once per frame
	void Update () {
		newDeathNumber = GameManagement.Instance.playerDeathNumber;

		if (SceneManager.GetActiveScene ().name != "Title Scene" && SceneManager.GetActiveScene ().name != "TItle Scene" && SceneManager.GetActiveScene ().name != "Intro Cutscene" && SceneManager.GetActiveScene ().name != "Beach")
		{
			//Panel1 = gameplay;
			Panel2 = pause;
			gameplay.SetActive (true);
			title.SetActive (false);
			background.SetActive (false);

		}
		if (SceneManager.GetActiveScene ().name == "Title Scene" || SceneManager.GetActiveScene ().name == "TItle Scene") 
		{
			//Panel1 = gameplay;
			Panel2 = title;
			background.SetActive (true);
			title.SetActive (true);
			gameplay.SetActive (false);
			pause.SetActive (false);
			DefeatPanel.SetActive (false);
			AreYouSurePanel.SetActive (false);
			VictoryPanel.SetActive (false);
		}
		if (SceneManager.GetActiveScene ().name == "Intro Cutscene") 
		{
			title.SetActive (false);
			background.SetActive (false);
			gameplay.SetActive (false);
			pause.SetActive (false);
			DefeatPanel.SetActive (false);
			AreYouSurePanel.SetActive (false);
			VictoryPanel.SetActive (false);
		}

//		if (SceneManager.GetActiveScene ().name == "Beach") 
//		{
//			Panel2 = pause;
//			Panel1 = gameplay;
//			gameplay.SetActive (true);
//			title.SetActive (false);
//			background.SetActive (false);
//			GameManagement.Instance.isPaused = true;
//			BlackPanel.SetActive (true);
//			Panel2.SetActive (true);
//			Panel3.SetActive (true);
//		}

	}

	public void switchToMenu(int menuID) {

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


	public void PauseScene ()
	{
		if (GM.isPaused == false) {
			GM.isPaused = true;
			Panel2.gameObject.SetActive (true);
			Panel1.gameObject.SetActive (false);
		} else {
			GM.isPaused = false;
			Panel2.gameObject.SetActive (true);
			Panel1.gameObject.SetActive (false);
		}
	}

	public void UnPauseScene ()
	{
		if (GM.isPaused == false) {
			GM.isPaused = true;
			Panel1.gameObject.SetActive (true);
			Panel2.gameObject.SetActive (false);
		} else {
			GM.isPaused = false;
			Panel1.gameObject.SetActive (true);
			Panel2.gameObject.SetActive (false);
		}
	}
		
	public void LoadSceneNow(string C) 
	{
		
		StartCoroutine(GameObject.Find("SceneManager").GetComponent<ScreenManagerScript>().LoadScene(C));
		
	}

	public void ReloadScene()
	{
		LoadSceneNow (SceneManager.GetActiveScene ().name);
		GameManagement.Instance.playerDeathNumber += 1;
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

	public void SpriteChange()
	{
		
		if (GameObject.Find("Sound Button Image").GetComponent<Image>().sprite == sprite1) 
		{
			GameObject.Find("Sound Button Image").GetComponent<Image>().sprite = sprite2;
		} else if (GameObject.Find("Sound Button Image").GetComponent<Image>().sprite == sprite2) 
		{
			GameObject.Find("Sound Button Image").GetComponent<Image>().sprite = sprite1;
		}

	}
		
	public void AreYouSure()
	{
		if (currentDeathNumber == newDeathNumber) {
			Panel2.SetActive (false);
			AreYouSurePanel.SetActive (true);
			GameManagement.Instance.isPaused = false;
		}

		if (currentDeathNumber != newDeathNumber) {
			AreYouSurePanel.SetActive (true);
			currentDeathNumber = newDeathNumber;
		}
	}

	public void TurnOnBlackScreen()
	{
		if (BlackPanel.activeSelf == false) {
			BlackPanel.SetActive (true);
	
		} else if (BlackPanel.activeSelf == true) {
			BlackPanel.SetActive (false);
		}
	}

	public void TurnOnBlackScreen2()
	{
		if (BlackPanel.activeSelf == false && (SceneManager.GetActiveScene().name == "Title Scene" || SceneManager.GetActiveScene().name == "TItle Scene" ))
			BlackPanel.SetActive (true);
		
		else if (BlackPanel.activeSelf == true && (SceneManager.GetActiveScene().name == "Title Scene" || SceneManager.GetActiveScene().name == "TItle Scene" )) 
			BlackPanel.SetActive (false);

	}


}
	