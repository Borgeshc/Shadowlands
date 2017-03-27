using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProximityAI : MonoBehaviour
{
    GameObject player;
    public float speed;
    public float stoppingDistance;

    public int damage;
    public float attackFrequency;

    bool attacking;
    Health myHealth;
    public Animator anim;
    NavMeshAgent nav;

	float turnSpeed = 5;

	StatusEffect checkStatus;
	Vector3 lookPosition;
	Quaternion rotation;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        myHealth = GetComponent<Health>();
		checkStatus = GetComponent<StatusEffect> ();
    }

    public void InRange(Collider other)
    {
		if (!myHealth.isDead && myHealth.health > 0 && player != null)
		{
			if (player.activeInHierarchy && myHealth.health > 0) 
			{
				lookPosition = player.transform.position - transform.position;
				lookPosition.y = 0;
				rotation = Quaternion.LookRotation (lookPosition);
				transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * turnSpeed);

				if (Vector3.Distance (transform.position, player.transform.position) > stoppingDistance) 
				{
					anim.SetBool ("Attack", false);
					anim.SetBool ("IsWalking", true);
					nav.SetDestination (player.transform.position);
				} 
				else 
				{
					if (!attacking) 
					{
						attacking = true;
						StartCoroutine (Attack ());
					}
				}
			}
		} 
		else 
		{
			anim.SetBool ("IsWalking", true);
			nav.Stop ();
		}
    }

    IEnumerator Attack()
    {
        anim.SetBool("Attack", true);

		if (Block.isBlocking && !checkStatus.statusEffectActive) 
		{
			checkStatus.OnFire (2, 5);
		}

		if(player.GetComponent<Health>() != null)
        player.GetComponent<Health>().TookDamage(damage);
		
        yield return new WaitForSeconds(attackFrequency);
        attacking = false;
    }
}
