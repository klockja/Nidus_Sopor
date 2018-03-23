using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManagerScript: GenericSingletonClass<MusicManagerScript> {

	public AudioSource musicPlayer;
	public AudioClip title;
	public AudioClip beach;
	public AudioClip forest;
	public AudioClip cave;
	public AudioClip escape;
	public AudioClip gameover;
	public AudioClip epilogue;

	private string currentScene;
	private string newScene;

	private float currentDeathNumber;
	private float newDeathNumber;

	// Use this for initialization
	void Start () {
		musicPlayer.clip = title;
		musicPlayer.Play();
		currentScene = SceneManager.GetActiveScene ().name;
		currentDeathNumber = GameManagement.Instance.playerDeathNumber;
	}
	
	// Update is called once per frame
	void Update () {
		newScene = SceneManager.GetActiveScene ().name;
		newDeathNumber = GameManagement.Instance.playerDeathNumber;

		if (currentScene != newScene || currentDeathNumber != newDeathNumber) {
			MusicSelector ();
			currentScene = newScene;
			currentDeathNumber = newDeathNumber;
		}
	}

	private void MusicSelector()
	{

		if (SceneManager.GetActiveScene ().name != "LoadingScreen") {
			
			if (SceneManager.GetActiveScene ().name == "Title Scene" || SceneManager.GetActiveScene ().name == "TItle Scene") {
				musicPlayer.clip = title;
				musicPlayer.Play ();
			} else if (SceneManager.GetActiveScene ().name == "Beach" && GameManagement.Instance.hasegg == false) {
				musicPlayer.clip = beach;
				musicPlayer.Play ();
			} else if (SceneManager.GetActiveScene ().name == "Forest1-1" && GameManagement.Instance.hasegg == false) {
				musicPlayer.clip = forest;
				musicPlayer.Play ();
			} else if (SceneManager.GetActiveScene ().name == "Forest1-2" && GameManagement.Instance.hasegg == false && GameManagement.Instance.playerDeathNumber != 0) {
				musicPlayer.clip = forest;
				musicPlayer.Play ();
			} else if (SceneManager.GetActiveScene ().name == "Forest1-3" && GameManagement.Instance.hasegg == false && GameManagement.Instance.playerDeathNumber != 0) {
				musicPlayer.clip = forest;
				musicPlayer.Play ();
			} else if (SceneManager.GetActiveScene ().name == "Forest1-4" && GameManagement.Instance.hasegg == false && GameManagement.Instance.playerDeathNumber != 0) {
				musicPlayer.clip = forest;
				musicPlayer.Play ();
			} else if (SceneManager.GetActiveScene ().name == "Cave" && GameManagement.Instance.hasegg == false) {
				musicPlayer.clip = cave;
				musicPlayer.Play ();
			} else if (SceneManager.GetActiveScene ().name == "Cave2" && GameManagement.Instance.hasegg == false) {
				musicPlayer.clip = cave;
				musicPlayer.Play ();
			} else if (SceneManager.GetActiveScene ().name == "Forest2" && GameManagement.Instance.hasegg == true) {
				musicPlayer.clip = escape;
				musicPlayer.Play ();
			}
		}

	}
}
