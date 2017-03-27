using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFires : MonoBehaviour
{
    public Health dragonHealth;
    GameObject groundFire;
    bool done;

    void Start()
    {
        groundFire = transform.FindChild("GroundFire").gameObject;
    }
    void Update()
    {
        if (dragonHealth.health <= 0)
        {
            groundFire.SetActive(false);
            GetComponent<DamageOnStay>().damage = 0;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireBreath" && other.gameObject.activeInHierarchy)
        {
			StartCoroutine (SetOnFire ());
        }
    }

	IEnumerator SetOnFire()
	{
		yield return new WaitForSeconds (2);
		groundFire.SetActive(true);
		GetComponent<DamageOnStay>().damage = 5;
	}
}
