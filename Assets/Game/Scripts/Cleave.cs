using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleave : Ability 
{
	public KeyCode keyCode;
	public float animationCooldown;
	public GameObject cleaveEffect;
	public float furyGain;
	public ResourceManager resourceManager;
	public AudioClip cleaveSound;
	public AudioSource source;

	AbilityManager abilityManager;
	Animator anim;
	bool isCleaving;
	bool attacking;
	bool gainingFury;

	void Start () 
	{
		abilityManager = GameObject.Find("GameManager").GetComponent<AbilityManager> ();
		anim = GetComponent<Animator> ();
	}

	void Update () 
	{
		if (Input.GetKeyDown (keyCode) && !abilityManager.abilityInProgress && TargetObject.target != null &&Vector3.Distance(transform.position, TargetObject.target.transform.position) < 3) 
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
		if (!gainingFury) {
            gainingFury = true;
            resourceManager.GainResource(furyGain);
        }
		cleaveEffect.SetActive (true);

		yield return new WaitForSeconds (.1f);

			source.clip = cleaveSound;
		source.Play ();
		yield return new WaitForSeconds (animationCooldown);

		anim.SetBool ("Cleave", false);
		cleaveEffect.SetActive (false);
		attacking = false;
        gainingFury = false;
	}
}
