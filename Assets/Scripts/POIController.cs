using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class POIController : MonoBehaviour {

	public List<GameObject> POIList;

	private int totalPOIs;
	private int POIsFound;

	void Start () 
	{
		// Build the initial list of points of interest
		FetchPOIs();

		totalPOIs = POIList.Count;
		POIsFound = 0;
	}

	private void FetchPOIs()
	{
		foreach (Transform child in transform)
			POIList.Add(child.gameObject);
	}

	public void POIFound(GameObject point) 
	{
		++POIsFound;
		Destroy(point);
	}
}
