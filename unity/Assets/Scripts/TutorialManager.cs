using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager: GenericSingletonClass<TutorialManager> {

	public GameObject point1;
	public GameObject point2;
	public GameObject point3;
	public GameObject point4;

	public GameObject text1;
	public GameObject text2;
	public GameObject text3;
	public GameObject text4;
	public float pointNumber;

	// Use this for initialization
	void Start () {

		point1.SetActive (true);
		point2.SetActive (false);
		point3.SetActive (false);
		point4.SetActive (false);

		text1.SetActive (true);
		text2.SetActive (false);
		text3.SetActive (false);
		text4.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {

		if (point1.activeSelf == true) {
			point2.SetActive (false);
		} else if (point2.activeSelf == true) {
			point1.SetActive (false);
		}
	}
		
}
