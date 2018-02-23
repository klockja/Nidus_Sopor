using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySightScript : MonoBehaviour 
{
	private EnemyController _enemyController;

	public bool isOnScreen;

	public int viewRadius;
	[Range(0, 360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

//	public MeshFilter viewMeshFilter;
//	Mesh viewMesh;
//	public float meshResolution;

//	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	private SpriteRenderer visuals;

	void Start()
	{
		visuals = this.gameObject.GetComponentInChildren<SpriteRenderer>();
		_enemyController = GetComponentInParent <EnemyController> ();
		StartCoroutine("FindTargetWithDelay", 0.2f);
	}

	void Update()
	{
//		DrawFieldOfView ();
	}

	IEnumerator FindTargetWithDelay(float delay) 
	{
		yield return new WaitForSeconds(delay);
		FindVisibleTargets();
	}

	//Finds targets inside field of view not blocked by walls
	void FindVisibleTargets() 
	{
		if(visuals.isVisible)
		{
			Debug.Log ("FINDING VISIBLE TARGETS");
			visibleTargets.Clear();
			//Adds targets in view radius to an array
			Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
			//For every targetsInViewRadius it checks if they are inside the field of view
			for (int i = 0; i < targetsInViewRadius.Length; i++) {
				Transform target = targetsInViewRadius[i].transform;
				Vector3 dirToTarget = (target.position - transform.position).normalized;
				if (Vector3.Angle(transform.up, dirToTarget) < viewAngle / 2) {
					float dstToTarget = Vector3.Distance(transform.position, target.position);
					//If line draw from object to target is not interrupted by wall, add target to list of visible 
					//targets
					if (!Physics2D.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask))
					{
						Debug.DrawRay (transform.position, dirToTarget, Color.blue);
						Debug.Log ("SAW PLAYER");
						visibleTargets.Add (target);
						_enemyController.lastSensedPlayerPosition = target.position;
						if (_enemyController.playerSensed == false)
						{
							StartCoroutine(_enemyController.PlaySound (_enemyController.growl, 0f));
							_enemyController.playerSensed = true;
						}
					}
					else
					{
						
					}
				}
			}
		}
		StartCoroutine("FindTargetWithDelay", 0.2f);
	}

	void DrawFieldOfView()
	{
//		int stepCount = Mathf.RoundToInt (viewAngle * meshResolution);
//		float stepAngleSize = viewAngle / stepCount;
//		List<Vector3> viewPoints = new List<Vector3> ();
//		for (int i = 0; i <= stepCount; i++)
//		{
//			float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
//			ViewCastInfo newViewCast = ViewCast (angle); 
//			viewPoints.Add (newViewCast.point);
//		}
//
//		int vertexCount = viewPoints.Count + 1;
//		Vector3[] vertices = new Vector3[vertexCount];
//		int[] triangles = new int[(vertexCount-2) * 3];
//
//		vertices [0] = Vector3.zero;
//		for (int i = 0; i < vertexCount - 1; i++)
//		{
//			vertices [i + 1] = transform.InverseTransformPoint (viewPoints [i]);
//			if (i < vertexCount - 2)
//			{
//				triangles [i * 3] = 0;
//				triangles [i * 3 + 1] = i + 1;
//				triangles [i * 3 + 2] = i + 2;
//			}
//		}

//		viewMesh.Clear ();
//		viewMesh.vertices = vertices;
//		viewMesh.triangles = triangles;
//		viewMesh.RecalculateNormals ();
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) 
	{
		if (!angleIsGlobal) 
		{
			angleInDegrees -= transform.eulerAngles.z;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
	}

	ViewCastInfo ViewCast(float globalAngle)
	{
		Vector3 dir = DirFromAngle (globalAngle, true);
		RaycastHit2D hit;
		hit = Physics2D.Raycast(transform.position, -Vector2.up);
		if (Physics2D.Raycast (transform.position, dir, hit.distance, viewRadius, obstacleMask))
		{
			return new ViewCastInfo (true, hit.point, hit.distance, globalAngle);
		} else
		{
			return new ViewCastInfo (false, transform.position + dir * viewRadius, viewRadius, globalAngle);
		}
	}

	public struct ViewCastInfo
	{
		public bool hit;
		public Vector3 point;
		public float dst;
		public float angle;

		public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
		{
			hit = _hit;
			point = _point;
			dst = _dst;
			angle = _angle;
		}
	}

	public IEnumerator LosePlayer (float seconds)
	{
//		yield return new WaitForSeconds (seconds);
		visibleTargets.Clear ();
		yield return null;
	}
}