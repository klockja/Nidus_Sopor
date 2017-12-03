using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellTriggerSpawnerScript : MonoBehaviour 
{
	public GameObject SmellTrigger;
	public float spawnDelay;
	private float timer;
	public bool isCovered;

	void Start()
	{
		timer = spawnDelay;
	}

	void Update()
	{
		timer = timer - Time.deltaTime;

		if (timer <= 0 && isCovered == false)
		{
			Instantiate (SmellTrigger, transform.position, Quaternion.identity);
			timer = spawnDelay;
		}
	}
}
