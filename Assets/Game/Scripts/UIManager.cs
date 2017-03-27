using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	public KeyCode inventoryKeyCode;
	public GameObject inventory;
	public AudioClip inventorySound;
	public Text goldText;
	[Space]
	public KeyCode skillsKeyCode;
	public GameObject skills;
	//public AudioClip skillsSound;

	AudioSource source;

	void Start () 
	{
		source = GetComponent<AudioSource> ();
	}

	void Update () 
	{
		if (inventory.activeInHierarchy) 
		{
			goldText.text = "" + PlayerPrefs.GetInt("Gold");
		}
		if (Input.GetKeyDown (inventoryKeyCode)) 
		{
			source.clip = inventorySound;
			source.Play ();
			inventory.SetActive (!inventory.activeSelf);
		}

		if (Input.GetKeyDown (skillsKeyCode)) 
		{
			//source.clip = skillsSound;
			//source.Play ();
			skills.SetActive (!skills.activeSelf);
		}


		if (DevMode.devMove) 
		{
			if (Input.GetKeyDown (KeyCode.R)) 
			{
				PlayerPrefs.SetInt ("Gold", 0);
				goldText.text = "" + PlayerPrefs.GetInt("Gold");
				print ("Gold Reset");
			}
		}
	}
}
