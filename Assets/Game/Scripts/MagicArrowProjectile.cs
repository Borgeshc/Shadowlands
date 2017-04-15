using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowProjectile : MonoBehaviour 
{
	public float projectileSpeed;
	public int minDamage;
	public int maxDamage;
	public int criticalNumber;
    public GameObject target;
	int damage;

	void Start()
	{	
		StartCoroutine (DeActivate ());
	}

	void Update () 
	{
		if(TargetObject.target != null)
		{
			transform.LookAt (new Vector3(target.transform.position.x, target.transform.position.y + (target.transform.localScale.y * .5f), target.transform.position.z));
			transform.Translate (Vector3.forward * projectileSpeed * Time.deltaTime);
		}
		else
			transform.Translate (Vector3.forward * projectileSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
            if(!other.GetComponent<ProximityAI>().IsSlowed())
            {
                StartCoroutine(other.GetComponent<ProximityAI>().Slowed());
            }
			other.GetComponent<Health> ().TookDamage (CritChance(),damage);
			gameObject.SetActive (false);
		}
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

	IEnumerator DeActivate()
	{
		yield return new WaitForSeconds (5);
		gameObject.SetActive (false);
	}
}
