using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurebridge : MonoBehaviour {

	public Animator bridgeAnim;
	private bool bridgeCooldown;
	private Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter (Collider other)
	{
		if ((other.tag == "Player" || other.tag == "Block") && bridgeCooldown == false) {
			StartCoroutine (BridgeCooldown ());
		}
	}

	void OnTriggerExit (Collider other)
	{
		if ((other.tag == "Player" || other.tag == "Block") && bridgeCooldown == false) {
			StartCoroutine (BridgeCooldown ());
		}
	}

	IEnumerator BridgeCooldown () {
		bridgeCooldown = true;
		//anim.SetTrigger ("Press");
		bridgeAnim.SetTrigger ("Bridge");
		yield return new WaitForSeconds(0.1f);
		bridgeCooldown = false;
	}
}
