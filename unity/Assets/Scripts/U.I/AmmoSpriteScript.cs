using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AmmoSpriteScript : MonoBehaviour {

	public Sprite Ammo0;
	public Sprite Ammo1;
	public Sprite Ammo2;
	public Sprite Ammo3;
	public Sprite Ammo4;
	public Sprite Ammo5;
	public Sprite Ammo6;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		AmmoSpriteChange ();
	}

	private void AmmoSpriteChange()
	{
		if (GameObject.Find ("Player").GetComponent<PlayerController> ().bulletNum == 0) {
			gameObject.GetComponent<Image> ().sprite = Ammo0;
		}

		else if (GameObject.Find ("Player").GetComponent<PlayerController> ().bulletNum == 1) {
			gameObject.GetComponent<Image> ().sprite = Ammo1;
		}
			
		else if (GameObject.Find ("Player").GetComponent<PlayerController> ().bulletNum == 2) {
			gameObject.GetComponent<Image> ().sprite = Ammo2;
		}

		else if (GameObject.Find ("Player").GetComponent<PlayerController> ().bulletNum == 3) {
			gameObject.GetComponent<Image> ().sprite = Ammo3;
		}

		else if (GameObject.Find ("Player").GetComponent<PlayerController> ().bulletNum == 4) {
			gameObject.GetComponent<Image> ().sprite = Ammo4;
		}

		else if (GameObject.Find ("Player").GetComponent<PlayerController> ().bulletNum == 5) {
			gameObject.GetComponent<Image> ().sprite = Ammo5;
		}

		else if (GameObject.Find ("Player").GetComponent<PlayerController> ().bulletNum == 6) {
			gameObject.GetComponent<Image> ().sprite = Ammo6;
		}
		
	}
}
