using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwingSword : MonoBehaviour 
{
	public KeyCode keyCode;
	public float animationWaitTime;
	public AudioClip[] swishSounds;
	public float swishSoundFreq;
	public float furyPerAttack;
	public int minDamage;
	public int maxDamage;
	public int criticalNumber;
	public float furyGain;
	public ResourceManager resourceManager;

	Animator anim;
	bool attacking;
	int animationType;

	CharacterController cc;
	public float speed;
	Transform target;
	Vector3 lookPosition;
	Quaternion rotation;

	public AudioSource source;
	bool playingSound;

	float turnSpeed = 10;
	bool damaging;
	bool dealingDamage;
	float storeSpeed;
	int damage;

	void Start()
	{
		cc = GetComponent<CharacterController> ();
		storeSpeed = GetComponent<Movement> ().speed;
		anim = GetComponent<Animator> ();
		speed = storeSpeed;
	}
	void FixedUpdate ()
	{
		if (TargetObject.target != null) {
			if (Input.GetKeyDown (keyCode)) {
				if (TargetObject.target != null) {
					target = TargetObject.target.transform;
					print ("looking for target " + TargetObject.target);
					lookPosition = target.transform.position - transform.position;
					lookPosition.y = 0;
					rotation = Quaternion.LookRotation (lookPosition);
					transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * turnSpeed);
				}

				if (!attacking) 
				{
					if (TargetObject.target.tag == "Dragon" && TargetObject.target != null && Vector3.Distance (transform.position, TargetObject.target.transform.position) < 5 && !Charge.isCharging) 
					{
						HandleAttack ();
					}
					else if (TargetObject.target != null && Vector3.Distance (transform.position, TargetObject.target.transform.position) < 2 && !Charge.isCharging) 
					{
						HandleAttack ();
					}
				}
			}
		}
	}

	void HandleAttack()
	{
		print ("Attempting to Attack");
		anim.SetBool ("IsWalking", false);
		speed = 0;
		attacking = true;
		animationType = Random.Range (1, 4);

		StartCoroutine (Attack ());
		if (!playingSound) {
			playingSound = true;
			StartCoroutine (PlaySound ());
		}
		if (!dealingDamage) {
			dealingDamage = true;
			StartCoroutine (DealDamage ());
		}
	}

	IEnumerator Attack()
	{
		anim.SetInteger ("SwingSword", animationType);
		yield return new WaitForSeconds (animationWaitTime);

		anim.SetInteger ("SwingSword", 0);
		attacking = false;
	}

	IEnumerator DealDamage()
	{
		yield return new WaitForSeconds (animationWaitTime * .7f);
		if(TargetObject.target != null)
			TargetObject.target.GetComponent<Health> ().TookDamage (CritChance(),damage);
		yield return new WaitForSeconds (animationWaitTime * .3f);
		dealingDamage = false;
	}

	IEnumerator PlaySound()
	{
		yield return new WaitForSeconds (.3f);

			source.clip = swishSounds [Random.Range (0, swishSounds.Length)];
			source.Play ();

		resourceManager.GainResource (furyGain);
		yield return new WaitForSeconds (swishSoundFreq);
		playingSound = false;
	}

	bool CritChance()
	{
		int damageAmount = Random.Range (minDamage, maxDamage);
		damage = damageAmount; 
		if (damageAmount < criticalNumber)
			return false;
		else
			return true;
	}
}
