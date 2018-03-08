using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPointScript : MonoBehaviour {

	private float num;
	private GameObject p1;
	private GameObject p2;
	private GameObject p3;
	private GameObject p4;

	private GameObject t1;
	private GameObject t2;
	private GameObject t3;
	private GameObject t4;
	// Use this for initialization
	void Start () {
		p1 = TutorialManager.Instance.point1;
		p2 = TutorialManager.Instance.point2;
		p3 = TutorialManager.Instance.point3;
		p4 = TutorialManager.Instance.point4;

		t1 = TutorialManager.Instance.text1;
		t2 = TutorialManager.Instance.text2;
		t3 = TutorialManager.Instance.text3;
		t4 = TutorialManager.Instance.text4;
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
				t2.SetActive (true);
				p1.SetActive (false);
				t1.SetActive (false);
			}
			else if (p3.activeSelf == true)
			{
				p4.SetActive (true);
				t4.SetActive (true);
				p3.SetActive (false);
				t3.SetActive (false);
			}
		}

		if (col.gameObject.tag == "Rocks")
		{
			//Debug.Log ("player enter point");
			if (p2.activeSelf == true) {
				p3.SetActive (true);
				t3.SetActive (true);
				p2.SetActive (false);
				t2.SetActive (false);
			}
		}
			
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if(p4.activeSelf == true)
			Destroy (gameObject);
	}
}
