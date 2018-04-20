using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparency : MonoBehaviour {


	public Material opaqueMat;
	public Material transMat;
	public Camera cam;
	private Vector3 screenPosition;
	public GameObject player;
	public RaycastHit obstruction;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Ray directionRay = cam.ScreenPointToRay (playerPosition.position);

		Vector3 playerPosition = new Vector3 (0.5f, 0.5f, 0);

		//screenPosition = cam.WorldToScreenPoint (playerPosition);


		Ray directionRay = cam.ViewportPointToRay (playerPosition);

		RaycastHit obstruction;

		Debug.DrawRay (directionRay.origin, directionRay.direction * 50, Color.yellow);

		if (Physics.Raycast (directionRay, out obstruction, 200)) 
		{
			if (obstruction.collider.tag == "Wall") 
			{
				obstruction.transform.GetComponent <Renderer> ().material = transMat;
			}

		}
			
	}
}
