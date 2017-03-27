using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour 
{
	public KeyCode keyCode;

	public static bool attacking;
	Animator anim;
	int melee;

	void Start () 
	{
		anim = GetComponent<Animator> ();
	}

	void Update () 
	{
		if (Input.GetKey (keyCode) && !attacking && TargetObject.target != null) 
		{
			attacking = true;
			StartCoroutine (Attack ());
		}
	}

	IEnumerator Attack()
	{
		melee++;
		if (melee == 4)
			melee = 1;
		anim.SetInteger ("Melee", melee);
		yield return new WaitForSeconds (.3f);
		anim.SetInteger ("Melee", 0);
		attacking = false;
	}
}
