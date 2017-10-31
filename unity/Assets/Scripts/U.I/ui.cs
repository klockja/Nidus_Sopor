using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
	public GameObject Panel9;
	public GameManagement GM;

	// Use this for initialization
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
		Panel9.SetActive (false);

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
		case 8:
			Panel9.gameObject.SetActive(true);
			break;
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
		SceneManager.LoadScene(C);	
	}

	public void Quit() 
	{
		Application.Quit();
	}

	public void Destroy()
	{
		Destroy (GameObject.Find("GameManager"));
	}

	public void SoundControl()
	{
		if (AudioListener.volume != 0) {
			AudioListener.volume = 0f;
		} else {
			AudioListener.volume = 1f;
		}
	}
}
	