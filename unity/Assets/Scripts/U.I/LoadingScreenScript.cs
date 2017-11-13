using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenScript : Singleton<LoadingScreenScript> {


	public static void UnloadLoadingScene()
	{
		GameObject.Destroy(instance.gameObject);
	}
}
