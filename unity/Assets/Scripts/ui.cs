using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ui : MonoBehaviour {

	public int menuID=0;
	public GameObject Panel1;
	public GameObject Panel2;
	public GameObject Panel3;
	//public GameObject Panel4;
	public GameManagement gameManagement;

	//test

	// Use this for initialization
	void Start () {
		//TitlePanel = GameObject.FindGameObjectWithTag("TitlePanel");

		//TitlePanel = GameObject.Find("TitlePanel");
		//GameSelectionPanel = GameObject.Find("GameSelectionPanel");



		//        int playerNum = PlayerInfo.playerID;
		//        Debug.Log (playerNum);
		switchToMenu (menuID);

		GameObject managementObject = GameObject.Find ("managementObject");
		gameManagement = managementObject.GetComponent<GameManagement>();
	}

	// Update is called once per frame
	void Update () {

	}

	public void switchToMenu(int menuID) {
		
		Panel1.SetActive (false);
		Panel2.SetActive (false);
		Panel3.SetActive (false);
		//Panel4.SetActive (false);

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
	//	case 3:
	//		Panel4.gameObject.SetActive(true);
	//		break;
		}
	}


	public void PauseScene ()
	{
		if (gameManagement.ispaused == false) {
			gameManagement.ispaused = true;
			Panel2.gameObject.SetActive (true);
			Panel1.gameObject.SetActive (false);
		} else {
			gameManagement.ispaused = false;
			Panel2.gameObject.SetActive (true);
			Panel1.gameObject.SetActive (false);
		}
	}

	public void UnPauseScene ()
	{
		if (gameManagement.ispaused == false) {
			gameManagement.ispaused = true;
			Panel1.gameObject.SetActive (true);
			Panel2.gameObject.SetActive (false);
		} else {
			gameManagement.ispaused = false;
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
}
	