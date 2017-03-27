using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sarathos : MonoBehaviour
{
    public static bool attackPlayer;
    public float speed;
    public float animationWaitTime;
    public float attackFrequency;
    public int meleeDamage;
    public GameObject fireBreath;
    [Space]
    [Space]
    public GameObject flightPhasePosition;
    public GameObject flightPhaseLookDirection;
    GameObject player;
    Animator anim;
    NavMeshAgent nav;
    Health myHealth;
    bool attacking;
    bool flightPhase1;
    bool flightPhase2;
    bool flightPhase3;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        myHealth = GetComponent<Health>();
    }

    void Update()
    {
        if (myHealth.health > 0)
        {
            if(myHealth.health/myHealth.maxHealth >= .7f && myHealth.health / myHealth.maxHealth <= .75f)
            {
                if(!flightPhase1)
                {
                    flightPhase1 = true;
                    StartCoroutine(PhaseTwo());
                }

                PhaseOne();
            }
            else if(myHealth.health / myHealth.maxHealth >= .45f && myHealth.health / myHealth.maxHealth <= .5f)
            {
                if (!flightPhase2)
                {
                    flightPhase2 = true;
                    StartCoroutine(PhaseTwo());
                }

                PhaseOne();
            }
            else if (myHealth.health / myHealth.maxHealth >= .2f && myHealth.health / myHealth.maxHealth <= .25f)
            {
                if (!flightPhase3)
                {
                    flightPhase3 = true;
                    StartCoroutine(PhaseTwo());
                }

                PhaseOne();
            }

            if(!flightPhase1 && !flightPhase2 && !flightPhase3)
                PhaseOne();
        }
    }

    void PhaseOne()
    {
        if (attackPlayer)
        {
            var lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);

            if (nav.remainingDistance > nav.stoppingDistance)
            {
                anim.SetBool("IsMoving", true);
                nav.SetDestination(player.transform.position);
            }
            else
            {
                anim.SetBool("IsMoving", false);
                if (!attacking)
                {
                    attacking = true;
                    StartCoroutine(Attack());
                }
            }
        }
    }

    IEnumerator PhaseTwo()
    {
        anim.SetBool("IsTakingOff", true);
        anim.SetLayerWeight(5, 1);
        yield return new WaitForSeconds(3.5f);
        anim.SetLayerWeight(5, 0);
        anim.SetLayerWeight(6, 1);
        anim.SetBool("IsTakingOff", false);
        var lookPos = flightPhasePosition.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
        yield return new WaitForSeconds(1);
        var newlookPos = flightPhaseLookDirection.transform.position - transform.position;
        lookPos.y = 0;
        var newrotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
        anim.SetBool("IsFlyAttacking", true);
        fireBreath.SetActive(true);
        yield return new WaitForSeconds(3f);
        anim.SetBool("IsFlyAttacking", false);
        fireBreath.SetActive(false);
        anim.SetBool("IsLanding", true);
        anim.SetLayerWeight(7, 1);
        anim.SetLayerWeight(6, 0);
        yield return new WaitForSeconds(3.5f);

        anim.SetBool("IsLanding", false);
        anim.SetLayerWeight(7, 0);
    }

    IEnumerator Attack()
    {
        anim.SetBool("IsAttacking", true);
        player.GetComponent<Health>().TookDamage(meleeDamage);
        yield return new WaitForSeconds(animationWaitTime);
        anim.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(attackFrequency);
        attacking = false;
    }
}
