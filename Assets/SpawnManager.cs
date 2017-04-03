using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static int classChosen;
    public GameObject[] classes;

    GameObject playerPrefab;
    GameObject player;
    bool saving;
    float lastTime;

    void Start ()
    {
        classChosen = 1;
        playerPrefab = Instantiate(classes[classChosen]) as GameObject;
        player = playerPrefab.transform.FindChild("Player").gameObject;
        player.transform.position = PlayerPrefsX.GetVector3("LastPosition");
        player.transform.rotation = PlayerPrefsX.GetQuaternion("LastRotation");

    }
	
	void Update ()
    {
		if((int)Time.time % 30 == 0)
        {
            PlayerPrefsX.SetVector3("LastPosition", player.transform.position);
            PlayerPrefsX.SetQuaternion("LastRotation", player.transform.rotation);
        }
	}

    private void OnApplicationQuit()
    {
        PlayerPrefsX.SetVector3("LastPosition", player.transform.position);
        PlayerPrefsX.SetQuaternion("LastRotation", player.transform.rotation);
    }
}
