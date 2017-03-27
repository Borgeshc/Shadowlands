using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : Ability 
{
	public static bool isBlocking;
	public KeyCode keyCode;
	public GameObject shieldEffect;
	public Image blockingMeter;
	AbilityManager abilityManager;
	Animator anim;
	bool exhausted;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		blockingMeter.enabled = false;
		abilityManager = GetComponent<AbilityManager> ();
	}

	void Update () 
	{
		if (Input.GetKeyDown (keyCode) && !abilityManager.abilityInProgress) 
		{
			abilityManager.abilityInProgress = true;
			isBlocking = true;
		}
		if (abilityManager.abilityInProgress && isBlocking) 
		{
			blockingMeter.enabled = true;
			blockingMeter.fillAmount -= (Time.deltaTime * .5f);

			if (blockingMeter.fillAmount <= 0) 
			{
				exhausted = true;

				abilityManager.abilityInProgress = false;

				isBlocking = false;
			}
			Movement.canMove = false;
			shieldEffect.SetActive (true);
			anim.SetBool ("Block", true);
		} 
		else 
		{

			if (!Movement.canMove)
				Movement.canMove = true;
			
				if (blockingMeter.fillAmount >= 1) 
				{
					blockingMeter.enabled = false;
					exhausted = false;
				}

			shieldEffect.SetActive (false);
			anim.SetBool ("Block", false);

			blockingMeter.fillAmount += Time.deltaTime;
		}

		if (Input.GetKeyUp (keyCode)) {
			abilityManager.abilityInProgress = false;

			isBlocking = false;
		}
	}
}
