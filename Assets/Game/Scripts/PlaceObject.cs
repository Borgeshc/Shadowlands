using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour 
{
	public LayerMask layerMask;
	public GameObject turret;
	public int maxTurrets;
	int placedObjects;
	bool placingObject;
	public bool canPlace;
	public List<GameObject> activeTurrets;
	RaycastHit hit;


	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.E))
		{
			canPlace = !canPlace;
		}

		if (canPlace && Input.GetMouseButtonDown(0)) 
		{
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, layerMask))
			{
				if(hit.collider.tag == "Ground")
				{
					if (placedObjects < maxTurrets && !placingObject) 
						{
							placingObject = true;
							placedObjects++;
							GameObject newTurret = Instantiate (turret, transform.position, transform.rotation) as GameObject;
							activeTurrets.Add (newTurret);
							newTurret.transform.position = new Vector3 (hit.point.x, newTurret.transform.localScale.y * .05f, hit.point.z);
							placingObject = false;	
						} 
						else if (activeTurrets.Count == maxTurrets && !placingObject) 
						{
							//Overrite and destroy the old turret
							Destroy (activeTurrets [0]);
							activeTurrets.Remove (activeTurrets [0]);
							placedObjects--;

							//Spawn the new turret
							placingObject = true;
							placedObjects++;
							GameObject newTurret = Instantiate (turret, transform.position, transform.rotation) as GameObject;
							activeTurrets.Add (newTurret);
							newTurret.transform.position = new Vector3 (hit.point.x, newTurret.transform.localScale.y * .05f, hit.point.z);
							placingObject = false;	
						}

				}
			}
		}
	}
}
