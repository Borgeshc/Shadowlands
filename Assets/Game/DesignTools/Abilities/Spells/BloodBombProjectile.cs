using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBombProjectile : MonoBehaviour 
{
	public float speed;
	public int damage;
	GameObject player;

	void Start()
	{
		player = GameObject.Find ("Player");
	}
	void Update()
	{
		if (player != null) {
			transform.Translate (player.transform.forward * speed * Time.deltaTime);
			Destroy (gameObject, 8);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			other.GetComponent<Health> ().TookDamage (damage);
		}
	}
}
