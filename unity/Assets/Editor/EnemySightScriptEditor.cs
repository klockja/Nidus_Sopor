using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (EnemySightScript))]
public class EnemySightScriptEditor : Editor {

	void OnSceneGUI() 
	{
		EnemySightScript ess = (EnemySightScript)target;

		//Draws view reach
		Handles.color = Color.white;
		Handles.DrawWireArc(ess.transform.position, Vector3.forward, Vector3.up, 360, ess.viewRadius);

		//Draws cone of view
		Vector3 viewAngleA = ess.DirFromAngle(-ess.viewAngle / 2, false);
		Vector3 viewAngleB = ess.DirFromAngle(ess.viewAngle / 2, false);
		Handles.DrawLine(ess.transform.position, ess.transform.position + viewAngleA * ess.viewRadius);
		Handles.DrawLine(ess.transform.position, ess.transform.position + viewAngleB * ess.viewRadius);

		Handles.color = Color.red;
		foreach (Transform visibleTarget in ess.visibleTargets)
		{
			Handles.DrawLine (ess.transform.position, visibleTarget.position);
		}
	}
}