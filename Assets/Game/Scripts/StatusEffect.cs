using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour 
{
	public bool statusEffectActive;
	public GameObject fireEffect;
	public int minDamage;
	public int maxDamage;
	public int criticalNumber;
	int damage;
	int statusCounter;

	Coroutine takingFireDamage;
	Health health;

	void Start()
	{
		health = GetComponent<Health> ();
	}

	public void OnFire(int damage, int statusLength)
	{
		statusEffectActive = true;

		if(takingFireDamage != null)
		StopCoroutine (takingFireDamage);
		
		takingFireDamage = StartCoroutine (TakingFireDamage (damage, statusLength));
		fireEffect.SetActive (true);
	}

	IEnumerator TakingFireDamage(int _damage, int _statusLength)
	{
		for (int i = _statusLength; i > 0; i--) 
		{
			health.TookDamage (CritChance(),damage);
			yield return new WaitForSeconds (1);
		}
		statusEffectActive = false;
		fireEffect.SetActive (false);
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
