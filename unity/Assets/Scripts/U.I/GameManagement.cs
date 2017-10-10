using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour {


	public bool ispaused;
	public bool isbackward;

	void Awake() {
		DontDestroyOnLoad (gameObject);
	}
	// Use this for initialization
	void Start () {
		//ispaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (ispaused);
		//Debug.Log(isbackward);
	}
}
