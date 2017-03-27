using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaveDamage : MonoBehaviour 
{
	public int minDamage;
	public int maxDamage;
	public int criticalNumber;
	int damage;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy" || other.tag == "Dragon") 
		{
			other.GetComponent<Health> ().TookDamage (CritChance(),damage);
		}
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
