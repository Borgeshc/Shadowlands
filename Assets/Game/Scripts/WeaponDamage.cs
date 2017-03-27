using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour 
{
	public int damage;
	bool dealingDamage;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			if (!dealingDamage) 
			{
				dealingDamage = true;
				StartCoroutine (DealDamage (other.gameObject));
			}
		}
	}

	IEnumerator DealDamage(GameObject enemy)
	{
		enemy.GetComponent<Health> ().TookDamage (damage);
		yield return new WaitForSeconds (1);
		dealingDamage = false;
	}
}
