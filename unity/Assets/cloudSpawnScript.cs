using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudSpawnScript : MonoBehaviour {

	public GameObject[] Clouds;

	public float timer;
	private float originalTimer;
	public float minRandomRange;
	public float maxRandomRange;

	// Use this for initialization
	void Start () 
	{
		originalTimer = timer;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (timer <= 0)
		{
			Instantiate (Clouds[Random.Range (0, Clouds.Length)], new Vector2(transform.position.x, transform.position.y + Random.Range (.1f, 5f)), Quaternion.identity);
			timer = originalTimer + Random.Range (minRandomRange, maxRandomRange);
		}

		timer -= Time.deltaTime;
	}
}
