using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour {

	private string nextScene;
	private string currentScene;
	private bool backward;
	// Use this for initialization
	void Start () {

		currentScene = SceneManager.GetActiveScene ().name;


	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (nextScene);
		backward = GameObject.Find ("managementObject").GetComponent<GameManagement> ().goingBackwards;
		DetermineDirection ();
		DetermineNextScene ();
	}

	void DetermineDirection()
	{
		if (backward == false && currentScene != "Cave")
		{
			transform.localPosition = new Vector3 (0, 10, 0);
		} else if (backward == true || currentScene == "Cave") 
		{
			transform.localPosition = new Vector3 (0, -12, 0);
		}
	}

	protected string DetermineNextScene()
	{
		//Debug.Log (currentScene);
		if ((currentScene == "Beach") && (backward == false)) {
			nextScene = "Forest";
			return nextScene;
		} else if ((currentScene == "Forest") && (backward == false)) {
			nextScene = "City";
			return nextScene;
		} else if ((currentScene == "City") && (backward == false)) {
			nextScene = "Cave";
			return nextScene;
		} else if ((currentScene == "Cave") && (backward == true)) {
			nextScene = "City";
			return nextScene;
		} else if ((currentScene == "City") && (backward == true)) {
			nextScene = "Forest";
			return nextScene;
		} else if ((currentScene == "Forest") && (backward == true)) {
			nextScene = "Beach";
			return nextScene;
		} else if ((currentScene == "Beach") && (backward == true)) {
			GameObject.Find ("Canvas").GetComponent<ui> ().Panel5.SetActive (true);
			nextScene = null;
			return nextScene;
		} else 
		{
			//Debug.Log ("Error In PortalScript");
			return nextScene;
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Player") 
		{
			SceneManager.LoadScene(nextScene);
		}
	} 


}
