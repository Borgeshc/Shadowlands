using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public LayerMask layerMask;
	public float moveSpeed;
	Vector3 destinationPosition;
	Quaternion targetRotation;
	CharacterController cc;
	Vector3 movedir;

	void Start()
	{
		cc = GetComponent<CharacterController> ();
	}
	void Update()
	{
		ClickToMove ();
	}
	void ClickToMove()
	{
		// Moves the Player if the Left Mouse Button was clicked
		if (Input.GetMouseButtonDown (0) && GUIUtility.hotControl == 0) 
		{ // && dontClick == false)
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitdist;

			if (Physics.Raycast (ray, out hitdist, 100, layerMask)) 
			{

				if (hitdist.collider.tag != "Player") 
				{
					Vector3 targetPoint = Vector3.zero;
					targetPoint.x = hitdist.point.x;
					targetPoint.y = transform.position.y;
					targetPoint.z = hitdist.point.z;
					destinationPosition = hitdist.point;
					targetRotation = Quaternion.LookRotation (targetPoint - transform.position);
				}
			}
		}
			
		//transform.rotation = Quaternion.Lerp(this.transform.rotation,targetRotation,Time.deltaTime *25);

			//	movedir = Vector3.zero;
				movedir = transform.TransformDirection(Vector3.forward*moveSpeed);
	//	}else
	//	{

	//	movedir = Vector3.Lerp(movedir,Vector3.zero,Time.deltaTime * 10);	

	//	}
	//	movedir.y -= 20 * Time.deltaTime;
		cc.SimpleMove(movedir * Time.deltaTime);	

	}
}
