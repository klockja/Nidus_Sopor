using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using UnityEditor.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour 
{

	public string SceneToLoad;
//	public GameObject Box;
	public float seconds;

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.name == "Player") 
		{
			StartCoroutine (WaitAndLoad (col.gameObject));
		}

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.name == "Player") 
		{
			StartCoroutine (WaitAndLoad (col.gameObject));
		}

	}

	IEnumerator WaitAndLoad(GameObject player)
	{
		yield return new WaitForSeconds (0.125f);
		Destroy (player);
//		Time.timeScale = 0;
//		Instantiate (Box, transform);
		yield return new WaitForSeconds (seconds);
//		Time.timeScale = 1;
		GameplayCanvasScript.Instance.LoadSceneNow (SceneToLoad);
	}
}
