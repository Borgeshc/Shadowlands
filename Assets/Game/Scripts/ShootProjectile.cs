using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour 
{
	public GameObject projectile;
	public KeyCode keycode;
	public int damage;
	public float cooldown;
	public float castTime;
	public int resourceCost;
	public float activeTime;
	public float speed;

	void Start()
	{
		//GetComponent<> ();
	}
}
