using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
	public GameObject objectToDeactivate;
	public float disableAfter;

	void OnEnable () 
	{
		StartCoroutine (Disable ());
	}

	IEnumerator Disable()
	{
		yield return new WaitForSeconds (disableAfter);
		objectToDeactivate.SetActive (false);
	}
}
