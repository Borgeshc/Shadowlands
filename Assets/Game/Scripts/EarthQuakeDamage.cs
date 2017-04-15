using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuakeDamage : MonoBehaviour 
{
	public int minDamage;
	public int maxDamage;
	public int criticalNumber;
	public int damage;
	bool dealingDamage;

	void OnTriggerEnter(Collider other)
	{
		if(other.transform.GetComponent<Health>() != null && other.transform.GetComponent<Health>().health > 0 && gameObject.activeInHierarchy)
		{
			if (other.tag == "Enemy" || other.tag == "Dragon")
			{
				StartCoroutine(DealDamage(other.gameObject));
			}
		}
	}

	IEnumerator DealDamage(GameObject enemy)
	{
		for (int i = 0; i < 5; i++) {
			enemy.GetComponent<Health> ().TookDamage (CritChance (), damage);
			yield return new WaitForSeconds (1);
		}
		dealingDamage = false;
	}

	bool CritChance()
	{
		int damageAmount = Random.Range (minDamage, maxDamage);
		damage = damageAmount + PlayerPrefs.GetInt("Damage");

        int checkForCrit = Random.Range(0, 100);
        if (checkForCrit <= PlayerPrefs.GetInt("CritChance"))
        {
            damage = damage * 2;
            return true;
        }
        else
            return false;
    }
}
