using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnEnable : MonoBehaviour
{
    public GameObject objectToSpawn;
    bool spawned;

    private void OnEnable()
    {
        spawned = false;
        if(!spawned)
        {
            spawned = true;
            Instantiate(objectToSpawn);
        }
    }
}
