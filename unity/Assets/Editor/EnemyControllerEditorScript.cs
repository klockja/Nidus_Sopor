using UnityEditor;
using System.Collections;
using UnityEngine;

[CustomEditor(typeof(EnemyController)), CanEditMultipleObjects]
public class EnemyControllerEditorScript : Editor 
{
	public override void OnInspectorGUI() 
	{
		//Called whenever the inspector is drawn for this object.
		DrawDefaultInspector();
		//This draws the default screen.  You don't need this if you want
		//to start from scratch, but I use this when I'm just adding a button or
		//some small addition and don't feel like recreating the whole inspector.

		if(GUILayout.Button("Your ButtonText")) 
		{
			//add everthing the button would do.
		}
	}

	void OnSceneGUI()
	{
		EnemyController EC = (EnemyController)target;

		//Draws view reach
		Handles.color = Color.white;
		Handles.DrawWireArc (EC.EnemyHead.transform.position, Vector3.forward, Vector3.up, 360, EC.viewRadius);

		//Draws cone of view
		Vector3 viewAngleA = EC.DirFromAngle(-EC.viewAngle / 2, false);
		Vector3 viewAngleB = EC.DirFromAngle(EC.viewAngle / 2, false);
		Handles.DrawLine(EC.EnemyHead.transform.position, EC.EnemyHead.transform.position + viewAngleA * EC.viewRadius);
		Handles.DrawLine(EC.EnemyHead.transform.position, EC.EnemyHead.transform.position + viewAngleB * EC.viewRadius);

		Handles.color = Color.red;
		foreach (Transform visibleTarget in EC.visibleTargets)
		{
			Handles.DrawLine (EC.EnemyHead.transform.position, visibleTarget.position);
		}
	}

//	public SerializedProperty
//		SetBehavior_Prop,
		



//	void OnEnable () 
//	{
//		
//	}
}
