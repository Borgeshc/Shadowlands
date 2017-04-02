using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrow : MonoBehaviour 
{
	public KeyCode keyCode;
	public GameObject arrowSpawn;

	public float resourceGain;
	public ResourceManager resourceManager;

	int damage;
	bool firing;

	AbilityManager abilityManager;
	ObjectPooling arrowPool;
	Animator anim;
    GameObject lastTarget;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		abilityManager = GameObject.Find ("GameManager").GetComponent<AbilityManager> ();
		arrowPool = GameObject.Find ("ArrowPool").GetComponent<ObjectPooling> ();
		damage = abilityManager.magicArrowDamage;
	}

	void Update () 
	{
		if (Input.GetKey (keyCode) && !abilityManager.abilityInProgress && TargetObject.target != null) 
		{
            lastTarget = TargetObject.target;
			Fire ();
		} 
		else if (Input.GetKeyUp (keyCode)) 
		{
			StopFire ();
		}
	}

	void Fire()
	{
		if(!abilityManager.abilityInProgress)
		abilityManager.abilityInProgress = true;

		if(ClickToMove.canMove)
		ClickToMove.canMove = false;

		if(!anim.GetBool("MagicArrow"))
			anim.SetBool ("MagicArrow", true);
	}

	void StopFire()
	{		
		if(!ClickToMove.canMove)
		ClickToMove.canMove = true;

		if(anim.GetBool("MagicArrow"))
			anim.SetBool ("MagicArrow", false);

		if(abilityManager.abilityInProgress)
			abilityManager.abilityInProgress = false;
	}

	void SpawnArrow()
	{
		resourceManager.GainResource (resourceGain);
		GameObject obj = arrowPool.GetPooledObject();

		if (obj == null)
		{
			return;
		}
        obj.GetComponent<MagicArrowProjectile>().target = lastTarget;
		obj.transform.position = arrowSpawn.transform.position;
		obj.SetActive(true);
	}
}
