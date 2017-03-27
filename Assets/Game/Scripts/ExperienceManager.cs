using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour 
{
	public Image experienceBar;
	public float xpRequiredPerLevel;
	public int currentLevel;
	public Text UIlevel;
	public GameObject levelUpEffect;
	public AudioSource source;
	Health health;
	public SkillManager skillManager;

	void Start()
	{
		currentLevel = PlayerPrefs.GetInt ("CurrentLevel");
		UIlevel.text = "" + currentLevel;
		experienceBar.fillAmount = PlayerPrefs.GetFloat ("CurrentExperience");

		health = GameObject.Find ("Player").GetComponent<Health>();
	}

	public void GainExperience(float experience)
	{
		if ((experienceBar.fillAmount + (experience / xpRequiredPerLevel)) > 1) {
			source.Play ();
			currentLevel++;
			UIlevel.text = "" + currentLevel;
			PlayerPrefs.SetInt ("CurrentLevel", currentLevel);
			skillManager.GainSkillPoint ();
			xpRequiredPerLevel += xpRequiredPerLevel * 1.5f;
			health.GainHealth (health.maxHealth);
			float neededXP = xpRequiredPerLevel - experienceBar.fillAmount;
			experienceBar.fillAmount += neededXP / xpRequiredPerLevel;
			experienceBar.fillAmount = 0;
			experience -= neededXP;
			experienceBar.fillAmount += experience / xpRequiredPerLevel;
			PlayerPrefs.SetFloat ("CurrentExperience",experienceBar.fillAmount);
			StartCoroutine (LeveledUp ());
		} 
		else 
		{
			experienceBar.fillAmount += experience / xpRequiredPerLevel;
			PlayerPrefs.SetFloat ("CurrentExperience",experienceBar.fillAmount);
		}
	}

	IEnumerator LeveledUp()
	{
		levelUpEffect.SetActive (true);
		yield return new WaitForSeconds (3);
		levelUpEffect.SetActive (false);
	}

	void Update()
	{
		if (DevMode.devMove) 
		{
			if (Input.GetKeyDown (KeyCode.R)) 
			{
				PlayerPrefs.SetInt ("CurrentLevel", 0);
				PlayerPrefs.SetFloat ("CurrentExperience", 0);

				currentLevel = PlayerPrefs.GetInt ("CurrentLevel");
				UIlevel.text = "" + currentLevel;
				experienceBar.fillAmount = PlayerPrefs.GetFloat ("CurrentExperience");

				print ("Level and Experience reset.");
			}
		}
	}
}
