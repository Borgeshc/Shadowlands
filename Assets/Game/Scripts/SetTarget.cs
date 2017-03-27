using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTarget : MonoBehaviour 
{
	public GameObject myMesh;
	Shader originalShader;
	Renderer rend;
	Shader outlineShader;

	// Use this for initialization
	void Start () 
	{
		rend = myMesh.GetComponent<Renderer> ();
		originalShader = rend.material.shader;
		outlineShader = Shader.Find ("Custom/OutlineDiffuse");
	}

	public void Targeted()
	{
		rend.material.shader = outlineShader;
	}

	public void NotTargeted()
	{
		rend.material.shader = originalShader;
	}
}
