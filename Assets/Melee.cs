using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour 
{
	public KeyCode keyCode;

	public static bool attacking;

	public float resourceGain;
	public ResourceManager resourceManager;

	AbilityManager abilityManager;
	Animator anim;
	int melee;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		abilityManager = GameObject.Find ("GameManager").GetComponent<AbilityManager> ();
	}

	void Update () 
	{
		if (Input.GetKey (keyCode) && !attacking && TargetObject.target != null && !abilityManager.abilityInProgress) 
		{
			if(!abilityManager.abilityInProgress)
				abilityManager.abilityInProgress = true;
			
			attacking = true;
			StartCoroutine (Attack ());
		}
	}

	IEnumerator Attack()
	{
		melee++;
		if (melee == 4)
			melee = 1;
		anim.SetInteger ("Melee", melee);
		yield return new WaitForSeconds (.3f);

		resourceManager.GainResource (resourceGain);

		anim.SetInteger ("Melee", 0);

		if(abilityManager.abilityInProgress)
			abilityManager.abilityInProgress = false;
		
		attacking = false;
	}
}
