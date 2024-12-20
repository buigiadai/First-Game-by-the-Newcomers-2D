using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDead : MonoBehaviour
{
	public float maxHealth;
	float currentHealth;
	public GameObject bloodEffect;

	void Start()
	{
		currentHealth = maxHealth;
	}

	void Update()
	{

	}
	public void addDamage(float damage)
	{
		if (damage <= 0)
			return;
		currentHealth -= damage;
		if (currentHealth <= 0)
			makeDead();
	}
	void makeDead()
	{
		Instantiate(bloodEffect, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
