using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHit : MonoBehaviour 
{/*
	ShieldThrowMovement shieldThrowMovement;
	int maxTargets = 5;
	int targetsHit;

	void Start()
	{
		shieldThrowMovement = GetComponentInParent<ShieldThrowMovement> ();
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			if (shieldThrowMovement.enemiesInRange.Contains (other.gameObject)) 
			{
				targetsHit++;
				if(targetsHit >= maxTargets)
					shieldThrowMovement.MoveToShieldPosition ();

				shieldThrowMovement.enemiesInRange.Remove (other.gameObject); 
				if (shieldThrowMovement.enemiesInRange.Count != 0)
					shieldThrowMovement.NewTarget (shieldThrowMovement.enemiesInRange [Random.Range (0, shieldThrowMovement.enemiesInRange.Count)].gameObject);
				else
					shieldThrowMovement.MoveToShieldPosition ();
				
			}
			else
				shieldThrowMovement.MoveToShieldPosition ();
		}
	}
	*/
}
