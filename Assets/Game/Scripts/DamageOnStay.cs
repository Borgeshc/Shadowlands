using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnStay : MonoBehaviour 
{
	public int damage;
	bool dealingDamage;

	void OnTriggerStay(Collider other)
	{
        if(other.transform.GetComponent<Health>() != null && other.transform.GetComponent<Health>().health > 0 && gameObject.activeInHierarchy)
        {
            if (other.tag == "Enemy" || other.tag == "Player")
            {
                if (!dealingDamage)
                {
                    dealingDamage = true;
                    StartCoroutine(DealDamage(other.gameObject));
                }
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
