using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropsGold : MonoBehaviour 
{
	public int minGold;
	public int maxGold;
	public GameObject goldItem;
	Text goldAmountText;
	int goldAmount;
	bool hasDropped;

	public void DropGold()
	{
		if (!hasDropped) 
		{
			hasDropped = true;
			goldAmount = Random.Range (minGold, maxGold);
			GameObject newGoldItem = Instantiate (goldItem, transform.position + new Vector3(0,goldItem.transform.localScale.y * .5f, 0), Quaternion.identity)as GameObject;
			newGoldItem.name = "GoldItem";
			newGoldItem.GetComponent<PickUpGold> ().SetGold(goldAmount);
			goldAmountText = newGoldItem.GetComponentInChildren<Text> ();
			goldAmountText.text = goldAmount + " GOLD";
		}
	}
}
