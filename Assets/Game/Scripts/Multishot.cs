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
	Vector3 destinationPosition;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		abilityManager = GameObject.Find ("GameManager").GetComponent<AbilityManager> ();
		multishotPool = GameObject.Find ("MultishotPool").GetComponent<ObjectPooling> ();
	}

	void Update () 
	{
		if (Input.GetKey (keyCode) && !abilityManager.abilityInProgress &&  resourceManager.resource > resourceCost && !firing) 
		{
			firing = true;
			StartCoroutine(Fire ());
		} 
	}

	IEnumerator Fire()
	{
		if (Input.GetMouseButtonDown (1) && GUIUtility.hotControl == 0) {

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
