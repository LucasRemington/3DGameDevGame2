using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerTest : MonoBehaviour {

	public float health = 100.0f;
	public Transform[] destinations;
	public int maxDestinations = 2;
	public int currentDestination = 0;
	private NavMeshAgent botAgent;
	public float idleTime = 2.0f;
	private bool isReverse = false;


	void Start () 
	{
		//currentDestination = Random.Range (0, maxDestinations - 1);
		botAgent = GetComponent<NavMeshAgent> ();
		StartCoroutine (AILoop ());


	}
	
	// Update is called once per frame
	void Update () 
	{
		if (health <= 0.0f) 
		{
			//Destroy (gameObject);
		}
	}
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "PlayerAttack") 
		{
			health = health - 20.0f;
		}
	}
	private IEnumerator AILoop () 
	{
		yield return StartCoroutine (SetPath ());
		if (health > 0) 
		{
			yield return StartCoroutine (CheckPath ());
		}

		StartCoroutine (AILoop ());

	}
	private IEnumerator SetPath () 
	{
		botAgent.destination = destinations [currentDestination].position;
		yield return null;
	}
	private IEnumerator CheckPath () 
	{
		if (botAgent.remainingDistance <= 0) 
		{

			if (currentDestination == maxDestinations - 1 && isReverse == false) 
			{
				isReverse = true;
			}
			if (currentDestination < maxDestinations - 1 && isReverse == false) 
			{
				currentDestination++;
			}
			if (currentDestination > 0 && isReverse == true) 
			{
				currentDestination = currentDestination - 1;
			}
			if (currentDestination == 0 && isReverse == true) 
			{
				isReverse = false;
			}

			yield return new WaitForSeconds (idleTime);
		} 
		else
		{
			yield return null;
		}
	}
}
