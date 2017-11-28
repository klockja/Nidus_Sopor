using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDetectionScript : MonoBehaviour {

	private CircleCollider2D myCollider;
	public float colliderRadius;
	public Vector3 AudioRadius;

	// Use this for initialization
	void Start () {
		
		myCollider = transform.GetComponent<CircleCollider2D>();
		colliderRadius = 0f;
		AudioRadius = new Vector3 (0, 0, 1);
		//myCollider.radius = colliderRadius;
		//transform.localScale = AudioRadius;

		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.localScale = AudioRadius;
		myCollider.radius = colliderRadius;
	}
}
