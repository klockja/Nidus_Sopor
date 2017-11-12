using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderBridgeScript : MonoBehaviour {

	public GameObject[] OverColliders;
	public GameObject[] UnderColliders;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player" || col.tag == "Enemy")
		{
			GetComponentInChildren <SpriteRenderer> ().sortingOrder = 0;
			for (int i = 0; i < OverColliders.Length; i++)
			{
				OverColliders [i].SetActive (false);
			}
			for (int i = 0; i < UnderColliders.Length; i++)
			{
				UnderColliders [i].SetActive (true);
			}
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.tag == "Player" || col.tag == "Enemy")
		{
			GetComponentInChildren <SpriteRenderer> ().sortingOrder = 2;
			for (int i = 0; i < OverColliders.Length; i++)
			{
				OverColliders [i].SetActive (true);
			}
			for (int i = 0; i < UnderColliders.Length; i++)
			{
				UnderColliders [i].SetActive (false);
			}
		}
	}
}
