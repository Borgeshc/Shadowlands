using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour 
{
	public Text skillPointsText;
	public GameObject notEnough;
	public GameObject[] abilities;
	int skillPoints;
	bool canBuy;

	void Start()
	{
		skillPoints = PlayerPrefs.GetInt ("SkillPoints");
		skillPointsText.text = "Skill Points: " + skillPoints;

		for (int i = 0; i < abilities.Length; i++) 
		{
			string newString = "Ability" + i;
			if (PlayerPrefs.GetInt (newString) == 1) 
			{
				abilities [i].GetComponent<Image> ().color = Color.white; // DOES NOT WORK
			}
		}
	}

	public void GainSkillPoint()
	{
		skillPoints++;
		PlayerPrefs.SetInt ("SkillPoints", skillPoints);
		skillPointsText.text = "Skill Points: " + skillPoints;
	}

	void Update()
	{
		if (DevMode.devMove) 
		{
			if (Input.GetKeyDown (KeyCode.R)) 
			{
				PlayerPrefs.SetInt ("SkillPoints", 0);
				skillPoints = PlayerPrefs.GetInt ("SkillPoints");
				skillPointsText.text = "Skill Points: " + skillPoints;

				for (int i = 0; i < abilities.Length; i++) 
				{
					string newString = "Ability" + i;
					if (PlayerPrefs.GetInt (newString) == 1) 
					{
						PlayerPrefs.SetInt (newString, 0);
					}
				}

				print ("Reset Abilities and Skill Points");
			}
		}
	}

	public void SpendSkillPoints(int amount)
	{
		if (skillPoints - amount >= 0)
		{
			canBuy = true;
			skillPoints -= amount;
			PlayerPrefs.SetInt ("SkillPoints", skillPoints);
			skillPointsText.text = "Skill Points: " + skillPoints;
			StartCoroutine (CanBuy ());
		} 
		else 
		{
			StartCoroutine (NotEnough ());
		}
	}

	public void ChangeIconColor(Image skillIcon)
	{
		if (canBuy)
		{
			print ("Changed COlored");
			skillIcon.color = Color.white;
		}
	}

	public void UnlockText(Text skillPointsText)
	{
		if(canBuy)
		skillPointsText.text = "3/3";
	}

	public void UnlockedAbility(int ability)
	{
		if (canBuy) 
		{
			string newString = "Ability" + ability;
			PlayerPrefs.SetInt (newString, 1);
		}
	}
	IEnumerator NotEnough()
	{
		notEnough.SetActive (true);
		yield return new WaitForSeconds (2);
		notEnough.SetActive (false);
	}

	IEnumerator CanBuy()
	{
		yield return new WaitForSeconds (.5f);
		canBuy = false;
	}
}
