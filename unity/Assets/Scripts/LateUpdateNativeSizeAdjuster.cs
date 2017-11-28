using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LateUpdateNativeSizeAdjuster : MonoBehaviour {

	private Image m_image;

	private void Awake()
	{
		m_image = this.GetComponent<Image>();
	}

	void LateUpdate()
	{
		m_image.SetNativeSize();
	}
}
