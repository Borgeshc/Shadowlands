using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour 
{
	public float resource;
	public float maxResource;

	public Image resourceBar;

	void Start()
	{
		resourceBar = GetComponent<Image> ();
	}

	public void GainResource(float resourceGained)
	{
		if (resource + resourceGained < maxResource)
			resource += resourceGained;
		else
			resource = maxResource;

		resourceBar.fillAmount = (resource / maxResource);
	}

	public void CostResource(float resourceCost)
	{
		if (resource - resourceCost > 0)
			resource -= resourceCost;
		else
			resource = 0;

		resourceBar.fillAmount = (resource / maxResource);
	}
}
