using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour {

	private NavMeshAgent navMeshAgent;
	public Transform target;
	public bool targetIsEnemy = false;
	public float attackRange = 5.0f;
	public float attackRate = 1.0f;
	private float attackCooldown;
	private bool blockCooldown;
	public int keyCount = 0;
	public GameObject attackHitbox;
	public Transform attackSpawn;
	public float health = 100.0f;
	public bool isBlocking = false;
	public GameObject meleeHitbox;
	private Lifetime lifetimeScript;
	private float distance;
	private Rigidbody rigBod;
	public Animator anim;
	public Text healthText;
	public Text goldText;
	public Image keyImage;
	public Animator doorAnim;
	public Animator chestAnim;
	public static int Gold;
	public BoxCollider chestBox;

	void Start () 
	{
		//anim = GetComponent <Animator> ();
		blockCooldown = false;
		navMeshAgent = GetComponent <NavMeshAgent> ();
		navMeshAgent.updateRotation = false;
		rigBod = GetComponent <Rigidbody> ();
		keyImage.enabled = false;
		Gold = 0;
	}
	void Update () 
	{
		//Ray directionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		//RaycastHit currentTarget;
		//Turn();

		healthText.text = "Health: " + health.ToString ();
		goldText.text = "Gold: " + Gold.ToString ();

		if (navMeshAgent.velocity != Vector3.zero) {
			anim.SetBool ("Idle", false);
				} else {
					anim.SetBool ("Idle", true);
				}

		rigBod.angularVelocity = Vector3.zero;

		if (Input.GetKey (KeyCode.Mouse1) && blockCooldown == false) 
		{
			StartCoroutine (BlockCooldown ());
		} 

		if (Input.GetKey (KeyCode.Mouse0)) 
		{
			Ray directionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit currentTarget;

			if (Physics.Raycast (directionRay, out currentTarget, 200)) 
			{
				target = currentTarget.transform;
				transform.LookAt (currentTarget.point);

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
			keyImage.enabled = true;
			Gold = Gold + 10;
		}
		if (other.tag == "Door" && keyCount > 0) 
		{
			doorAnim = other.GetComponent<Animator>();
			doorAnim.SetTrigger ("Open");
			keyImage.enabled = false;
			Gold = Gold + 10;
		}
		if (other.tag == "Chest") 
		{
			chestAnim = other.GetComponent<Animator>();
			chestBox = other.GetComponent<BoxCollider> ();
			chestBox.enabled = false;
			chestAnim.SetTrigger ("Open");
			Gold = Gold + 30;
		}
		if (other.tag == "SmallEnemyAttack1" && isBlocking == false) 
		{
			health = health - 10.0f;
			if (health <= 10f) {
				anim.SetTrigger ("Death");
				navMeshAgent.enabled = false;
			} else {
				anim.SetTrigger ("Knockback");
			}
		}
		if (other.tag == "Hostile" && targetIsEnemy == true) 
		{
			navMeshAgent.isStopped = true;
		}

	}

	IEnumerator BlockCooldown () {
		blockCooldown = true;
		anim.SetTrigger ("Block");
		isBlocking = true;
		navMeshAgent.isStopped = true;
		yield return new WaitForSeconds(1.5f);
		blockCooldown = false;
		isBlocking = false;
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
					anim.SetTrigger ("Attack");
					attackCooldown = Time.time + attackRate;
					Instantiate (attackHitbox, attackSpawn.position, transform.rotation);
				}
			}
		}
	}

	private void Turn () 
	{
		Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);
		Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint (Input.mousePosition);
		float angle = AngleBetweenTwoPoints (positionOnScreen, mouseOnScreen);
		transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, angle));
	}

	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2 (a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}
}
