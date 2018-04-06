using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPointScript : MonoBehaviour {

	private int gp;

	private GameObject p1;
	private GameObject p2;
	private GameObject p3;

	private GameObject t1;
	private GameObject t2;
	private GameObject t3;

	// Use this for initialization
	void Start () {

		t1 = TutorialManager.Instance.text1;
		t2 = TutorialManager.Instance.text2;
		t3 = TutorialManager.Instance.text3;

	}
	
	// Update is called once per frame
	void Update () {
		gp = TutorialManager.Instance.getPoint;
		if (gp >= 3) 
		{
			p1 = TutorialManager.Instance.point1;
			p2 = TutorialManager.Instance.point2;
			p3 = TutorialManager.Instance.point3;
			TutorialManager.Instance.getPoint += 1;
		}
			

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			Debug.Log ("player enter point");
			if (p1.activeSelf == true) {
				p2.SetActive (true);
				t2.SetActive (true);
				p1.SetActive (false);
				t1.SetActive (false);
			}
	
		}

		if (col.gameObject.tag == "Rocks" || col.gameObject.tag == "Enemy")
		{
			if (p2.activeSelf == true) 
			{
				p3.SetActive (true);
				t3.SetActive (true);
				p2.SetActive (false);
				t2.SetActive (false);
			}
		}
			
	}
}
