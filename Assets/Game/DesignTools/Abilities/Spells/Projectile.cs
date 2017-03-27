using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
	public float speed;
	public float destroyTime;
	public int minDamage;
	public int maxDamage;
	public int criticalNumber;
	int damage;

	GameObject player;

	void Start()
	{
		player = GameObject.Find ("Player");
		StartCoroutine (Deactivate ());
	}
	void Update()
	{
		if (player != null) {
			transform.Translate (player.transform.forward * speed * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			other.GetComponent<Health> ().TookDamage (CritChance(),damage);
			//gameObject.SetActive (false);
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

	IEnumerator Deactivate()
	{
		yield return new WaitForSeconds (destroyTime);
		gameObject.SetActive (false);
	}
}
