using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour {

	private NavMeshAgent navMeshAgent;
	public Transform target;
	public bool targetIsEnemy = false;
	public float attackRange = 5.0f;
	public float attackRate = 1.0f;
	private float attackCooldown;
	public int keyCount = 0;
	public GameObject attackHitbox;
	public Transform attackSpawn;
	public float health = 100.0f;
	public bool isBlocking = false;
	public GameObject meleeHitbox;
	private Lifetime lifetimeScript;
	private float distance;
	private Rigidbody rigBod;

	void Start () 
	{
		navMeshAgent = GetComponent <NavMeshAgent> ();
		navMeshAgent.updateRotation = false;
		rigBod = GetComponent <Rigidbody> ();
	}
	void Update () 
	{
		//Ray directionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		//RaycastHit currentTarget;

		rigBod.angularVelocity = Vector3.zero;

		if (Input.GetKey (KeyCode.Mouse1)) 
		{
			isBlocking = true;
			navMeshAgent.isStopped = true;
		} 
		else 
		{
			isBlocking = false;
		}
		if (Input.GetKey (KeyCode.Mouse0)) 
		{
			Ray directionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit currentTarget;

			if (Physics.Raycast (directionRay, out currentTarget, 200)) 
			{
				target = currentTarget.transform;
				//transform.LookAt (currentTarget.point);
				if (currentTarget.collider.tag == "Hostile") 
				{
					targetIsEnemy = true;
				} 
				else
				{
					targetIsEnemy = false;
				}
	
				if (isBlocking) 
				{
					navMeshAgent.isStopped = true;
				} 
				else
				{
					navMeshAgent.destination = currentTarget.point;
					navMeshAgent.isStopped = false;
				}
				if (target && targetIsEnemy && isBlocking == false) 
				{
					//navMeshAgent.isStopped = true;
					Attack ();
				}


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
		if (other.tag == "SmallEnemyAttack1" && isBlocking == false) 
		{
			health = health - 10.0f;
		}
		if (other.tag == "Hostile" && targetIsEnemy == true) 
		{
			navMeshAgent.isStopped = true;
		}

	}
	private void Attack ()
	{
		if (targetIsEnemy || target) 
		{
			navMeshAgent.destination = target.position;
			//if (navMeshAgent.remainingDistance >= attackRange) 
			//	{
			//	navMeshAgent.isStopped = false;
			//	}
			distance = Vector3.Distance (target.position, transform.position);
			if (distance <= attackRange) 
			{
				transform.LookAt (target);
				navMeshAgent.isStopped = true;
				if (Time.time > attackCooldown) 
				{
					attackCooldown = Time.time + attackRate;
					Instantiate (attackHitbox, attackSpawn.position, transform.rotation);
				}
			}
		}
	}
}
