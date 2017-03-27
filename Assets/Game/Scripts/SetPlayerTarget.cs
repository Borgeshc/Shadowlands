using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerTarget : MonoBehaviour
{
    public GameObject bossHealthBar;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            bossHealthBar.SetActive(true);
            Sarathos.attackPlayer = true;
        }
    }
}
