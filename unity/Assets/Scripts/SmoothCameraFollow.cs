using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour {

	[SerializeField]
	private Transform m_target;

	[SerializeField]
	private Vector3 m_targetOffset;

	[SerializeField]
	private float m_speed;

	[SerializeField]
	private Vector2 m_minBounds;

	[SerializeField]
	private Vector2 m_maxBounds;

	private void LateUpdate()
	{
		Vector3 desiredPosition = m_target.transform.position + m_targetOffset;
		Vector3 position = Vector3.Lerp(this.transform.position, desiredPosition, Time.deltaTime * m_speed);

		position.x = Mathf.Clamp (position.x, m_minBounds.x, m_maxBounds.x);
		position.y = Mathf.Clamp (position.y, m_minBounds.y, m_maxBounds.y);

		this.transform.position = position;
	}
}
