using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour 
{
	public KeyCode keyCode;

	public static bool attacking;
    public GameObject sword1;
    public GameObject sword2;

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
		if (Input.GetKeyDown (keyCode) && !attacking && TargetObject.target != null && !abilityManager.abilityInProgress && Vector3.Distance(transform.position, TargetObject.target.transform.position) <= 1.5f) 
		{
			if(!abilityManager.abilityInProgress)
				abilityManager.abilityInProgress = true;
			
			attacking = true;
			StartCoroutine (Attack ());
		}

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(keyCode) && !attacking && TargetObject.target != null && !abilityManager.abilityInProgress && Vector3.Distance(transform.position, TargetObject.target.transform.position) <= 1.5f)
        {
            if (!abilityManager.abilityInProgress)
                abilityManager.abilityInProgress = true;

            attacking = true;
            StartCoroutine(Attack());
        }
    }

	IEnumerator Attack()
	{
		melee++;
		if (melee == 4)
			melee = 1;
		anim.SetInteger ("Melee", melee);
		yield return new WaitForSeconds (.7f);

		resourceManager.GainResource (resourceGain);

		anim.SetInteger ("Melee", 0);

		if(abilityManager.abilityInProgress)
			abilityManager.abilityInProgress = false;
		
		attacking = false;
	}

    void DealDamage()
    {
        sword1.SetActive(true);
        sword2.SetActive(true);
    }

    void StopDealingDamage()
    {
        sword1.SetActive(false);
        sword2.SetActive(false);
    }
}
