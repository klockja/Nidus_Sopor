using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnButtonScript : MonoBehaviour {

	[SerializeField] private Button _returnButton;

	[SerializeField] private AudioSource audioSource;

	[SerializeField] private AudioClip buttonPressSound;


	private void Awake()
	{
		_returnButton.onClick.AddListener (OnPlayButtonClicked);
	}

	void Update()
	{
		Debug.Log (SceneManager.GetActiveScene ().name);
	}

	private void OnPlayButtonClicked()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().name == "Beach") 
		{
			if (GameManagement.Instance.playerDeathNumber == 0) {
				audioSource.PlayOneShot (buttonPressSound);
				GameplayCanvasScript.Instance.switchToMenu (0);
				if (GameplayCanvasScript.Instance.BlackPanel.activeSelf == true)
					GameplayCanvasScript.Instance.BlackPanel.SetActive(false);

				else if (GameplayCanvasScript.Instance.BlackPanel.activeSelf == false)
					GameplayCanvasScript.Instance.BlackPanel.SetActive(true);

			}

		}

		else if (SceneManager.GetActiveScene().name != "Title" || SceneManager.GetActiveScene().name != "TItle" || SceneManager.GetActiveScene().name != "Beach"){

			audioSource.PlayOneShot (buttonPressSound);
			GameplayCanvasScript.Instance.switchToMenu (1);
			Debug.Log ("bye");
		}
	}
}
