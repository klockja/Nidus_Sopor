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
	}

	// Use this for initialization
	void Start () 
	{
		Health = MaxHealth;	
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void Damage(float damage)
	{
		Debug.Log ("Took Damage");
		if (canBeHurt)
		{
			Health = Health - damage;
			gameObject.GetComponent<EnemyController>().GetComponent<AudioSource>().PlayOneShot (gameObject.GetComponent<EnemyController>().takeDamage);
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
