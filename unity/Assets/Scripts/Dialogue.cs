using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class Dialogue : MonoBehaviour 
{
	private Text _textComponent;

	public Image CurrentCharacterPortrait;
	public string[] CharacterNames;
	public Sprite[] CharacterPortraits;

	public string[] DialogueStrings;

	[Header ("Sound")]

	public AudioClip TypingSound;
	public AudioClip EndSound;
	public AudioClip NextDialogueSound;
	private AudioSource AudioSource;

	[Header ("Timing")]

	public float StartDialogueDelay;
	public float SecondsBetweenCharacters = 0.075f;
	public float CharacterRateMultiplier = 0.5f;

	public KeyCode DialogueInput = KeyCode.Return;

	private bool _isStringBeingRevealed = false;
	private bool _isDialoguePlaying = false;
	private bool _isEndOfDialogue = false;
	public bool DialogueEnded = false;

	private bool _canEndDialogueEarly = true;

	public string NextScene;
	public float NextSceneDelay;

	private int DialogueContinueWasCalled = 0;
	private bool MultipleDialogueContinues = false;

	public GameObject ContinueIcon;
	public GameObject StopIcon;

	// Use this for initialization
	void Start () 
	{
		_textComponent = GetComponent<Text> ();
		_textComponent.text = "";

		CurrentCharacterPortrait.enabled = false;

		AudioSource = GetComponent <AudioSource> ();

		_isDialoguePlaying = true;
		StartCoroutine (StartDialogue ());

		HideIcons ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (DialogueInput)) 
		{
			if (!_isDialoguePlaying) 
			{
				_isDialoguePlaying = true;
				StartCoroutine (StartDialogue ());
			}
		}

		if (_isEndOfDialogue && (Input.GetKeyDown(DialogueInput)))
		{
			DialogueEnded = true;
			Debug.Log ("Go to next scene from end of dialogue");
			if (NextDialogueSound != null)
			{
				AudioSource.PlayOneShot (NextDialogueSound);
			}
//			_isEndOfDialogue = false;
//			_isDialoguePlaying = false;
			DialogBox.Hide ();
//			if (activatesAnimation &&)
//			{
//
//			}

			StartCoroutine (LoadNextScene (NextScene, NextSceneDelay));
		}
	}

	private IEnumerator StartDialogue()
	{
		yield return new WaitForSeconds (StartDialogueDelay);
		CurrentCharacterPortrait.enabled = true;
		int dialogueLength = DialogueStrings.Length;
		int currentDialogueIndex = 0;

		while (currentDialogueIndex < dialogueLength || !_isStringBeingRevealed)
		{
			if (!_isStringBeingRevealed)
			{
				_isStringBeingRevealed = true;
				StartCoroutine (DisplayString (DialogueStrings [currentDialogueIndex++]));

				if (currentDialogueIndex >= dialogueLength) 
				{
					_isEndOfDialogue = true;
				}
			}

			yield return 0;
		}

		while (true)
		{
			if (Input.GetKeyDown(DialogueInput))
			{
				break;
			}
			if (Input.GetKeyDown(KeyCode.Return))
			{
				break;
			}

			else

			yield return 0;
		}

		HideIcons ();
		_isEndOfDialogue = false;

		_isDialoguePlaying = false;
	}

	private IEnumerator DisplayString(string stringToDisplay)
	{
		int stringLength = stringToDisplay.Length;
		int currentCharacterIndex = 0;

		HideIcons ();

		_textComponent.text = "";

		for (int i = 0; i < CharacterNames.Length; i++)
		{
			if (stringToDisplay.Contains (CharacterNames[i] + ":"))
			{
				CurrentCharacterPortrait.sprite = CharacterPortraits[i];
			}
		}

		while (currentCharacterIndex < stringLength) 
		{
			_textComponent.text += stringToDisplay [currentCharacterIndex];
			currentCharacterIndex++;

			if (currentCharacterIndex < stringLength) 
			{
				if (TypingSound != null)
				{
					AudioSource.PlayOneShot (TypingSound);
				}

				if (_canEndDialogueEarly && Input.GetKeyDown (DialogueInput))
				{
					Debug.Log ("Input was pressed during dialogue");
					_textComponent.text = stringToDisplay;
					currentCharacterIndex = stringLength;
					yield return new WaitForSeconds (SecondsBetweenCharacters);
				}

				if (Input.GetKeyDown (DialogueInput) == false)
				{
					yield return new WaitForSeconds (SecondsBetweenCharacters);
				}
//				if (Input.GetKey (DialogueInput)) 
//				{
//					yield return new WaitForSeconds (SecondsBetweenCharacters * CharacterRateMultiplier);
//				} 
//				else 
//				{
//					yield return new WaitForSeconds (SecondsBetweenCharacters);
//				}
			} 
			else 
			{
				break;
			}
		}

		ShowIcons ();

		while (true) 
		{
			//Continues text
			if (Input.GetKeyDown (DialogueInput)) 
			{
				if (NextDialogueSound != null)
				{
					AudioSource.PlayOneShot (NextDialogueSound);
				}
				break;
			}

			if (DialogueContinueWasCalled > 0) 
			{
				if (DialogueContinueWasCalled > 1 || MultipleDialogueContinues == true) 
				{
					Debug.Log("Dialogue Continued, even after the user pressed multiple times.");
					yield return new WaitForSeconds(2f);
					DialogueContinueWasCalled -= 1;
					MultipleDialogueContinues = true;
					if (DialogueContinueWasCalled == 0)
					{
						MultipleDialogueContinues = false;
					}
					break;
				}
				else
				{
				Debug.Log("Dialogue Continued");
				Debug.Log("DialogueContinueWasCalled = " + DialogueContinueWasCalled);
				DialogueContinueWasCalled -= 1;
				Debug.Log("DialogueContinueWasCalled = " + DialogueContinueWasCalled);
				break;
				}
			}
//			else
//			{
//				yield return new WaitForSeconds(2);
//				break;
//			}

			yield return 0;
		}

		HideIcons ();

		_isStringBeingRevealed = false;
		_textComponent.text = "";
	}

	private IEnumerator LoadNextScene(string scene, float delay)
	{
		yield return new WaitForSeconds (delay);
		GameplayCanvasScript.Instance.LoadSceneNow (NextScene);
	}

	private void HideIcons()
	{
		ContinueIcon.SetActive (false);
		StopIcon.SetActive(false);
	}

	private void ShowIcons()
	{
		if (_isEndOfDialogue) 
		{
			StopIcon.SetActive (true);
			if (EndSound != null)
			{
				AudioSource.PlayOneShot (EndSound);
			}
			return;
		}

		if (ContinueIcon.activeSelf == false)
		{
			ContinueIcon.SetActive (true);
			if (EndSound != null)
			{
				AudioSource.PlayOneShot (EndSound);
			}
		}

	}

	public void DialogueStart()
	{
		_isDialoguePlaying = true;
		StartCoroutine (StartDialogue ());
	}
	public void DialogueContinue()
	{
		DialogueContinueWasCalled += 1;
	}
}