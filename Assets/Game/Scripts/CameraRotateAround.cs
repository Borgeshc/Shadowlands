using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateAround : MonoBehaviour 
{
	public float speed;
	public GameObject anchor;

	void Update ()
	{
		transform.LookAt (anchor.transform);
		transform.Rotate (Vector3.right * speed * Time.deltaTime);
	}
}
