using System.Collections;
using UnityEngine;

public class CamGyroAR : MonoBehaviour 
{
	public GameObject cameraViewport;
	GameObject camParent;

	void Start () 
	{
		camParent = new GameObject ("CamParent");
		GetComponent<Camera>().transform.position = this.transform.position;
		this.transform.parent = camParent.transform;
		camParent.transform.Rotate (Vector3.right, 90);
		Input.gyro.enabled = true;

		WebCamTexture webCamTexture = new WebCamTexture ();
		cameraViewport.GetComponent<MeshRenderer> ().material.mainTexture = webCamTexture;
		webCamTexture.Play ();
	}

	void Update () 
	{
		Quaternion rotationFix = new Quaternion (Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);
		this.transform.localRotation = rotationFix;
	}
}
