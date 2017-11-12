using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenScript : GenericSingletonClass<LoadingScreenScript> {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UnloadLoadingScene()
	{
		Destroy (gameObject);
	}
}
