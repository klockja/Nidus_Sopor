using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreYouSureButton : MonoBehaviour {

	[SerializeField] private Button _noButton;

	[SerializeField] private AudioSource audioSource;

	[SerializeField] private AudioClip buttonPressSound;


	private void Awake()
	{
		_noButton.onClick.AddListener (OnPlayButtonClicked);
	}

	void Update()
	{
		
	}

	private void OnPlayButtonClicked()
	{
		if (GameManagement.Instance.isPlayerDead == false) {

			audioSource.PlayOneShot (buttonPressSound);
			GameManagement.Instance.isPaused = false;
			GameplayCanvasScript.Instance.TurnOnBlackScreen ();
			GameplayCanvasScript.Instance.AreYouSurePanel.SetActive (false);

		}

		if (GameManagement.Instance.isPlayerDead == true) {

			audioSource.PlayOneShot (buttonPressSound);
			GameplayCanvasScript.Instance.ReloadScene ();
			GameplayCanvasScript.Instance.TurnOnBlackScreen ();

		}
	}
}
