using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour 
{
	public GameObject vendor;
    public GameObject inventory;
	public GameObject player;
    public GameObject[] vendorSlots;
    public GameObject[] vendorItems;

    Inventory myInventory;
	bool canBuy;

    void Start()
    {
        myInventory = inventory.GetComponent<Inventory>();
    }

    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			vendor.SetActive (true);
            inventory.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") 
		{
			vendor.SetActive (false);
            inventory.SetActive(false);
		}
	}

    public void VendorSlot(int item)
    {
		StartCoroutine (CheckingGold ());

		if(canBuy)
        myInventory.AddItem(vendorItems[item]);
    }

	public void GoldAmount(int goldAmt)
	{
		int oldGold = PlayerPrefs.GetInt ("Gold");
		if (oldGold - goldAmt < 0)
			canBuy = false;
		else {
			canBuy = true;
			oldGold -= goldAmt;
			PlayerPrefs.SetInt ("Gold", oldGold);
		}
	}

	IEnumerator CheckingGold()
	{
		yield return new WaitForSeconds (.1f);
	}
}
