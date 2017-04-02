using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNode : MonoBehaviour
{
    public GameObject[] levelOneEnemies;
    public GameObject[] levelTwoEnemies;
    public GameObject[] levelThreeEnemies;
    public int enemyDifficultyLevel;
    public int allowedSpawnAmount;

    List<GameObject> activatedEnemies;

    public void NodeActivated()
    {
        PopulateActivatedEnemies();
        Spawn();
        Destroy(gameObject);
    }

    void PopulateActivatedEnemies()
    {
        if (enemyDifficultyLevel >= 1)
        {
            for (int i = 0; i < levelOneEnemies.Length; i++)
            {
                activatedEnemies.Add(levelOneEnemies[i]);
            }

            if (enemyDifficultyLevel >= 2)
            {
                for (int j = 0; j < levelTwoEnemies.Length; j++)
                {
                    activatedEnemies.Add(levelTwoEnemies[j]);
                }

                if (enemyDifficultyLevel >= 3)
                {
                    for (int k = 0; k < levelThreeEnemies.Length; k++)
                    {
                        activatedEnemies.Add(levelThreeEnemies[k]);
                    }
                }
            }
        }
    }

    void Spawn()
    {
        for(int i = 0; i < allowedSpawnAmount; i++) //For the amount of enemies allowed to spawn
        {
            int randomEnemy = Random.Range(0, activatedEnemies.Count);  //Choose a random enemypool from the list of activated enemies

            GameObject enemy = activatedEnemies[randomEnemy].GetComponent<ObjectPooling>().GetPooledObject();   //Grab a deactivated instance of that enemy

            if (enemy == null)
            {
                return;
            }

            enemy.transform.position = SpawnPosition();
            enemy.SetActive(true);
        }
    }

    Vector3 SpawnPosition()
    {
        Vector3 newSpawnPosition = new Vector3(transform.position.x + Random.Range(0, 10), transform.position.y, transform.position.z + Random.Range(0, 10));
        return newSpawnPosition;
    }
}
