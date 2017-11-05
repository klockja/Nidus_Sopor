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
		if (col.tag == "Player" || col.tag == "Enemy")
		{
			spriteRenderer.sortingOrder = 3;
		}
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Player" || col.tag == "Enemy")
		{
			spriteRenderer.sortingOrder = 3;
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Player" || col.tag == "Enemy")
		{
			spriteRenderer.sortingOrder = 0;
		}
	}
}
