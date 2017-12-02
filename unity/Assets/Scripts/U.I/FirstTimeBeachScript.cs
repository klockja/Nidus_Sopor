using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstTimeBeachScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

		if (GameManagement.Instance.playerDeathNumber == 0) 
		{
			GameManagement.Instance.isPaused = true;
			GameplayCanvasScript.Instance.BlackPanel.SetActive (true);
			GameplayCanvasScript.Instance.Panel2.SetActive (true);
			GameplayCanvasScript.Instance.Panel3.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
