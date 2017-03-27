using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour 
{
	public GameObject playerCamera;
	public GameObject bossCamera;
	public GameObject cameraPosition1;
	public GameObject cameraPosition2;
	public GameObject fallenRockPile;
	public Animator anim;

	GameObject player;
	bool startLerp;
	bool triggered;
	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Player") 
		{		
			if (!triggered) 
			{
				triggered = true;
				StartCoroutine (CutScene ());
			}
		}
	}

	void Update()
	{
		if (startLerp) 
		{
			bossCamera.transform.position = Vector3.Lerp (cameraPosition1.transform.position, cameraPosition2.transform.position, 5 * Time.deltaTime);
		}
	}

	IEnumerator CutScene()
	{
		ClickToMove.canMove = false;
		bossCamera.SetActive (true);
		playerCamera.SetActive (false);
		anim.SetBool ("Roar", true);
		yield return new WaitForSeconds (3);

		startLerp = true;
		yield return new WaitForSeconds (2.5f);
		fallenRockPile.SetActive (true);
		yield return new WaitForSeconds (2);

		playerCamera.SetActive (true);
		bossCamera.SetActive (false);
		ClickToMove.canMove = true;
		anim.SetLayerWeight (2, 0);
	}
}
