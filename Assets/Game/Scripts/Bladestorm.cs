using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bladestorm : MonoBehaviour 
{
	public KeyCode keyCode;
	public float abilityCooldown;
	public float animationLength;
	public GameObject bladestormEffect;
	public GameObject bladestormDamage;
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
			anim.SetBool ("Bladestorm", true);
		}
	}

	void BladestormAttackBegin()
	{
		resourceManager.CostResource (furyCost);
		bladestormEffect.SetActive (true);
	}

	void BladestormAttackEnd()
	{
		bladestormEffect.SetActive (false);
		anim.SetBool("Bladestorm", false);
		abilityManager.abilityInProgress = false;
		StartCoroutine (Cooldown ());
	}

	IEnumerator Cooldown()
	{
		yield return new WaitForSeconds (abilityCooldown);
		onCooldown = false;
	}
	IEnumerator BladestormDamage()
	{
		bladestormDamage.SetActive (true);
		yield return new WaitForSeconds (.1f);
		bladestormDamage.SetActive (false);
	}
}
