using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager: GenericSingletonClass<TutorialManager> {

	public GameObject point1;
	public GameObject point2;
	public float pointNumber;

	// Use this for initialization
	void Start () {

		point1.SetActive (true);
		point2.SetActive (false);
		
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
