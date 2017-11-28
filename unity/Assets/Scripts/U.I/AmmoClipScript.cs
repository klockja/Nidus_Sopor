using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoClipScript : MonoBehaviour {

	public GameObject[] m_ammoClips;

	public int ammoNum = -1;

	private PlayerController m_playerController;

	// Use this for initialization
	void Start()
	{
		SearchForPlayer();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_playerController ==  null)
		{
			SearchForPlayer();
			return;
		}

		int nextAmmoNum = (int)m_playerController.unusedBulletNum;
		if(nextAmmoNum != ammoNum)
		{
			switchAmmoClip (nextAmmoNum);
			ammoNum = nextAmmoNum;
		}
	}

	private void SearchForPlayer()
	{
		GameObject gameObject = GameObject.Find ("Player");
		if(gameObject != null)
		{
			m_playerController = gameObject.GetComponent<PlayerController>();
		}
	}

	private void switchAmmoClip(int num) {

		int numAmmoClips = m_ammoClips.Length;
		for(int i=0; i<numAmmoClips; i++)
		{
			m_ammoClips[i].SetActive(num > i);
		}
	}
}
