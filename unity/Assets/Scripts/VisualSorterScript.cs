using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualSorterScript : MonoBehaviour 
{
	private SpriteRenderer spriteRenderer;

	public void Start()
	{
		spriteRenderer = GetComponent <SpriteRenderer> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			col.GetComponent <SpriteRenderer> ().sortingOrder = -1;
		}

		if (col.tag == "Enemy")
		{
			col.GetComponentInChildren <SpriteRenderer> ().sortingOrder = -1;
		}
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			col.GetComponent <SpriteRenderer> ().sortingOrder = -1;
		}

		if (col.tag == "Enemy")
		{
			col.GetComponentInChildren <SpriteRenderer> ().sortingOrder = -1;
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Player" || col.tag == "Enemy")
		{
			col.GetComponentInChildren <SpriteRenderer> ().sortingOrder = 2;
		}
	}
}
