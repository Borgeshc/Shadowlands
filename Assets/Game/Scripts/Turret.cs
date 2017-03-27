using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour 
{
	public float attackRange;
	public float attackFreq;
	public GameObject projectile;
	public int damagePerShot;
	public float projectileSpeed;

	GameObject target;
	bool enemyInRange;
	bool hasTarget;
	bool attacking;
	RaycastHit hit;
	LineRenderer line;

	void Start()
	{
		attacking = false;
		line = GetComponent<LineRenderer> ();
		line.enabled = false;
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			enemyInRange = true;
			if (!hasTarget) 
			{
				hasTarget = true;
				target = other.gameObject;
			}
		} 
		else 
		{
			enemyInRange = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (target == other.gameObject) 
		{
			hasTarget = false;
			line.enabled = false;
		}
	}

	void Update()
	{
		if (target == null || target.activeInHierarchy == false) 
		{
			hasTarget = false;
			line.enabled = false;
		}

		if (hasTarget) 
		{
			//transform.LookAt (target.transform);
			transform.up = target.transform.position - transform.position;

			line.enabled = true;
			line.SetPosition (0, transform.position);
			line.SetPosition (1, target.transform.position);

			if (!attacking) 
			{
				attacking = true;
				StartCoroutine (Attack (target));
			}
		}
	}

	IEnumerator Attack(GameObject enemy)
	{
		enemy.GetComponent<Health> ().TookDamage (2);
		yield return new WaitForSeconds (attackFreq);
		attacking = false;
	}
}
