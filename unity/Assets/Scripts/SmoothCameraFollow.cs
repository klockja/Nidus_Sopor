using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
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

	private Camera m_camera;

	private void Awake()
	{
		m_camera = this.GetComponent<Camera> ();
	}

	private void LateUpdate()
	{
		Vector3 desiredPosition = m_target.transform.position + m_targetOffset;
		Vector3 position = Vector3.Lerp(this.transform.position, desiredPosition, Time.deltaTime * m_speed);

		float halfHeight = m_camera.orthographicSize;
		float halfWidth = halfHeight * Screen.width/Screen.height;

		position.x = Mathf.Clamp (position.x, m_minBounds.x + halfWidth, m_maxBounds.x - halfWidth);
		position.y = Mathf.Clamp (position.y, m_minBounds.y + halfHeight, m_maxBounds.y - halfHeight);

		this.transform.position = position;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Vector3 position = new Vector3 ((m_minBounds.x + m_maxBounds.x) / 2.0f, (m_minBounds.y + m_maxBounds.y) / 2.0f, 0.0f);
		Vector3 size = new Vector3 (m_maxBounds.x - m_minBounds.x, m_maxBounds.y - m_minBounds.y, 1);
		Gizmos.DrawWireCube (position, size);
	}
}
