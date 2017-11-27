using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSightRadiusScript : MonoBehaviour {

	private GameObject Player;
	private float currentDeathNumber;

	// Use this for initialization
	void Awake () 
	{
		Player = GameManagement.Instance.player;
		currentDeathNumber = GameManagement.Instance.playerDeathNumber;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (GameManagement.Instance.playerDeathNumber > currentDeathNumber)
		{
			Player = GameManagement.Instance.player;
			currentDeathNumber = GameManagement.Instance.playerDeathNumber;
		}

		transform.Translate ((Player.transform.position - transform.position) * Time.deltaTime * 3);
	}
}
