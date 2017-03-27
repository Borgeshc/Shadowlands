using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MonoBehaviour 
{
	public KeyCode keyCode;
	public GameObject barrage;
	public float abilityCooldown;
	public float abilityTime;
	public float resourceCost;
	public ResourceManager resourceManager;

	bool spendingResource;
	bool firing;
	bool onCooldown;

	AbilityManager abilityManager;
	ObjectPooling multishotPool;
	Animator anim;
	Vector3 tarPos;
	Vector3 destinationPosition;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		abilityManager = GameObject.Find ("GameManager").GetComponent<AbilityManager> ();
		multishotPool = GameObject.Find ("BarragePool").GetComponent<ObjectPooling> ();
	}

	void Update () 
	{
		if (Input.GetKey (keyCode) && !abilityManager.abilityInProgress && TargetObject.target != null &&  resourceManager.resource > resourceCost && !firing && !onCooldown) 
		{
			firing = true;
			onCooldown = true;
			StartCoroutine(Fire ());
		} 
	}

	IEnumerator Fire()
	{
		if (GUIUtility.hotControl == 0) {

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
		anim.SetBool ("Barrage", true);

		yield return new WaitForSeconds (abilityTime);

		ClickToMove.canMove = true;
		anim.SetBool ("Barrage", false);
		abilityManager.abilityInProgress = false;
		firing = false;
		StartCoroutine (Cooldown ());
	}

	IEnumerator SpawnBarrage()
	{
		if (!spendingResource) {
			spendingResource = true;
			resourceManager.CostResource (resourceCost);
		}

		barrage.SetActive (true);
		yield return new WaitForSeconds (1);
		barrage.SetActive (false);
	}

	IEnumerator Cooldown()
	{
		yield return new WaitForSeconds (abilityCooldown);
		onCooldown = false;
	}
}
