using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUnkePosition : MonoBehaviour {

	public GameObject Unke;
	private float x;
	private float y;

	// Use this for initialization
	void Start () {

		//Unke = GameObject.Find ("Unke");
	}
	
	// Update is called once per frame
	void Update () {


		if (Unke != null) 
		{
			x = Unke.transform.position.x;
			y = Unke.transform.position.y + 0.5f;
			transform.position = new Vector3 (x, y, 0);

		} 
		else 
		{
			TutorialManager.Instance.point7.SetActive (false);
			TutorialManager.Instance.text7.SetActive (false);
			TutorialManager.Instance.point8.SetActive (true);
			TutorialManager.Instance.text8.SetActive (true);
		}
	}
}
