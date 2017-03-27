using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleave : Ability 
{
	public KeyCode keyCode;
	public float animationCooldown;
	public GameObject cleaveEffect;
	public float furyCost;
	public ResourceManager resourceManager;
	public AudioClip cleaveSound;
	public AudioSource source;

	AbilityManager abilityManager;
	Animator anim;
	bool isCleaving;
	bool attacking;
	bool spendingFury;

	void Start () 
	{
		abilityManager = GameObject.Find("GameManager").GetComponent<AbilityManager> ();
		anim = GetComponent<Animator> ();
	}

	void Update () 
	{
		if (Input.GetKeyDown (keyCode) && !abilityManager.abilityInProgress && resourceManager.resource > furyCost) 
		{
			abilityManager.abilityInProgress = true;
			isCleaving = true;
		}

		if (isCleaving)
		{
			Movement.canMove = false;
			if (!attacking) 
			{
				attacking = true;
				anim.SetBool ("Cleave", true);
			}
		}
		if (Input.GetKeyUp (keyCode)) 
		{
			isCleaving = false;
			abilityManager.abilityInProgress = false;
		}
	}

	IEnumerator CleaveAttack()
	{
		if (!spendingFury) {
			spendingFury = true;
			resourceManager.CostResource (furyCost);
		}
		cleaveEffect.SetActive (true);

		yield return new WaitForSeconds (.1f);

			source.clip = cleaveSound;
		source.Play ();
		yield return new WaitForSeconds (animationCooldown);

		anim.SetBool ("Cleave", false);
		cleaveEffect.SetActive (false);
		attacking = false;
		spendingFury = false;
	}
}
