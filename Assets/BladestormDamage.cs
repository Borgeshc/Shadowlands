﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladestormDamage : MonoBehaviour 
{
	public int minDamage;
	public int maxDamage;
	public int criticalNumber;
	int damage;

	bool dealingDamage;

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.GetComponent<Health> () != null && other.transform.GetComponent<Health> ().health > 0 && gameObject.activeInHierarchy) 
		{
			if (other.tag == "Enemy" || other.tag == "Dragon") 
			{
				DealDamage (other.gameObject);
			}
		}
	}

	void DealDamage(GameObject enemy)
	{
		enemy.GetComponent<Health> ().TookDamage (CritChance (), damage);
	}

	bool CritChance()
	{
		int damageAmount = Random.Range (minDamage, maxDamage);
		damage = damageAmount; 
		if (damageAmount < criticalNumber)
			return false;
		else
			return true;
	}
}
