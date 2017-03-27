using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximity : MonoBehaviour 
{
	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player") 
		{
			if (!CombatManager.inCombat)
				CombatManager.inCombat = true;
			
			GetComponentInParent<ProximityAI> ().InRange(other);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			if (CombatManager.inCombat)
				CombatManager.inCombat = false;
		}
	}
}
