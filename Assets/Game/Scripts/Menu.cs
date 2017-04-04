using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour 
{
	public void NewGame()
	{
        SceneManager.LoadScene ("World");
    }

	public void Continue()
	{
        PlayerPrefs.SetString("GameStatus", "Continue");
        SceneManager.LoadScene (PlayerPrefs.GetString("LastScene"));
	}

	public void Settings()
	{
		
	}

	public void Quit()
	{
		Application.Quit();
	}

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("GameStatus", "NewGame");
    }
}
