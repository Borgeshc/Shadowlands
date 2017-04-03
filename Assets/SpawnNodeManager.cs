using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNodeManager : MonoBehaviour
{
    public static List<GameObject> spawnNodes;
    
	void Start ()
    {
        Application.DontDestroyOnLoad(gameObject);
        spawnNodes = new List<GameObject>();
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("SpawnNode"))
        {
            spawnNodes.Add(go);
        }

        for(int i = 0; i < spawnNodes.Count; i++)
        {
            spawnNodes[i].name = "SpawnNode" + i;
        }
	}

    private void OnLevelWasLoaded(int level)
    {
        Application.DontDestroyOnLoad(gameObject);
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("SpawnNode"))
        {
            if(!spawnNodes.Contains(go))
            spawnNodes.Add(go);
        }

        for (int i = 0; i < spawnNodes.Count; i++)
        {
            spawnNodes[i].name = "SpawnNode" + i;
        }
    }

    public void SpawnNodeUsed (string spawnNodeName)
    {
        PlayerPrefsX.SetBool(spawnNodeName, true);
	}
}
