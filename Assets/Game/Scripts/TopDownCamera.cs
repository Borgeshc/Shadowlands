using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour 
{
	public float speed;

	void Update()
	{
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) 
		{
			Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
			transform.Translate (-touchDeltaPosition.x * speed * Time.deltaTime, -touchDeltaPosition.y * speed * Time.deltaTime, 0);
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -50, 50), transform.position.y, Mathf.Clamp (transform.transform.position.z, -50, 50));
		}
	}
}
