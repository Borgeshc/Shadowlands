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
		SceneManager.LoadScene ("World");
	}

	public void Settings()
	{
		
	}

	public void Quit()
	{
		Application.Quit();
	}
}
