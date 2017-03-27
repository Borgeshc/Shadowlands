using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBomb : MonoBehaviour 
{
	public static bool bloodBombActive;
	public AudioSource source;
	public GameObject bloodBomb;
	public GameObject bloodBombSpawn;
	public GameObject castEffect1;
	public GameObject castEffect2;
	public float abilityCost;
	public Animator anim;
	bool casting;
	bool onCooldown;
	SiphonedBlood siphonedBlood;

	void Start()
	{
		siphonedBlood = GameObject.Find ("SiphonedBlood").GetComponent<SiphonedBlood> ();
	}
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Mouse1) && !casting && !onCooldown && !BloodDrain.bloodDrainActive && siphonedBlood.siphonedBlood >= abilityCost)
		{
			source.PlayOneShot (source.clip);
			siphonedBlood.UseBlood (abilityCost);
			bloodBombActive = true;
			MovementScript.canMove = false;
			onCooldown = true;
			castEffect1.SetActive (true);
			castEffect2.SetActive (true);
			casting = true;
			anim.SetBool ("BloodBomb", true);
			StartCoroutine (Spawn ());
		}
	}

	IEnumerator Spawn()
	{
		yield return new WaitForSeconds (.8f);
		Instantiate (bloodBomb, bloodBombSpawn.transform.position, transform.rotation);
		bloodBombActive = false;
		MovementScript.canMove = true;
		castEffect1.SetActive (false);
		castEffect2.SetActive (false);
		anim.SetBool ("BloodBomb", false);
		StartCoroutine (Cooldown ());
		casting = false;
	}

	IEnumerator Cooldown()
	{ 
		yield return new WaitForSeconds (1);
		onCooldown = false;
	}
}
