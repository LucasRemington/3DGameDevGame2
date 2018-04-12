using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour {

	private NavMeshAgent navMeshAgent;
	private Transform target;

	public int keyCount = 0;

	void Start () 
	{

		navMeshAgent = GetComponent <NavMeshAgent> ();
		navMeshAgent.updateRotation = false;
	}
	void Update () 
	{
		
		Ray directionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit currentTarget;
		if (Input.GetKey (KeyCode.Mouse0)) 
		{
			if (Physics.Raycast (directionRay, out currentTarget, 120)) 
			{
				
				navMeshAgent.destination = currentTarget.point;
			}
		}
	}
	void OnTriggerEnter (Collider other) 
	{
		if (other.tag == "Key") 
		{
			keyCount++;
			Destroy (other.gameObject);
		}
		if (other.tag == "Door" && keyCount > 0) 
		{
			Destroy (other.gameObject);
		}
	}
}
