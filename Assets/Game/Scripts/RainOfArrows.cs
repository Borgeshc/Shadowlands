using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainOfArrows : MonoBehaviour 
{
	public KeyCode keyCode;
	public GameObject arrowSpawn;
	public float abilityLength;
	public float abilityCooldown;
	public float resourceCost;
	public GameObject rainOfArrowsDamage;
	public ResourceManager resourceManager;

	bool spendingResource;
	bool firing;
	bool onCooldown;

	AbilityManager abilityManager;
	ObjectPooling rainOfArrowsPool;
	Animator anim;
	Vector3 tarPos;
	Vector3 destinationPosition;
	GameObject target;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		abilityManager = GameObject.Find ("GameManager").GetComponent<AbilityManager> ();
		rainOfArrowsPool = GameObject.Find ("RainOfArrowsPool").GetComponent<ObjectPooling> ();
	}

	void Update () 
	{
		if (Input.GetKey (keyCode) && !abilityManager.abilityInProgress && TargetObject.target != null &&  resourceManager.resource > resourceCost && !firing && !onCooldown) 
		{
			target = TargetObject.target;
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
		anim.SetBool ("RainOfArrows", true);
		anim.SetBool ("RainOfArrowsDamage", true);

		yield return new WaitForSeconds (abilityLength);

		ClickToMove.canMove = true;
		anim.SetBool ("RainOfArrows", false);
		abilityManager.abilityInProgress = false;
		firing = false;
	}

	void SpawnRainOfArrows()
	{
		if (!spendingResource) {
			spendingResource = true;
			resourceManager.CostResource (resourceCost);
		}

		GameObject obj = rainOfArrowsPool.GetPooledObject();

		if (obj == null)
		{
			return;
		}
		obj.transform.position = target.transform.position + new Vector3(0, 2,0);
		obj.transform.rotation = new Quaternion(gameObject.transform.rotation.x,-gameObject.transform.rotation.y,gameObject.transform.rotation.z,gameObject.transform.rotation.w);
		obj.SetActive(true);

		spendingResource = false;
		onCooldown = true;
		StartCoroutine (RainOfArrowsDamage (obj.transform.Find("ArrowStormDamage").gameObject));
		StartCoroutine (Cooldown());
	}

	IEnumerator RainOfArrowsDamage(GameObject _rainOfArrowDamage)
	{
		rainOfArrowsDamage = _rainOfArrowDamage;
		rainOfArrowsDamage.SetActive (true);
		yield return new WaitForSeconds (.1f);
		rainOfArrowsDamage.SetActive (false);
	}

	void RainOfArrowsDamageEnd()
	{
		if(anim.GetBool("RainOfArrowsDamage") == true)
		anim.SetBool ("RainOfArrowsDamage", false);
	}

	IEnumerator Cooldown()
	{
		yield return new WaitForSeconds (abilityCooldown);
		onCooldown = false;
	}
}
