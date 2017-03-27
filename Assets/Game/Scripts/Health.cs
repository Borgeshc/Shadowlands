using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour 
{
	public Animator anim;
	public float deathTime;
	public float maxHealth;
	public bool hasHealthBar;
	public Image healthBar;
	public float health;
	public Image deathPanel;
	public Text deathText;
	public GameObject combatText;

	float timer;
	bool hitEffect;
	[HideInInspector]
	public bool isDead;
	CapsuleCollider collision;
	public AudioClip[] hitEffectSounds;
	public AudioSource source;
	public float experienceWorth;
	bool isImmune;

	ExperienceManager xpManager;

	void Start()
	{
		health = maxHealth;
		xpManager = GameObject.Find ("GameManager").GetComponent<ExperienceManager> ();
		collision = GetComponent<CapsuleCollider> ();
	}

	public void TookDamage(int damage)
	{
		if (Block.isBlocking || isImmune) {
			return;
		} 

		health -= damage;		

		if (hasHealthBar)
			healthBar.fillAmount = (health / maxHealth);

		if (health <= 0) 
		{
			anim.SetBool ("Died", true);
			StartCoroutine(Died ());
		}
	}

	public void TookDamage( bool isCrit, int damage)
		{

		if (!isCrit) {
			CombatText (damage.ToString ()).GetComponent<Animator> ().SetTrigger ("CBT");

		} else {
			CombatText (damage.ToString ()).GetComponent<Animator> ().SetTrigger ("Crit");
		}
		
			health -= damage;		

			if (hasHealthBar)
				healthBar.fillAmount = (health / maxHealth);

		if (health <= 0 && isDead == false) 
			{
				anim.SetBool ("Died", true);
				StartCoroutine(Died ());
			}

			if (!hitEffect && health > 0) 
			{
				source.clip = hitEffectSounds [Random.Range (0, hitEffectSounds.Length)];
				source.Play ();
				hitEffect = true;
				anim.SetBool ("Hit", true);
				StartCoroutine (HitEffect ());
			}
	}

	IEnumerator Died()
	{

		if (transform.tag == "Enemy" || transform.tag == "Dragon" && isDead == false)
        {
            isDead = true;
            GetComponent<DropsGold> ().DropGold ();
			xpManager.GainExperience (experienceWorth);
			anim.SetLayerWeight (0, 0);
			anim.SetLayerWeight (2, 0);
			anim.SetLayerWeight (3, 0);
            if(collision != null)
			collision.enabled = false;
			if (TargetObject.highlightedTargets.Contains (gameObject))
				TargetObject.highlightedTargets.Remove (gameObject);

			if (CombatManager.inCombat)
				CombatManager.inCombat = false;
			
			if(TargetObject.target == this.gameObject)
			{
                if(transform.GetComponent<SetTarget>() != null)
				transform.GetComponent<SetTarget> ().NotTargeted ();
				TargetObject.target = null;
			}
		} 
		else if (transform.tag == "Player") 
		{
			StartCoroutine (Fade ());
		}

		yield return new WaitForSeconds (deathTime);
        if(transform.tag != "Dragon")
		Destroy (gameObject);
	}

	public void GainHealth(float healthGain)
	{
		if (health + healthGain < maxHealth) 
		{
			health += healthGain;
			healthBar.fillAmount = (health / maxHealth);
		} 
		else 
		{
			health = maxHealth;
			healthBar.fillAmount = (health / maxHealth);
		}
	}

	public IEnumerator GainHealthOverTime(float healthGain)
	{
		for (int i = 0; i < 10; i++) 
		{
			if (health + healthGain < maxHealth) 
			{
				health += healthGain;
				healthBar.fillAmount = (health / maxHealth);
			} 
			else 
			{
				health = maxHealth;
				healthBar.fillAmount = (health / maxHealth);
			}
			yield return new WaitForSeconds (1);
		}
	}

	public IEnumerator Immune()
	{
		isImmune = true;
		yield return new WaitForSeconds (10);
		isImmune = false;
	}

	IEnumerator Fade()
	{
		yield return new WaitUntil(FadingIn);
	}
	bool FadingIn()
	{
		deathPanel.color = Color.Lerp(deathPanel.color, new Color(0,0,0, .75f), Time.deltaTime * .5f);
		deathText.color = Color.Lerp(deathText.color, Color.red, Time.deltaTime * .25f);
		timer += 1 / 60.0f;
		if (deathPanel.color == Color.clear)
			return true;
		else
			return false;
	}

	IEnumerator HitEffect()
	{
		yield return new WaitForSeconds (.3f);
		anim.SetBool ("Hit", false);
		hitEffect = false;
	}

	GameObject CombatText(string damageText)
	{
		GameObject cbt = Instantiate (combatText) as GameObject;
		RectTransform cbtTransform = cbt.GetComponent<RectTransform> ();
		cbt.transform.SetParent (transform.FindChild ("Canvas"));
		cbtTransform.transform.localPosition = combatText.transform.localPosition;
		cbtTransform.transform.localScale = combatText.transform.localScale;
		cbtTransform.transform.localRotation = combatText.transform.localRotation;

		cbt.GetComponent<Text> ().text = damageText;
		Destroy (cbt.gameObject, 2);
		return cbt;
	}
}
