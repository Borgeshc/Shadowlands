using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleave : Ability 
{
	public KeyCode keyCode;
	public float animationCooldown;
	public float effectTimer;
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
		abilityManager = GetComponent<AbilityManager> ();
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
				StartCoroutine (Attack ());
			}
		}
		if (Input.GetKeyUp (keyCode)) 
		{
			isCleaving = false;
			abilityManager.abilityInProgress = false;
		}
	}

	IEnumerator Attack()
	{
		if (!spendingFury) {
			spendingFury = true;
			resourceManager.CostResource (furyCost);
		}
		cleaveEffect.SetActive (true);

		anim.SetBool ("Cleave", true);
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
