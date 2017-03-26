using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{
	public static bool canMove;
	public static bool canRotate;
	public Animator anim;
	public float speed = 10.0f;
	CharacterController cc;
	public Transform gameObjectToRotate;

	Vector3 middleOfScreen;
	Health myHealth;
	
	void Start()
	{
		myHealth = GetComponent<Health> ();
		canMove = true;
		canRotate = true;
		//Cursor.visible = false;
		cc = GetComponent<CharacterController>();
		middleOfScreen = new Vector3(Screen.width/2, Screen.height/2, 0f);
	}

	void FixedUpdate()
	{
		if (canRotate && myHealth.health > 0) 
		{
			Vector3 camVec = Input.mousePosition - middleOfScreen;
			Vector3 flipped = new Vector3 (camVec.x, 0f, camVec.y);
			gameObjectToRotate.LookAt (flipped);
		}
		if (Input.GetKey (KeyCode.LeftShift) || !canMove && myHealth.health > 0) 
		{
			anim.SetBool ("IsWalking", false);
			cc.Move (Vector3.zero);
		} 
		else 
		{
			if (myHealth.health > 0) 
			{
				if (Input.mousePosition.x > middleOfScreen.x + (Screen.width * .3f)
					|| Input.mousePosition.y > middleOfScreen.y + (Screen.height * .3f)
					|| Input.mousePosition.x < middleOfScreen.x + (Screen.width * -.3f)
					|| Input.mousePosition.y < middleOfScreen.y + (Screen.height * -.3f)) 
				{
					anim.SetBool ("IsWalking", true);
					cc.SimpleMove (transform.forward * speed * Time.deltaTime);
				} else 
				{
					anim.SetBool ("IsWalking", false);
					cc.SimpleMove(Vector3.zero);
				}
			}
		}
	}	
}
