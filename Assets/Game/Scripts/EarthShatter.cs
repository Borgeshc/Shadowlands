using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthShatter : MonoBehaviour 
{
	public KeyCode keyCode;
	public GameObject earthShatterSpawn;
	public float abilityLength;
	public float abilityCooldown;
	public float resourceCost;
	public ResourceManager resourceManager;

	bool spendingResource;
	bool firing;

	AbilityManager abilityManager;
	ObjectPooling earthShatterPool;
	Animator anim;
	Vector3 tarPos;
	Vector3 destinationPosition;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		abilityManager = GameObject.Find ("GameManager").GetComponent<AbilityManager> ();
		earthShatterPool = GameObject.Find ("EarthShatterPool").GetComponent<ObjectPooling> ();
	}

	void Update () 
	{
		if (Input.GetKeyDown (keyCode) && !abilityManager.abilityInProgress &&  resourceManager.resource > resourceCost && !firing) 
		{
			firing = true;
			StartCoroutine(Fire ());
		} 
	}

	IEnumerator Fire()
	{
		if (GUIUtility.hotControl == 0) 
		{
			Plane playerPlane = new Plane (Vector3.up, transform.position);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float hitdist = 0.0f;

			if (playerPlane.Raycast (ray, out hitdist)) {
				Vector3 targetPoint = ray.GetPoint (hitdist);
				destinationPosition = ray.GetPoint (hitdist);
				Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position);
				//	myTransform.rotation = Quaternion.Slerp (myTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
				transform.rotation = targetRotation;
			}
		}
		abilityManager.abilityInProgress = true;
		ClickToMove.canMove = false;
		anim.SetBool ("EarthShatter", true);

		yield return new WaitForSeconds (abilityLength);

		ClickToMove.canMove = true;
		anim.SetBool ("EarthShatter", false);
		abilityManager.abilityInProgress = false;
	}

	void SpawnEarthShatter()
	{
		if (!spendingResource) {
			spendingResource = true;
			resourceManager.CostResource (resourceCost);
		}

		GameObject obj = earthShatterPool.GetPooledObject();

		if (obj == null)
		{
			return;
		}
		obj.transform.position = earthShatterSpawn.transform.position;
		obj.transform.rotation = gameObject.transform.rotation;
		obj.SetActive(true);

		spendingResource = false;

		StartCoroutine (Cooldown ());
	}

	IEnumerator Cooldown()
	{
		yield return new WaitForSeconds (abilityCooldown);
		firing = false;
	}
}
