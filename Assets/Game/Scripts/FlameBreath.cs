using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBreath : MonoBehaviour 
{
	public KeyCode keyCode;
	public GameObject flameBreath;
	public BoxCollider flameCollider;
	Animator anim;

	void Start()
	{
		anim = GetComponent<Animator> ();
	}
	void Update () 
	{
		if (Input.GetKey (keyCode)) 
		{
			Movement.canMove = false;
			anim.SetBool ("FlameBreath", true);
			flameBreath.SetActive (true);
			flameCollider.enabled = true;
		} 
		else 
		{
			Movement.canMove = true;
			anim.SetBool ("FlameBreath", false);
			flameBreath.SetActive (false);
			flameCollider.enabled = false;
		}
	}
}
