using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernoStrike : MonoBehaviour 
{
	public KeyCode keyCode;
	public float chargeSpeed;
	public int range;
	public float abilityCooldown;
	public GameObject chargeEffect;
	public GameObject cleaveEffect;

	CharacterController cc;
	Animator anim;

	Vector3 destinationPosition;
	RaycastHit hit;
	bool onCooldown;

	void Start()
	{
		cc = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
	}

	void Update()
	{
		if (Input.GetKeyDown (keyCode)) 
		{
			if(Physics.Raycast(transform.position + (transform.localScale * .5f), transform.forward, out hit, 10))
			{
				if (hit.transform.tag != "Environment" && !onCooldown) 
				{
					onCooldown = true;
					if (GUIUtility.hotControl == 0) 
					{
						Plane playerPlane = new Plane (Vector3.up, transform.position);
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						float hitdist = 0.0f;

						if (playerPlane.Raycast (ray, out hitdist)) 
						{
							Vector3 targetPoint = ray.GetPoint (hitdist);
							destinationPosition = ray.GetPoint (hitdist);
							Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position);
							transform.rotation = targetRotation;
						}
					}
					StartCoroutine (StartAnimation ());
				}
			}
		}
	}

	IEnumerator StartAnimation()
	{
		ClickToMove.canMove = false;
		anim.SetBool ("InfernoStrike", true);
		//chargeEffect.SetActive (true);
		yield return new WaitForSeconds (.3f);
		for(int i = 0; i < range; i++)
			transform.position = Vector3.Lerp (transform.position, (transform.position + transform.forward), chargeSpeed * Time.deltaTime);

		StartCoroutine (EndAnimation());
	}

	IEnumerator EndAnimation()
	{
		yield return new WaitForSeconds (.5f);
		cleaveEffect.SetActive (true);
		//chargeEffect.SetActive (false);
		yield return new WaitForSeconds (.5f);
		anim.SetBool ("InfernoStrike", false);
		ClickToMove.canMove = true;

		StartCoroutine (Cooldown ());
	}

	IEnumerator Cooldown()
	{

		cleaveEffect.SetActive (false);
		yield return new WaitForSeconds (abilityCooldown);
		onCooldown = false;
	}
}
