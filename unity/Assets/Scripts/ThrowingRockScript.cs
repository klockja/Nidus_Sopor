using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingRockScript : MonoBehaviour {

	public float time;
	public GameObject RockObject;

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreCollision (gameObject.GetComponent<CircleCollider2D> (), GameObject.Find("Player").GetComponent<BoxCollider2D> ());
		StartCoroutine(Delay(2));
		gameObject.GetComponentInChildren<AudioDetectionScript> ().AudioRadius = new Vector3 (0, 0, 1);
		gameObject.GetComponentInChildren<AudioDetectionScript> ().colliderRadius = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (time == 0) 
		{
			gameObject.GetComponentInChildren<AudioDetectionScript> ().AudioRadius = new Vector3 (10, 10, 1);
			gameObject.GetComponentInChildren<AudioDetectionScript> ().colliderRadius = 0.1f;
			gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			Invoke ("SwitchObject", 2);
		}


	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			GameObject.Find ("GameManager").GetComponent<GameManagement> ().rockCount += 1;
		}

		if (col.gameObject.tag == "Object" || col.gameObject.tag == "Enemy") 
		{
			gameObject.GetComponentInChildren<AudioDetectionScript> ().AudioRadius = new Vector3 (10, 10, 1);
			gameObject.GetComponentInChildren<AudioDetectionScript> ().colliderRadius = 0.1f;
			gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		}
	}

	void SwitchObject()
	{
		Instantiate (RockObject, gameObject.transform.position, gameObject.transform.rotation);
		Destroy (gameObject);
	}

	IEnumerator Delay(float maxtime)
	{
		time = maxtime;
		while (time > 0) {
			//Debug.Log (time);
			yield return new WaitForSeconds (1.0f);
			time--;
		}
	}
}
