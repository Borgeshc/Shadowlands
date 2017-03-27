using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour 
{
	public static bool canMove;
	public float speed;
	public float rotationSpeed;
	public float footStepFreq;
	public AudioClip leftFoot;
	public AudioClip rightFoot;

	Vector3 input;
	Vector3 targetRotation;

	CharacterController cc;
	AudioSource source;
	Animator anim;
	AbilityManager abilityManager;

	bool step;

	bool footSteps;

	void Start () 
	{
		canMove = true;
		abilityManager = GetComponent<AbilityManager> ();
		cc = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();
	}

	void FixedUpdate()
	{
		if (canMove && !abilityManager.abilityInProgress) 
		{

			input = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			if (input.sqrMagnitude > 1f) 
			{
				input.Normalize ();
			}

			if (input != Vector3.zero) 
			{
				if (!source.isPlaying && !footSteps) 
				{
					footSteps = true;
					StartCoroutine (FootStepSound());
				}
				
				targetRotation = Quaternion.LookRotation (input).eulerAngles;
				anim.SetBool ("IsWalking", true);
				cc.SimpleMove (transform.forward * speed * Time.deltaTime);
			} else
				anim.SetBool ("IsWalking", false);

			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (targetRotation.x,
				Mathf.Round (targetRotation.y / 45) * 45, targetRotation.z), Time.deltaTime * rotationSpeed);
		}
	}

	IEnumerator FootStepSound()
	{
		yield return new WaitForSeconds (footStepFreq);
		if (!step) 
		{
			step = true;
			source.clip = rightFoot;
			source.Play ();
		} 
		else  if(step)
		{
			source.clip = leftFoot;
			source.Play ();
			step = false;
		}
		footSteps = false;
	}
}
