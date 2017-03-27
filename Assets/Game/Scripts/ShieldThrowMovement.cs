using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldThrowMovement : MonoBehaviour 
{/*
	public List<GameObject> enemiesInRange;
	public float speed;
	GameObject target;
	Transform shieldPosition;
	GameObject newTarget;

	public void SetTarget(GameObject _target, GameObject _shieldPosition)
	{
		if (_target != null) 
		{
			target = _target;
			newTarget = target;
			enemiesInRange.Add (newTarget);
			shieldPosition = _shieldPosition.transform;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (this.enabled == true && other.tag == "Enemy" && other.tag != newTarget.tag && !enemiesInRange.Contains(other.gameObject)) 
		{
			enemiesInRange.Add (other.gameObject);
		}
	}

	void Update()
	{
		if (newTarget != null) {
			transform.position = Vector3.Slerp (transform.position, newTarget.transform.position, speed * Time.deltaTime);
			//transform.LookAt (newTarget.transform);
			//transform.position += new Vector3(0,0,transform.forward.z) * speed * Time.deltaTime;
			//transform.Translate (transform.forward * speed * Time.deltaTime);
		}
	}

	public void NewTarget(GameObject _newTarget)
	{
		newTarget = _newTarget;
	}

	public void MoveToShieldPosition()
	{
		newTarget = shieldPosition.gameObject;
	}*/
}
