using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiphonedBlood : MonoBehaviour 
{
	public Image siphonedBloodBar;
	float maxSiphonedBlood = 100;
	[HideInInspector]
	public float siphonedBlood;


	public void SipponeBlood(float bloodAmount)
	{
		siphonedBlood += bloodAmount;
		if (siphonedBlood > maxSiphonedBlood) 
		{
			siphonedBlood = maxSiphonedBlood;
		}

		siphonedBloodBar.fillAmount = siphonedBlood / maxSiphonedBlood;
	}


	public void UseBlood(float bloodUsed)
	{
		siphonedBlood -= bloodUsed;
		siphonedBloodBar.fillAmount = siphonedBlood / maxSiphonedBlood;
	}
}

