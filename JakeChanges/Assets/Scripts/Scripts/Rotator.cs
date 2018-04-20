using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	public float spin = 5.0f;

	void Start ()
	{
	}
	void Update () 
	{
		transform.Rotate (Vector3.up, spin * Time.deltaTime);
	}
}
