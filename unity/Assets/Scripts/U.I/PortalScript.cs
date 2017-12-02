using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour {

	private string nextScene;
	private string currentScene;
	//private bool backward;

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawCube (transform.position, new Vector2 (0.75f, 0.75f));
	}

	void Start () 
	{



	}

	void Update () 
	{
			
		//Debug.Log (currentScene);
		//Debug.Log (nextScene);
		//backward = GameObject.Find ("GameManager").GetComponent<GameManagement> ().goingBackwards;

	}
		

	protected string DetermineNextScene()
	{
		//Debug.Log (currentScene);
		if (currentScene == "Beach") {
			nextScene = "Forest1-1";
			return nextScene;
		} else if (currentScene == "Forest1-1") {
			nextScene = "Forest1-2";
			return nextScene;
		} else if (currentScene == "Forest1-2") {
			nextScene = "Forest1-3";
			return nextScene;
		} else if (currentScene == "Forest1-3") {
			nextScene = "Forest1-4";
			return nextScene;
		} else if (currentScene == "Forest1-4") {
			nextScene = "Cave";
			return nextScene;
		}  else if (currentScene == "Cave" && GameManagement.Instance.hasegg == true) {
			nextScene = "Forest2";
			return nextScene;
		}  else if (currentScene == "Forest2") {
			nextScene = "Beach2";
			return nextScene;
		} else if ((currentScene == "Beach2")) {
			MusicManagerScript.Instance.musicPlayer.clip = MusicManagerScript.Instance.epilogue;
			MusicManagerScript.Instance.musicPlayer.Play();
			GameplayCanvasScript.Instance.VictoryPanel.SetActive (true);
			nextScene = null;
			return nextScene;
		} else 
		{
			Debug.Log ("Error In PortalScript");
			return nextScene;
		}
	}
		

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")  
		{
			currentScene = SceneManager.GetActiveScene().name;
			DetermineNextScene ();
			GameObject.Find ("GameManager").GetComponent<GameManagement> ().nextScene = true;
			//Debug.Log ("TIME TO LOAD NEW SCENE");
			GameplayCanvasScript.Instance.LoadSceneNow (nextScene);
		}
	} 


}
