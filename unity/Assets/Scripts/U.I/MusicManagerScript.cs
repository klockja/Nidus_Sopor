using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript: GenericSingletonClass<MusicManagerScript> {

	public AudioSource musicPlayer;

	public AudioClip titleMusic;
	public AudioClip beachMusic;
	public AudioClip forestMusic;
	public AudioClip caveMusic;
	public AudioClip escapeMusic;

	// Use this for initialization
	void Start () {
		musicPlayer.clip = titleMusic;
		musicPlayer.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayMusic(string m)
	{
		if (m == beachMusic.ToString ()) {
			musicPlayer.clip = beachMusic;
			musicPlayer.Play ();
		}

		else if (m == forestMusic.ToString ()) {
			musicPlayer.clip = forestMusic;
			musicPlayer.Play ();
		}

		else if (m == caveMusic.ToString ()) {
			musicPlayer.clip = caveMusic;
			musicPlayer.Play ();
		}

		else if (m == escapeMusic.ToString ()) {
			musicPlayer.clip = escapeMusic;
			musicPlayer.Play ();
		}

		else if (m == titleMusic.ToString ()) {
			musicPlayer.clip = titleMusic;
			musicPlayer.Play ();
		}
	}
}
