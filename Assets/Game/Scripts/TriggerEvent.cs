using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour 
{
	public List<GameObject> possibleEvents;

	public static bool eventActive;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			if (!eventActive && possibleEvents != null) 
			{
				TriggerEvent.eventActive = true;
				possibleEvents [Random.Range (0, possibleEvents.Count)].SetActive (true);
			}
		}
	}
}
