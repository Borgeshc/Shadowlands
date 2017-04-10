using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public int classChosen;
    public GameObject[] classes;
    GameObject freshGamePosition;

    GameObject playerPrefab;
    GameObject player;
    bool saving;
    float lastTime;

    private void OnLevelWasLoaded(int level)
    {
        print("Called");
        PlayerPrefs.SetString("LastScene", Application.loadedLevelName);
        freshGamePosition = GameObject.Find("FreshGamePosition").gameObject;

        classChosen = PlayerPrefs.GetInt("ClassChosen");
        
        playerPrefab = Instantiate(classes[classChosen]) as GameObject;

        player = playerPrefab.transform.FindChild("Player").gameObject;

        if (PlayerPrefs.GetString("GameStatus") == "NewGame")
        {
            player.transform.position = freshGamePosition.transform.position;
        }
        else if(PlayerPrefs.GetString("GameStatus") == "Entrance")
        {
            foreach(GameObject go in GameObject.FindGameObjectsWithTag("Entrance"))
            {
                if(go.GetComponent<LoadLevel>().entranceNumber == PlayerPrefs.GetInt("EntranceNumber"))
                {
                    player.transform.position = go.GetComponent<LoadLevel>().entrancePosition.transform.position;
                }
            }
        }
        else
        {
            player.transform.position = PlayerPrefsX.GetVector3("LastPosition");
            player.transform.rotation = PlayerPrefsX.GetQuaternion("LastRotation");
        }
        
    }

    void Update ()
    {

        if ((int)Time.time % 5 == 0)
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
