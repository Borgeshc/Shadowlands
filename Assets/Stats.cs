using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public GameObject statisticsPanel;
    public KeyCode keycode;
    public Text damageText;
    public Text healthText;
    public Text armorText;
    public Text critChanceText;

    public Text skillPointsText;

    public static int damage;
    public static int health;
    public static int armor;
    public static int critChance;

    GameObject player;
    int skillPoints;
    Health myHealth;

    private void Start()
    {
        if (!PlayerPrefsX.GetBool("NewGameSetStats") && PlayerPrefs.GetInt("ClassChosen") == 0) //Warrior
        {
            PlayerPrefsX.SetBool("NewGameSetStats", true);
            damage = 1;
            health = 150;
            armor = 3;
            critChance = 5;
            PlayerPrefs.SetInt("Damage", damage);
            PlayerPrefs.SetInt("Health", health);
            PlayerPrefs.SetInt("Armor", armor);
            PlayerPrefs.SetInt("CritChance", critChance);
        }
        else if (!PlayerPrefsX.GetBool("NewGameSetStats") && PlayerPrefs.GetInt("ClassChosen") == 1) //Archer
        {
            PlayerPrefsX.SetBool("NewGameSetStats", true);
            damage = 2;
            health = 100;
            armor = 1;
            critChance = 10;
            PlayerPrefs.SetInt("Damage", damage);
            PlayerPrefs.SetInt("Health", health);
            PlayerPrefs.SetInt("Armor", armor);
            PlayerPrefs.SetInt("CritChance", critChance);
        }
        else
        {
            damage = PlayerPrefs.GetInt("Damage");
            health = PlayerPrefs.GetInt("Health");
            armor = PlayerPrefs.GetInt("Armor");
            critChance = PlayerPrefs.GetInt("CritChance");
        }

        damageText.text = damage.ToString();
        healthText.text = health.ToString();
        armorText.text = armor.ToString();
        critChanceText.text = critChance.ToString();

        skillPoints = PlayerPrefs.GetInt("SkillPoints");
        skillPointsText.text = skillPoints.ToString();
        player = GameObject.FindGameObjectWithTag("Player");
        myHealth = player.GetComponent<Health>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(keycode))
        {
            statisticsPanel.SetActive(!statisticsPanel.activeSelf);
        }
    }

    public void UpdateStats(string stat)
    {
        if (skillPoints > 0)
        {
            skillPoints--;
            PlayerPrefs.SetInt("SkillPoints", skillPoints);
            skillPointsText.text = skillPoints.ToString();
            print("Used skillpoint");
        }
        else
            return;

        switch (stat)
        {
            case "Damage":
                print("Changing damage text");
                damage++;
                PlayerPrefs.SetInt("Damage", damage);
                damageText.text = damage.ToString();
                break;
            case "Health":
                health += 10;
                PlayerPrefs.SetInt("Health", health);
                healthText.text = health.ToString();
                myHealth.maxHealth = health;
                myHealth.GainHealth(10);
                break;
            case "Armor":
                armor++;
                PlayerPrefs.SetInt("Armor", armor);
                armorText.text = armor.ToString();
                break;
            case "CritChance":
                critChance++;
                PlayerPrefs.SetInt("CritChance", critChance);
                critChanceText.text = critChance.ToString();
                break;
        }
    }

    public void GainSkillPoint()
    {
        skillPoints++;
        PlayerPrefs.SetInt("SkillPoints", skillPoints);
        skillPointsText.text = skillPoints.ToString();
    }
}
