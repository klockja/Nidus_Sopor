using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPointScript : MonoBehaviour {

	private float num;
	private GameObject p1;
	private GameObject p2;
	private GameObject p3;
	private GameObject p4;
	// Use this for initialization
	void Start () {
		p1 = TutorialManager.Instance.point1;
		p2 = TutorialManager.Instance.point2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			Debug.Log ("player enter point");
			if (p1.activeSelf == true) {
				p2.SetActive (true);
				p1.SetActive (false);
			}
		}

		if (col.gameObject.tag == "Object")
		{
			//Debug.Log ("player enter point");
			if (p2.activeSelf == true) {
				p3.SetActive (true);
				p2.SetActive (false);
			}
		}
	}
}
