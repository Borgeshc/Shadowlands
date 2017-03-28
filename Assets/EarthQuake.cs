using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : MonoBehaviour 
{
	public KeyCode keyCode;
	public float abilityCooldown;
	public float abilityLength;
	public GameObject earthquakeEffect;
	public float furyCost;
	public ResourceManager resourceManager;

	bool onCooldown;
	AbilityManager abilityManager;
	Animator anim;

	void Start () 
	{
		abilityManager = GameObject.Find("GameManager").GetComponent<AbilityManager> ();
		anim = GetComponent<Animator> ();
	}

	void Update () 
	{
		if (Input.GetKeyDown (keyCode) && !abilityManager.abilityInProgress && resourceManager.resource > furyCost && !onCooldown) 
		{
			onCooldown = true;
			anim.SetBool ("EarthQuake", true);
		}
	}

	IEnumerator EarthQuakeStart()
	{
		resourceManager.CostResource (furyCost);
		Instantiate (earthquakeEffect, transform.position, earthquakeEffect.transform.rotation);
		yield return new WaitForSeconds (abilityLength);
		anim.SetBool("EarthQuake", false);
		abilityManager.abilityInProgress = false;
		StartCoroutine (Cooldown ());
	}

	IEnumerator Cooldown()
	{
		yield return new WaitForSeconds (abilityCooldown);
		onCooldown = false;
	}
}
