using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoClipScript : MonoBehaviour {

	public GameObject ammoClip1;
	public GameObject ammoClip2;
	public GameObject ammoClip3;
	public GameObject ammoClip4;
	public GameObject ammoClip5;
	public GameObject ammoClip6;
	public GameObject ammoClip7;
	public GameObject ammoClip8;
	public GameObject ammoClip9;
	public GameObject ammoClip10;
	public GameObject ammoClip11;
	public GameObject ammoClip12;

	public int ammoNum;

	// Use this for initialization
	void start()
	{

	}
	
	// Update is called once per frame
	void Update () {
		ammoNum = (int)GameObject.Find ("Player").GetComponent<PlayerController> ().unusedBulletNum;
		switchAmmoClip (ammoNum);
	}

	private void switchAmmoClip(int num) {

		ammoClip1.SetActive (false);
		ammoClip2.SetActive (false);
		ammoClip3.SetActive (false);
		ammoClip4.SetActive (false);
		ammoClip5.SetActive (false);
		ammoClip6.SetActive (false);
		ammoClip7.SetActive (false);
		ammoClip8.SetActive (false);
		ammoClip9.SetActive (false);
		ammoClip10.SetActive (false);
		ammoClip11.SetActive (false);
		ammoClip12.SetActive (false);

		switch (num) {
		case 0:
			break;
		case 1:
			ammoClip1.SetActive(true);
			break;
		case 2:
			ammoClip1.SetActive(true);
			ammoClip2.SetActive(true);
			break;
		case 3:
			ammoClip1.SetActive(true);
			ammoClip2.SetActive(true);
			ammoClip3.SetActive(true);
			break;
		case 4:
			ammoClip1.SetActive(true);
			ammoClip2.SetActive(true);
			ammoClip3.SetActive(true);
			ammoClip4.SetActive(true);
			break;
		case 5:
			ammoClip1.SetActive(true);
			ammoClip2.SetActive(true);
			ammoClip3.SetActive(true);
			ammoClip4.SetActive(true);
			ammoClip5.SetActive(true);
			break;
		case 6:
			ammoClip1.SetActive(true);
			ammoClip2.SetActive(true);
			ammoClip3.SetActive(true);
			ammoClip4.SetActive(true);
			ammoClip5.SetActive(true);
			ammoClip6.SetActive(true);
			break;
		case 7:
			ammoClip1.SetActive(true);
			ammoClip2.SetActive(true);
			ammoClip3.SetActive(true);
			ammoClip4.SetActive(true);
			ammoClip5.SetActive(true);
			ammoClip6.SetActive(true);
			ammoClip7.SetActive(true);
			break;
		case 8:
			ammoClip1.SetActive(true);
			ammoClip2.SetActive(true);
			ammoClip3.SetActive(true);
			ammoClip4.SetActive(true);
			ammoClip5.SetActive(true);
			ammoClip6.SetActive(true);
			ammoClip7.SetActive(true);
			ammoClip8.SetActive(true);
			break;
		case 9:
			ammoClip1.SetActive(true);
			ammoClip2.SetActive(true);
			ammoClip3.SetActive(true);
			ammoClip4.SetActive(true);
			ammoClip5.SetActive(true);
			ammoClip6.SetActive(true);
			ammoClip7.SetActive(true);
			ammoClip8.SetActive(true);
			ammoClip9.SetActive(true);
			break;
		case 10:
			ammoClip1.SetActive(true);
			ammoClip2.SetActive(true);
			ammoClip3.SetActive(true);
			ammoClip4.SetActive(true);
			ammoClip5.SetActive(true);
			ammoClip6.SetActive(true);
			ammoClip7.SetActive(true);
			ammoClip8.SetActive(true);
			ammoClip9.SetActive(true);
			ammoClip10.SetActive(true);
			break;
		case 11:
			ammoClip1.SetActive(true);
			ammoClip2.SetActive(true);
			ammoClip3.SetActive(true);
			ammoClip4.SetActive(true);
			ammoClip5.SetActive(true);
			ammoClip6.SetActive(true);
			ammoClip7.SetActive(true);
			ammoClip8.SetActive(true);
			ammoClip9.SetActive(true);
			ammoClip10.SetActive(true);
			ammoClip11.SetActive(true);
			break;
		case 12:
			ammoClip1.SetActive(true);
			ammoClip2.SetActive(true);
			ammoClip3.SetActive(true);
			ammoClip4.SetActive(true);
			ammoClip5.SetActive(true);
			ammoClip6.SetActive(true);
			ammoClip7.SetActive(true);
			ammoClip8.SetActive(true);
			ammoClip9.SetActive(true);
			ammoClip10.SetActive(true);
			ammoClip11.SetActive(true);
			ammoClip12.SetActive(true);
			break;
		}
	}
}
