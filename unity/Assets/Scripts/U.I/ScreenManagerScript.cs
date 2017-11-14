using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManagerScript :MonoBehaviour {

	[SerializeField]
	private Fading m_blackScreen;
	[SerializeField]
	private float m_minDuration = 1.5f;
	[SerializeField]
	private GameObject all;

	void start ()
	{
		
	}
		
	public IEnumerator LoadScene(string sceneName)
	{
		// Disable Canvas
		all.SetActive (false);

		// Fade to black
		yield return StartCoroutine(m_blackScreen.FadeIn());

		// Load loading screen
		yield return SceneManager.LoadSceneAsync("LoadingScreen");

		// !!! unload old screen (automatic)

		// Fade to loading screen
		Debug.Log ("Fade out");
		yield return StartCoroutine(m_blackScreen.FadeOut());


		float endTime = Time.time + m_minDuration;


		// Load level async
		yield return Application.LoadLevelAdditiveAsync(sceneName);

		while (Time.time < endTime)
			yield return null;
		//yield return new WaitForSeconds(endTime - Time.time);

		// Load appropriate zone's music based on zone data
		//MusicManagerScript.Instance.PlayMusic(music);

		// Fade to black
		yield return StartCoroutine(m_blackScreen.FadeIn());

		// !!! unload loading screen
		SceneManager.UnloadSceneAsync("LoadingScreen");

		// Reactive Canvas
		all.SetActive (true);

		// Fade to new screen
		yield return StartCoroutine(m_blackScreen.FadeOut());


	}

}
