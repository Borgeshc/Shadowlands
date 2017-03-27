using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarLookAt : MonoBehaviour 
{
	Vector3 lookPosition;
	Quaternion rotation;

	float turnSpeed = 100;

	void Update () 
	{
		lookPosition = Camera.main.transform.position - transform.position;
		lookPosition.y = 0;
		rotation = Quaternion.LookRotation (-lookPosition);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * turnSpeed);
	}
}
