using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindChild : MonoBehaviour 
{
	Inventory inventory;

	void Start()
	{
		inventory = GameObject.Find ("Inventory").GetComponent<Inventory> ();
	}
	public void FindMyChild()
	{
		if (transform.GetChild(0).childCount > 0) 
		{
			inventory.RemoveItem (transform.GetChild (0).GetChild (0).gameObject);
		}
	}
}
