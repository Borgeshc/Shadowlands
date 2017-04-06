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

    public static bool bladestorming;

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
		if (Input.GetKey (keyCode) && !abilityManager.abilityInProgress && resourceManager.resource > furyCost)
        {
            bladestorming = true;
            if (!bladestormEffect.activeInHierarchy)
                bladestormEffect.SetActive(true);

            if (!anim.GetBool("Bladestorm"))
			anim.SetBool ("Bladestorm", true);
		}
        else
        {
            bladestorming = false;

            if (bladestormEffect.activeInHierarchy)
                bladestormEffect.SetActive(false);

            if (anim.GetBool("Bladestorm"))
                anim.SetBool("Bladestorm", false);
        }
	}

	IEnumerator BladestormDamage()
    {
        resourceManager.CostResource(furyCost);
        bladestormDamage.SetActive (true);
		yield return new WaitForSeconds (.1f);
		bladestormDamage.SetActive (false);
	}
}
