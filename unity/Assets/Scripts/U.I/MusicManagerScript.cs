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

	// Use this for initialization
	void Start () {
		musicPlayer.clip = title;
		musicPlayer.Play();
		currentScene = SceneManager.GetActiveScene ().name;
	}
	
	// Update is called once per frame
	void Update () {
		newScene = SceneManager.GetActiveScene ().name;

		if (currentScene != newScene) {
			if (SceneManager.GetActiveScene ().name != "LoadingScreen") {

				if (SceneManager.GetActiveScene ().name == "Title Scene") {
					musicPlayer.clip = title;
					musicPlayer.Play ();
					Debug.Log ("play title music");
				} else if (SceneManager.GetActiveScene ().name == "Beach") {
					musicPlayer.clip = beach;
					musicPlayer.Play ();
				} else if (SceneManager.GetActiveScene ().name == "Forest") {
					musicPlayer.clip = forest;
					musicPlayer.Play ();
				} else if (SceneManager.GetActiveScene ().name == "Cave") {
					musicPlayer.clip = cave;
					musicPlayer.Play ();
				} else if (GameObject.Find ("Player").GetComponent<PlayerController> ().hasEgg == true) {
					musicPlayer.clip = escape;
					musicPlayer.Play ();
				} else if (ui.Instance.DefeatPanel.activeSelf == true || ui.Instance.AreYouSurePanel.activeSelf == true) {
					musicPlayer.clip = gameover;
					musicPlayer.Play ();
				} else if (ui.Instance.VictoryPanel.activeSelf == true) {
					musicPlayer.clip = epilogue;
					musicPlayer.Play ();
				}
			}
			currentScene = newScene;
		}
	


	}
}
