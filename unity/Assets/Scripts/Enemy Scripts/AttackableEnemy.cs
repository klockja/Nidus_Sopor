using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableEnemy : MonoBehaviour 
{
	private bool canBeHurt = true;

	private float MaxHealth;
	public float Health;


	void Awake()
	{
//		Health = MaxHealth;
	}

	// Use this for initialization
	void Start () 
	{
		Health = MaxHealth;	
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void OnHit()
	{
		
		if (Health <= 0) 
		{
			
		}
	}

	public void SetMaxHealth(float newMaxHealth)
	{
		MaxHealth = newMaxHealth;
	}

	public float GetHealth()
	{
		return Health;
	}
}
