using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPointScript : MonoBehaviour {

	private int gp;

	private GameObject p1;
	private GameObject pi2;
	private GameObject pi3;
	private GameObject pi4;
	private GameObject pi5;
	private GameObject pi6;
	private GameObject pi7;
	private GameObject p2;
	private GameObject p3;
	private GameObject p4;
	private GameObject p4a;
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

	private GameObject i1;
	private GameObject i2;
	private GameObject i3;
	private GameObject i4;
	private GameObject i5;
	private GameObject i6;
	private GameObject i7;


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

		i1 = TutorialManager.Instance.image1;
		i2 = TutorialManager.Instance.image2;
		i3 = TutorialManager.Instance.image3;
		i4 = TutorialManager.Instance.image4;
		i5 = TutorialManager.Instance.image5;
		i6 = TutorialManager.Instance.image6;
		i7 = TutorialManager.Instance.image7;


	}
	
	// Update is called once per frame
	void Update () {
		gp = TutorialManager.Instance.getPoint;
		if (gp == 3) 
		{
			//p1 = TutorialManager.Instance.point1;
			//pi2 = TutorialManager.Instance.pointi2;
			//pi3 = TutorialManager.Instance.pointi3;
			//pi4 = TutorialManager.Instance.pointi4;
			//pi5 = TutorialManager.Instance.pointi5;
			//pi6 = TutorialManager.Instance.pointi6;
			//pi7 = TutorialManager.Instance.pointi7;
			//p2 = TutorialManager.Instance.point2;
			//p3 = TutorialManager.Instance.point3;
			//p4 = TutorialManager.Instance.point4;
			//p4a = TutorialManager.Instance.point4a;
			//p5 = TutorialManager.Instance.point5;
			//p6 = TutorialManager.Instance.point6;
			//p7 = TutorialManager.Instance.point7;
			//p8 = TutorialManager.Instance.point8;
			TutorialManager.Instance.getPoint += 1;
		}

		if (gp == 4) {
			if (TutorialManager.Instance.pointi2.activeSelf == true && Input.GetKeyDown (KeyCode.Space)) {
				TutorialManager.Instance.pointi3.SetActive (true);
				i3.SetActive (true);
				TutorialManager.Instance.pointi2.SetActive (false);
				i2.SetActive (false);
				
			} else if (TutorialManager.Instance.pointi3.activeSelf == true && Input.GetKeyDown (KeyCode.Space)) {
				i4.SetActive (true);
				TutorialManager.Instance.pointi4.SetActive (true);
				i3.SetActive (false);
				TutorialManager.Instance.pointi3.SetActive (false);

			} else if (TutorialManager.Instance.pointi4.activeSelf == true && Input.GetKeyDown (KeyCode.Space)) {
				TutorialManager.Instance.point3.SetActive (true);
				t3.SetActive (true);
				GameManagement.Instance.isPaused = false;
				i4.SetActive (false);
				TutorialManager.Instance.pointi4.SetActive (false);

			} else if (TutorialManager.Instance.pointi5.activeSelf == true && Input.GetKeyDown (KeyCode.Space)) {
				TutorialManager.Instance.point4.SetActive (true);
				t4.SetActive (true);
				GameManagement.Instance.isPaused = false;
				i5.SetActive (false);
				TutorialManager.Instance.pointi5.SetActive (false);

			} else if (TutorialManager.Instance.pointi6.activeSelf == true && Input.GetKeyDown (KeyCode.Space)) {
				TutorialManager.Instance.point5.SetActive (true);
				t5.SetActive (true);
				GameManagement.Instance.isPaused = false;
				i6.SetActive (false);
				TutorialManager.Instance.pointi6.SetActive (false);

			} else if (TutorialManager.Instance.pointi7.activeSelf == true && Input.GetKeyDown (KeyCode.Space)) {
				TutorialManager.Instance.point7.SetActive (true);
				t7.SetActive (true);
				GameManagement.Instance.isPaused = false;
				i7.SetActive (false);
				TutorialManager.Instance.pointi7.SetActive (false);

			}
		}

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			//Debug.Log ("player enter point");
			if (TutorialManager.Instance.point1.activeSelf == true) {
				TutorialManager.Instance.pointi2.SetActive (true);
				i2.SetActive (true);
				GameManagement.Instance.isPaused = true;
				TutorialManager.Instance.point1.SetActive (false);
				t1.SetActive (false);

			} 

			else if (TutorialManager.Instance.point3.activeSelf == true) {
				TutorialManager.Instance.pointi5.SetActive (true);
				i5.SetActive (true);
				GameManagement.Instance.isPaused = true;
				TutorialManager.Instance.point3.SetActive (false);
				t3.SetActive (false);

			} 

			else if (TutorialManager.Instance.point4a.activeSelf == true) {
				TutorialManager.Instance.pointi6.SetActive (true);
				i6.SetActive (true);
				GameManagement.Instance.isPaused = true;
				TutorialManager.Instance.point4a.SetActive (false);

			}

			else if (TutorialManager.Instance.point5.activeSelf == true) {
				TutorialManager.Instance.point6.SetActive (true);
				t6.SetActive (true);
				TutorialManager.Instance.point5.SetActive (false);
				t5.SetActive (false);
			}

			else if (TutorialManager.Instance.point6.activeSelf == true) {
				TutorialManager.Instance.pointi7.SetActive (true);
				i7.SetActive (true);
				GameManagement.Instance.isPaused = true;
				TutorialManager.Instance.point6.SetActive (false);
				t6.SetActive (false);
			}
	
		}

		else if (col.gameObject.tag == "Rocks" || col.gameObject.name == "Unke (3)")
		{
			if (TutorialManager.Instance.point4.activeSelf == true) 
			{
				TutorialManager.Instance.point4a.SetActive (true);
				TutorialManager.Instance.point4.SetActive (false);
				t4.SetActive (false);
			}

			//else if (p7.activeSelf == true) 
			//{
			//	p8.SetActive (true);
			//	t8.SetActive (true);
			//	p7.SetActive (false);
			//	t7.SetActive (false);
			//}
		}
			
	}
}
