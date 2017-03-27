using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevMode : MonoBehaviour 
{
	public static bool devMove;
	int keyCount;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Keypad0))
		{
			if (devMove == false) 
			{
				keyCount++;
				if (keyCount >= 5) 
				{
					devMove = true;
					print ("DevMode Active");
					keyCount = 5;
				}
			} 
			else if (devMove == true) 
			{
				keyCount--;
				if (keyCount <= 0) 
				{
					devMove = false;
					print ("DevMode InActive");
					keyCount = 0;
				}
			}
		}
	}
}
