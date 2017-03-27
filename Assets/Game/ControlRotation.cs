using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRotation : MonoBehaviour 
{
	public Vector3 offset;

	void Update()
	{
		transform.position = offset;
		transform.rotation = Camera.main.transform.rotation;
	}
}
