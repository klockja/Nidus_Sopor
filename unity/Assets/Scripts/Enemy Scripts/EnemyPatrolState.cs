using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyState 
{
	Vector2[] waypoints;

	float walkSpeed;

	public EnemyPatrolState(EnemyStateMachine machine):base(machine)
	{
	}

	override public void OnEnter()
	{
		walkSpeed = m_machine.Controller.walkSpeed;
		Patrol ();
	}

	override public void Patrol()
	{
		waypoints = new Vector2[m_machine.Controller.PatrolPath.childCount];
		for (int i = 0; i < waypoints.Length; i++)
		{
			waypoints [i] = m_machine.Controller.PatrolPath.GetChild (i).position;
		}
		m_machine.Controller.StartChildCoroutine (FollowPath());

	}

	IEnumerator FollowPath()
	{
		m_machine.Controller.transform.position = waypoints [0];

		int targetWaypointIndex = 1;
		Vector3 targetWaypoint = waypoints [targetWaypointIndex];

		while (true)
		{
			m_machine.Controller.M_Body.position = Vector2.MoveTowards (m_machine.Controller.transform.position, targetWaypoint, walkSpeed * Time.deltaTime);
			//			m_machine.Controller.M_Body.MovePosition (targetWaypoint * (walkSpeed * Time.deltaTime));
			//			= Vector2.MoveTowards (m_machine.Controller.transform.position, targetWaypoint, walkSpeed * Time.deltaTime)
			if (m_machine.Controller.transform.position == targetWaypoint)
			{
				targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
				targetWaypoint = waypoints [targetWaypointIndex];
				yield return new WaitForSeconds (m_machine.Controller.patrolWaitTime);
			}
			yield return null;
		}
	}

	override public void OnExit()
	{
		//COULD NOT FIGURE OUT HOW TO STOP ONE COROUTINE, SO I HAD TO STOP ALL OF THEM
//		m_machine.Controller.StopAllCoroutines ();
		m_machine.Controller.StopChildCoroutine (FollowPath ());
		//		m_machine.Controller.StopChildCoroutine (FollowPath());
		Debug.Log ("Exited Patrol State");
	}
}