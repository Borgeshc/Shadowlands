using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnRadius : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("SpawnNode"))
        {
            other.GetComponent<SpawnNode>().NodeActivated();
        }
    }
}
