using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowProjectile : MonoBehaviour 
{
	public float projectileSpeed;
	public int minDamage;
	public int maxDamage;
	public int criticalNumber;
	int damage;

	void Start()
	{	
		StartCoroutine (DeActivate ());
	}

	void Update () 
	{
		if(TargetObject.target != null)
		{
			transform.LookAt (new Vector3(TargetObject.target.transform.position.x, TargetObject.target.transform.position.y + (TargetObject.target.transform.localScale.y * .5f), TargetObject.target.transform.position.z));
			transform.Translate (Vector3.forward * projectileSpeed * Time.deltaTime);
		}
		else
			transform.Translate (Vector3.forward * projectileSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			other.GetComponent<Health> ().TookDamage (CritChance(),damage);
			gameObject.SetActive (false);
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

	IEnumerator DeActivate()
	{
		yield return new WaitForSeconds (5);
		gameObject.SetActive (false);
	}
}
