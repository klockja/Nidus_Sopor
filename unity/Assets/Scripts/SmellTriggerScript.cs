using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellTriggerScript : MonoBehaviour 
{
	public float destructionDelay;

	void Update()
	{
		destructionDelay = destructionDelay - Time.deltaTime;

		if (destructionDelay <= 0)
		{
			Destroy (gameObject);
		}
	}
}
