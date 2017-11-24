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

	public float SecondsBetweenCharacters = 0.075f;
	private float OriginalSecondsBetweenCharacters;
	public float CharacterRateMultiplier = 0.5f;

	public KeyCode DialogueInput = KeyCode.Return;

	private bool _isStringBeingRevealed = false;
	private bool _isDialoguePlaying = false;
	private bool _isEndOfDialogue = false;

	private bool _canEndDialogueEarly = true;

	public string NextScene;

	private int DialogueContinueWasCalled = 0;
	private bool MultipleDialogueContinues = false;

	public GameObject ContinueIcon;
	public GameObject StopIcon;

	// Use this for initialization
	void Start () 
	{
		_textComponent = GetComponent<Text> ();
		_textComponent.text = "";

		_isDialoguePlaying = true;
		StartCoroutine (StartDialogue ());

		HideIcons ();

		OriginalSecondsBetweenCharacters = SecondsBetweenCharacters;
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
			Debug.Log ("Go to next scene from end of dialogue");
			_isEndOfDialogue = false;
			_isDialoguePlaying = false;
			GameplayCanvasScript.Instance.IntroCutscenePanel.SetActive (false);
			GameplayCanvasScript.Instance.LoadSceneNow (NextScene);
		}
	}

	private IEnumerator StartDialogue()
	{
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
			return;
		}

		ContinueIcon.SetActive (true);
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