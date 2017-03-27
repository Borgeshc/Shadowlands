using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAfter : MonoBehaviour 
{
	public float spawnAfter;
	ObjectPooling barragePool;

	void Start () 
	{
		barragePool = GameObject.Find ("BarragePool").GetComponent<ObjectPooling>();
		StartCoroutine (WaitForSpawn ());
	}

	void OnEnable()
	{
		StartCoroutine (WaitForSpawn ());
	}

	IEnumerator WaitForSpawn()
	{
		yield return new WaitForSeconds (spawnAfter);

		GameObject obj = barragePool.GetPooledObject();

		obj.transform.position = transform.position;
		obj.SetActive(true);
	}
}
