using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutsceneBoatScript : MonoBehaviour {

	public float timerTillDisappear;

	Animator anim;

	public Dialogue DialogueScript;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent <Animator> ();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (DialogueScript.DialogueEnded)
		{
			timerTillDisappear -= Time.deltaTime;

			if (timerTillDisappear <= 0)
			{
				anim.SetBool ("SailAway", true);
			}
		}
	}
}
