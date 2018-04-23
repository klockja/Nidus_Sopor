using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPointScript : MonoBehaviour {

	private int gp;

	private GameObject p1;
	private GameObject p2;
	private GameObject p3;
	private GameObject p4;
	private GameObject p5;
	private GameObject p6;
	private GameObject p7;
	private GameObject p8;

	private GameObject t1;
	private GameObject t2;
	private GameObject t3;
	private GameObject t4;
	private GameObject t5;
	private GameObject t6;
	private GameObject t7;
	private GameObject t8;

	// Use this for initialization
	void Start () {

		t1 = TutorialManager.Instance.text1;
		t2 = TutorialManager.Instance.text2;
		t3 = TutorialManager.Instance.text3;
		t4 = TutorialManager.Instance.text4;
		t5 = TutorialManager.Instance.text5;
		t6 = TutorialManager.Instance.text6;
		t7 = TutorialManager.Instance.text7;
		t8 = TutorialManager.Instance.text8;

	}
	
	// Update is called once per frame
	void Update () {
		gp = TutorialManager.Instance.getPoint;
		if (gp >= 3) 
		{
			p1 = TutorialManager.Instance.point1;
			p2 = TutorialManager.Instance.point2;
			p3 = TutorialManager.Instance.point3;
			p4 = TutorialManager.Instance.point4;
			p5 = TutorialManager.Instance.point5;
			p6 = TutorialManager.Instance.point6;
			p7 = TutorialManager.Instance.point7;
			p8 = TutorialManager.Instance.point8;
			TutorialManager.Instance.getPoint += 1;
		}

		if (p2.activeSelf == true && Input.GetKeyDown(KeyCode.Space))
		{
				p3.SetActive (true);
				t3.SetActive (true);
				p2.SetActive (false);
				t2.SetActive (false);
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

			else if (p3.activeSelf == true) {
				p4.SetActive (true);
				t4.SetActive (true);
				p3.SetActive (false);
				t3.SetActive (false);
			}
				

			else if (p5.activeSelf == true) {
				p6.SetActive (true);
				t6.SetActive (true);
				p5.SetActive (false);
				t5.SetActive (false);
			}

			else if (p6.activeSelf == true) {
				p7.SetActive (true);
				t7.SetActive (true);
				p6.SetActive (false);
				t6.SetActive (false);
			}
	
		}

		else if (col.gameObject.tag == "Rocks" || col.gameObject.name == "Unke (3)")
		{
			if (p4.activeSelf == true) 
			{
				p5.SetActive (true);
				t5.SetActive (true);
				p4.SetActive (false);
				t4.SetActive (false);
			}

			else if (p7.activeSelf == true) 
			{
				p8.SetActive (true);
				t8.SetActive (true);
				p7.SetActive (false);
				t7.SetActive (false);
			}
		}
			
	}
}
