using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempInvisibleWall : MonoBehaviour {

	private bool floorIsActive = true;
	public GameObject walkable;

	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (floorIsActive == false) 
		{
			
			walkable.SetActive (false);
		} 
		else 
		{
			
			walkable.SetActive (true);
		}

	}

	void OnTriggerEnter (Collider other) 
	{
		if (other.tag == "Block") 
		{
			floorIsActive = false;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Block") 
		{
			floorIsActive = true;
		}
	}

}
