using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : GenericSingletonClass<SceneManagerScript> {


	public float endTime;
	public float m_minDuration;
	public GameObject m_GM;
	// Use this for initialization


	void start ()
	{
		m_minDuration = 5f;
		m_GM = GameObject.Find ("GameManager");
	}
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator LoadScene(string sceneName, string music)
	{

		// Fade to black
		yield return (m_GM.GetComponent<Fading>().BeginFade(1));

		// Load loading screen
		yield return SceneManager.LoadSceneAsync("LoadingScreen");

		// !!! unload old screen (automatic)

		// Fade to loading screen
		yield return (m_GM.GetComponent<Fading>().BeginFade(-1));

		float endTime = Time.time + m_minDuration;

		// Load level async
		yield return SceneManager.LoadSceneAsync(sceneName);

		if (Time.time < endTime)

			yield return new WaitForSeconds(endTime - Time.time);

		// Load appropriate zone's music based on zone data
		MusicManagerScript.Instance.PlayMusic(music);

		// Fade to black
		yield return (m_GM.GetComponent<Fading>().BeginFade(1));

		// !!! unload loading screen
		LoadingScreenScript.Instance.UnloadLoadingScene();

		// Fade to new screen
		yield return (m_GM.GetComponent<Fading>().BeginFade(-1));

	}

}
