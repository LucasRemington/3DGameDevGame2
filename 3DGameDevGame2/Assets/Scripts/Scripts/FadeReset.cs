using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeReset : MonoBehaviour {

	//private PlayerControl playerScript;
	public Transparency transparencyScript;
	private Material defaultMat;
	public bool isTransparent = false;


	// Use this for initialization
	void Start () 
	{
		//playerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerControl> ();
		defaultMat = GetComponent <Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isTransparent == false) 
		{
			GetComponent <Renderer> ().material = defaultMat;
		}
	}
}
