using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	public float health = 100.0f;
	public Transform[] destinations;
	public int maxDestinations = 2;
	public int currentDestination = 0;
	private NavMeshAgent botAgent;
	public float idleTime = 2.0f;
	private bool isReverse = false;
	public bool inCombat = false;
	private bool isDead = false;
	private Enemy1FOV fovScript;
	private bool targetinRange;
	private float distance;
	public float attackRange = 5.0f;
	public GameObject player;
	public Transform attackSpawn;
	public GameObject attackHitbox;
	public float attackCooldown;
	public GameObject[] tempHealth;
	private int onHealth;
	private bool hurtCooldown;

	void Start () 
	{
		//currentDestination = Random.Range (0, maxDestinations - 1);
		botAgent = GetComponent<NavMeshAgent> ();
		botAgent.destination = destinations [currentDestination].position;
		StartCoroutine (AILoop ());
		StartCoroutine (AttackLoop ());
		onHealth = -1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (health <= 0.0f) 
		{
			isDead = true;
			Destroy (gameObject);
			PlayerControl.Gold = PlayerControl.Gold + 15;
		}
		fovScript = gameObject.GetComponentInChildren <Enemy1FOV> ();
		if (fovScript.playerInSight == false) 
		{
			inCombat = false;
		}
		if (fovScript.player) 
		{
			distance = Vector3.Distance (fovScript.player.transform.position, transform.position);
			if (distance <= attackRange) 
			{
				targetinRange = true;
			} 
			else 
			{
				targetinRange = false;
			}
			player = fovScript.player;
		}
	}
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "PlayerAttack" && hurtCooldown == false) 
		{
			inCombat = true;
			health = health - 20.0f;
			onHealth++;
			tempHealth [onHealth].SetActive(false);
			StartCoroutine (hurtCool ());
		}
	}
	private IEnumerator hurtCool(){
		hurtCooldown = true;
		yield return new WaitForSeconds(1.5f);
		hurtCooldown = false;
	}

	private IEnumerator AILoop () 
	{
		if (isDead == false) 
		{
			yield return StartCoroutine (CheckPath ());
			yield return StartCoroutine (SetPath ());
			StartCoroutine (AILoop ());
		}
			
	}
	private IEnumerator SetPath () 
	{

		if (inCombat == false && isDead == false) 
		{
			botAgent.destination = destinations [currentDestination].position;
		}
		if (inCombat == true && isDead == false) 
		{
			botAgent.destination = gameObject.GetComponentInChildren <Enemy1FOV> ().player.transform.position;
		}
		yield return null;
	}
	private IEnumerator CheckPath () 
	{
		if (botAgent.remainingDistance <= 0) 
		{
			if (inCombat == false) 
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
		} 
		else
		{
			yield return null;
		}
	}
	private IEnumerator AttackLoop ()
	{
		yield return StartCoroutine (Attack());
		StartCoroutine (AttackLoop ());
	}
	private IEnumerator Attack ()
	{
		if (targetinRange == true && player) 
		{
			transform.LookAt (player.transform);
			botAgent.isStopped = true;
			Instantiate (attackHitbox, attackSpawn.position, transform.rotation);
			botAgent.isStopped = false;
			yield return new WaitForSeconds (attackCooldown);
		} 
		else 
		{
			yield return null;
		}
	}
}
