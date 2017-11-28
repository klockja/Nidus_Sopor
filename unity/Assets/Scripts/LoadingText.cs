using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LoadingText : MonoBehaviour {

	private static float TIME_BETWEEN_ELIPSES = 0.35f;

	private static int NUM_ELIPSES = 3;

	private Text m_text;

	private float m_timeBetweenElipses = 0.0f;

	private int m_currentElipses = 0;

	private void Awake()
	{
		m_text = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_timeBetweenElipses -= Time.deltaTime;
		if(m_timeBetweenElipses <= 0.0f)
		{
			m_timeBetweenElipses = TIME_BETWEEN_ELIPSES;

			m_text.text = "LOADING";

			m_currentElipses++;
			if(m_currentElipses > NUM_ELIPSES)
			{
				m_currentElipses = 0;
			}

			for(int i=0; i<m_currentElipses; i++)
			{
				m_text.text += ".";
			}
		}
	}
}
