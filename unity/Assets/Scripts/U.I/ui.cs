using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ui : MonoBehaviour {

	public int menuID=0;
	public GameObject Panel1;
	public GameObject Panel2;
	public GameObject Panel3;
	public GameObject Panel4;
	public GameObject Panel5;
	public GameObject Panel6;
	public GameObject Panel7;
	public GameObject Panel8;

	public GameObject BlackPanel;
	public GameObject DefeatPanel;
	public GameObject VictoryPanel;
	public GameObject AreYouSurePanel;
	public GameManagement GM;

	public Sprite sprite1;
	public Sprite sprite2;



	// Use this for initialization
	void Awake() {

		DontDestroyOnLoad (gameObject);
		BlackPanel.SetActive (false);
		AreYouSurePanel.SetActive (false);
	}

	void Start () {
		//TitlePanel = GameObject.FindGameObjectWithTag("TitlePanel");

		//TitlePanel = GameObject.Find("TitlePanel");
		//GameSelectionPanel = GameObject.Find("GameSelectionPanel");



		//        int playerNum = PlayerInfo.playerID;
		//        Debug.Log (playerNum);
		switchToMenu (menuID);

		GameObject GameManager = GameObject.Find ("GameManager");
		GM = GameManager.GetComponent<GameManagement>();
	}

	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name != "Title Scene") 
		{
			if (Input.GetKeyDown (KeyCode.Escape)) 
			{
				PauseScene ();
			}

		}


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
		if (C == "Title Scene") {
			SceneManagerScript.Instance.LoadScene (C, "titleMusic");
		} else if (C == "Beach") {
			SceneManagerScript.Instance.LoadScene (C, "beachMusic");
		}
	}

	public void ReloadScene()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		DefeatPanel.gameObject.SetActive(false);
	}

	public void Quit() 
	{
		Application.Quit();
	}

	public void Destroy()
	{
		Destroy (GameObject.Find("GameManager"));
	}

	public void Destroyself()
	{
		Invoke ("_Destroyself", 0.1f);
	}

	void _Destroyself()
	{
		Destroy (gameObject);
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
		
		if (GameObject.Find("Sound Button").GetComponent<Image>().sprite == sprite1) 
		{
			GameObject.Find("Sound Button").GetComponent<Image>().sprite = sprite2;
		} else if (GameObject.Find("Sound Button").GetComponent<Image>().sprite == sprite2) 
		{
			GameObject.Find("Sound Button").GetComponent<Image>().sprite = sprite1;
		}

	}

	public void AreYouSure()
	{
		AreYouSurePanel.SetActive (true);
	}

	public void TurnOnBlackScreen()
	{
		if (BlackPanel.activeSelf == false) {
			BlackPanel.SetActive (true);
	
		} else if (BlackPanel.activeSelf == true) {
			BlackPanel.SetActive (false);
		}
	}


}
	