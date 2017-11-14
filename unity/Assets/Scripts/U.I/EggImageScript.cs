using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggImageScript : MonoBehaviour {


	public Sprite egg;
	public Sprite noEgg;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Image> ().sprite = noEgg;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManagement.Instance.hasegg == true) {
			gameObject.GetComponent<Image> ().sprite = egg;
		}
	}
}
