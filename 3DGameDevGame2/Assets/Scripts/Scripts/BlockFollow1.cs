using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFollow1 : MonoBehaviour {

	public Vector3 offset;
	public GameObject player;
	public bool dragging = false;
	private bool canDrag;


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () 

	{
		if (Input.GetKeyDown (KeyCode.Space) && canDrag == true)
			{
			offset = transform.position - player.transform.position;
			dragging = true;
			}
		if (Input.GetKeyUp (KeyCode.Space))
		{

			dragging = false;
		}

		if (dragging == true) 
		{
			transform.position = player.transform.position + offset;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") 
		{
			canDrag = true;
		}
	}
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player") 
		{
			canDrag = false;
		}
	}
}
