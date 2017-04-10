using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour 
{
	public string level;
    public int entranceNumber;
    public int exitNumber;

    public GameObject entrancePosition;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
	    {
            PlayerPrefs.SetInt("EntranceNumber", entranceNumber);
            PlayerPrefs.SetString("GameStatus", "Entrance");
			SceneManager.LoadScene (level);
		}
	}
}
