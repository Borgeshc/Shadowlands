using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffects : MonoBehaviour 
{
	Health health;

	void Start()
	{
		health = GetComponent<Health> ();
	}

	public void UsedItem(string itemUsed)
	{
		switch (itemUsed) 
		{
		case "HealthPotion":
			print ("Health Potion Used");
			health.GainHealth (50);
				break;
		case "RegenerationPotion":
			print ("Regeneration Potion Used");
			StartCoroutine(health.GainHealthOverTime (10));
				break;
			case "InsanityPotion":
			print ("Insanity Potion Used");
				break;
		case "ImmunityPotion":
			print ("Immunity Potion Used");
			StartCoroutine (health.Immune());
				break;
			case "EnragePotion":
			print ("Enrage Potion Used");
				break;
		}
	}
}
