using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager: GenericSingletonClass<TutorialManager> {

	public GameObject point1;
	public GameObject point2;
	public GameObject point3;

	public GameObject text1;
	public GameObject text2;
	public GameObject text3;

	public float pointNumber; // Currently not in used
	public int getPoint;

	private string currentScene;
	private string newScene;

	// Use this for initialization
	void Start () {

		currentScene = SceneManager.GetActiveScene ().name;
		getPoint = 1;
			
	}
	
	// Update is called once per frame
	void Update () {

		newScene = SceneManager.GetActiveScene ().name;

		if (currentScene != newScene) {
			currentScene = newScene;
		}

		if (currentScene == "Beach" && point1 == null && point2 == null && point3 == null) {

			SearchForPoint ();

			text1.SetActive (true);
			text2.SetActive (false);
			text3.SetActive (false);

		}

		if (getPoint == 2) {
			point1.SetActive (true);
			point2.SetActive (false);
			point3.SetActive (false);
			getPoint += 1;
		}

		if (currentScene != "Beach") {
			text1.SetActive (false);
			text2.SetActive (false);
			text3.SetActive (false);

		}
	}

	private void SearchForPoint()
	{
		point1 = GameObject.Find ("Point1");
		point2 = GameObject.Find ("Point2");
		point3 = GameObject.Find ("Point3");
		getPoint += 1;

	}
		
}
