using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Survival : MonoBehaviour 
{
	public List<GameObject> enemies;
	public List<GameObject> spawnpoints;
	public float survivalTime;
	public float spawnFrequency;

	public Text countdownText;

	int randomEnemy; //The enemy that was chosen at random

	float timeLeft;	//Time left in event

	bool canSpawn;	//Is the event running?
	bool spawning; //Are you currently spawning
	bool startEvent; //Checks to see if its time to start the event
	bool endingEvent; //Checks to see if the event has already ended

	AudioSource source;

	void Start ()
	{
		timeLeft = survivalTime;
		canSpawn = true;

		if (GetComponent<AudioSource> () != null) //Makes a sound once the event begins.
		{
			source = GetComponent<AudioSource> ();
			source.Play();
		}

		StartCoroutine (SurvivalEventStarting ());
	}

	void Update () 
	{   
		if (startEvent) 
		{
			timeLeft -= Time.deltaTime;
			countdownText.text = "" + (int)timeLeft;

			if (canSpawn && !spawning) 
			{
				spawning = true;
				StartCoroutine(Spawn ());
			}
			if(timeLeft < 0)
			{
				if (!endingEvent) 
				{
					endingEvent = true;
					canSpawn = false;
					StartCoroutine (EventComplete ());
					TriggerEvent.eventActive = false;
					Destroy (gameObject);
				}
			}
		}
	}

	IEnumerator Spawn()
	{
		Instantiate(enemies[Random.Range(0, enemies.Count)], spawnpoints[Random.Range(0, spawnpoints.Count)].transform.position, Quaternion.identity);
		yield return new WaitForSeconds (spawnFrequency);
		spawning = false;
	}

	IEnumerator SurvivalEventStarting()
	{
		for (int i = 3; i > 0; i--) 
		{
			countdownText.text = "Survival Event Starts In " + i + "...";
			yield return new WaitForSeconds (1);
		}
		startEvent = true;
	}

	IEnumerator EventComplete()
	{
		countdownText.text = "Event Complete!";
		yield return new WaitForSeconds (2);
	}

}
