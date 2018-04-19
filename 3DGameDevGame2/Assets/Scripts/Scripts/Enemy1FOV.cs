using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1FOV : MonoBehaviour {

	public bool playerInSight = false;
	public GameObject player;
	private EnemyController enemyScript;


	void Start () 
	{
		enemyScript = gameObject.GetComponentInParent <EnemyController> ();
	}
	void Update () 
	{
		if (playerInSight == true) 
		{
			enemyScript.inCombat = true;
		}
	}
	void OnTriggerEnter (Collider other) 
	{
		if (other.tag == "Player") 
		{
			playerInSight = true;
			player = other.gameObject;
		}
	}
	void OnTriggerExit (Collider other) 
	{
		if (other.tag == "Player") 
		{
			playerInSight = false;
		}
	}
}
