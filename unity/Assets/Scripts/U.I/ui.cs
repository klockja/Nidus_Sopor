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
	public GameObject Panel9;
	public GameManagement GM;

	public Sprite sprite1;
	public Sprite sprite2;

	public Sprite Ammo0;
	public Sprite Ammo1;
	public Sprite Ammo2;
	public Sprite Ammo3;
	public Sprite Ammo4;
	public Sprite Ammo5;
	public Sprite Ammo6;

	// Use this for initialization
	void Awake() {

		DontDestroyOnLoad (gameObject);
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

			AmmoSpriteChange ();

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

	public void ReloadScene()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		Panel8.gameObject.SetActive(false);
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

	private void AmmoSpriteChange()
	{
		if (SceneManager.GetActiveScene ().name != "Title Scene") {
			if (GameObject.Find ("Player").GetComponent<PlayerController> ().bulletNum == 0)
				GameObject.Find ("Ammo Image").GetComponent<Image> ().sprite = Ammo0;

			if (GameObject.Find ("Player").GetComponent<PlayerController> ().bulletNum == 1)
				GameObject.Find ("Ammo Image").GetComponent<Image> ().sprite = Ammo1;

			if (GameObject.Find ("Player").GetComponent<PlayerController> ().bulletNum == 2)
				GameObject.Find ("Ammo Image").GetComponent<Image> ().sprite = Ammo2;

			if (GameObject.Find ("Player").GetComponent<PlayerController> ().bulletNum == 3)
				GameObject.Find ("Ammo Image").GetComponent<Image> ().sprite = Ammo3;

			if (GameObject.Find ("Player").GetComponent<PlayerController> ().bulletNum == 4)
				GameObject.Find ("Ammo Image").GetComponent<Image> ().sprite = Ammo4;

			if (GameObject.Find ("Player").GetComponent<PlayerController> ().bulletNum == 5)
				GameObject.Find ("Ammo Image").GetComponent<Image> ().sprite = Ammo5;

			if (GameObject.Find ("Player").GetComponent<PlayerController> ().bulletNum == 6)
				GameObject.Find ("Ammo Image").GetComponent<Image> ().sprite = Ammo6;
		} 
	}
}
	