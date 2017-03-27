using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGold : MonoBehaviour 
{
	AudioSource source;
	int goldAmount;
	int oldGold;
	bool pickedUp;

	void Start()
	{
		source = GameObject.Find ("GoldSoundManager").GetComponent<AudioSource>();
	}
	public void SetGold(int goldAmt)
	{
		goldAmount = goldAmt;
	}

	public void PickUp()
	{
		if (!pickedUp) 
		{
			pickedUp = true;
			source.Play ();
			oldGold = PlayerPrefs.GetInt ("Gold");
			goldAmount = oldGold + goldAmount;
			PlayerPrefs.SetInt ("Gold", goldAmount);
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			PickUp ();
		}	
	}
}
