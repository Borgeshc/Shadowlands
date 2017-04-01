using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowStorm : MonoBehaviour 
{
	public KeyCode keyCode;
	public float abilityCooldown;
	public float animationLength;
	public GameObject arrowstormEffect;
	public GameObject arrowstormDamage;
	public float resourceCost;
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
		if (Input.GetKeyDown (keyCode) && !abilityManager.abilityInProgress && resourceManager.resource > resourceCost && !onCooldown) 
		{
			onCooldown = true;
			anim.SetBool ("ArrowStorm", true);
		}
	}

	void ArrowstormAttackBegin()
	{
		resourceManager.CostResource (resourceCost);
		arrowstormEffect.SetActive (true);
	}

	void ArrowstormAttackEnd()
	{
		arrowstormEffect.SetActive (false);
		anim.SetBool("ArrowStorm", false);
		abilityManager.abilityInProgress = false;
		StartCoroutine (Cooldown ());
	}

	IEnumerator Cooldown()
	{
		yield return new WaitForSeconds (abilityCooldown);
		onCooldown = false;
	}
	IEnumerator ArrowstormDamage()
	{
		arrowstormDamage.SetActive (true);
		yield return new WaitForSeconds (.1f);
		arrowstormDamage.SetActive (false);
	}
}
