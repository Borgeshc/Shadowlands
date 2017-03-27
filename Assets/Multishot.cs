using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multishot : MonoBehaviour 
{
	public KeyCode keyCode;
	public GameObject arrowSpawn;
	public float abilityCooldown;
	public float resourceCost;
	public ResourceManager resourceManager;

	bool spendingResource;
	bool firing;

	AbilityManager abilityManager;
	ObjectPooling multishotPool;
	Animator anim;
	Vector3 tarPos;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		abilityManager = GameObject.Find ("GameManager").GetComponent<AbilityManager> ();
		multishotPool = GameObject.Find ("MultishotPool").GetComponent<ObjectPooling> ();
	}

	void Update () 
	{
		if (Input.GetKey (keyCode) && !abilityManager.abilityInProgress && TargetObject.target != null &&  resourceManager.resource > resourceCost && !firing) 
		{
			firing = true;
			StartCoroutine(Fire ());
		} 
	}

	IEnumerator Fire()
	{
		abilityManager.abilityInProgress = true;
		ClickToMove.canMove = false;
		anim.SetBool ("Multishot", true);

		yield return new WaitForSeconds (abilityCooldown);

		ClickToMove.canMove = true;
		anim.SetBool ("Multishot", false);
		abilityManager.abilityInProgress = false;
		firing = false;
	}

	void SpawnMultishot()
	{
		if (!spendingResource) {
			spendingResource = true;
			resourceManager.CostResource (resourceCost);
		}

		GameObject obj = multishotPool.GetPooledObject();

		if (obj == null)
		{
			return;
		}
		obj.transform.position = arrowSpawn.transform.position;
		obj.transform.rotation = gameObject.transform.rotation;
		obj.SetActive(true);

		spendingResource = false;
	}
}
