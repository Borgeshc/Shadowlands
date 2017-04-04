using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNodeManager : MonoBehaviour
{
    public List<GameObject> spawnNodes;
    bool initiated;

    private void OnLevelWasLoaded(int level)
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("SpawnManager"))
        {
            Destroy(go);
        }

        if (!initiated)
        {
            initiated = true;
            spawnNodes = new List<GameObject>();
        }

        Application.DontDestroyOnLoad(gameObject);
        PopulateList();
 
    }

    void PopulateList()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("SpawnNode"))
        {
            if (!spawnNodes.Contains(go))
                spawnNodes.Add(go);
        }

        for (int i = 0; i < spawnNodes.Count; i++)
        {
            if(spawnNodes[i] != null)
            spawnNodes[i].name = "SpawnNode" + i;
        }
    }

    public void SpawnNodeUsed (string spawnNodeName)
    {
        PlayerPrefsX.SetBool(spawnNodeName, true);
	}
}
