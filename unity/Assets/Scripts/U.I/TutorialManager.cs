using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager: GenericSingletonClass<TutorialManager> {

	public GameObject point1;
	public GameObject pointi2;
	public GameObject pointi3;
	public GameObject pointi4;
	public GameObject pointi5;
	public GameObject pointi6;
	public GameObject pointi7;
	public GameObject point2;
	public GameObject point3;
	public GameObject point4;
	public GameObject point4a;
	public GameObject point5;
	public GameObject point6;
	public GameObject point7;
	public GameObject point8;

	public GameObject text1;
	public GameObject text2;
	public GameObject text3;
	public GameObject text4;
	public GameObject text5;
	public GameObject text6;
	public GameObject text7;
	public GameObject text8;

	public GameObject image1;
	public GameObject image2;
	public GameObject image3;
	public GameObject image4;
	public GameObject image5;
	public GameObject image6;
	public GameObject image7;

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

		if (currentScene == "Beach" && point1 == null && point2 == null && point3 == null && point4 == null && point4a == null && point5 == null && point6 == null && point7 == null && point8 == null && pointi2 == null && pointi3 == null && pointi4 == null && pointi5 == null && pointi6 == null && pointi7 == null) {

			SearchForPoint ();

			text1.SetActive (true);
			text2.SetActive (false);
			text3.SetActive (false);
			text4.SetActive (false);
			text5.SetActive (false);
			text6.SetActive (false);
			text7.SetActive (false);
			text8.SetActive (false);

			image1.SetActive (false);
			image2.SetActive (false);
			image3.SetActive (false);
			image4.SetActive (false);
			image5.SetActive (false);
			image6.SetActive (false);
			image7.SetActive (false);

		}

		if (getPoint == 2) {
			point1.SetActive (true);
			pointi2.SetActive (false);
			pointi3.SetActive (false);
			pointi4.SetActive (false);
			pointi5.SetActive (false);
			pointi6.SetActive (false);
			pointi7.SetActive (false);
			point2.SetActive (false);
			point3.SetActive (false);
			point4.SetActive (false);
			point4a.SetActive (false);
			point5.SetActive (false);
			point6.SetActive (false);
			point7.SetActive (false);
			point8.SetActive (false);
			getPoint += 1;
		}

		if (currentScene != "Beach") {
			text1.SetActive (false);
			text2.SetActive (false);
			text3.SetActive (false);
			text4.SetActive (false);
			text5.SetActive (false);
			text6.SetActive (false);
			text7.SetActive (false);
			text8.SetActive (false);

			image1.SetActive (false);
			image2.SetActive (false);
			image3.SetActive (false);
			image4.SetActive (false);
			image5.SetActive (false);
			image6.SetActive (false);
			image7.SetActive (false);

		}
			
	}

	private void SearchForPoint()
	{
		point1 = GameObject.Find ("Point1");
		pointi2 = GameObject.Find ("Pointi2");
		pointi3 = GameObject.Find ("Pointi3");
		pointi4 = GameObject.Find ("Pointi4");
		pointi5 = GameObject.Find ("Pointi5");
		pointi6 = GameObject.Find ("Pointi6");
		pointi7 = GameObject.Find ("Pointi7");
		point2 = GameObject.Find ("Point2");
		point3 = GameObject.Find ("Point3");
		point4 = GameObject.Find ("Point4");
		point4a = GameObject.Find ("Point4a");
		point5 = GameObject.Find ("Point5");
		point6 = GameObject.Find ("Point6");
		point7 = GameObject.Find ("Point7");
		point8 = GameObject.Find ("Point8");
		getPoint += 1;

	}
		
}
