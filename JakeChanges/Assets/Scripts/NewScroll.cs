using UnityEngine;
using System.Collections;

public class NewScroll : MonoBehaviour {

	public Renderer bg;

	public float speed = 0.1f;

	// Use this for initialization
	void Start () {
		bg = GetComponent<Renderer> ();
	}

	// Update is called once per frame
	void Update () {
		Vector2 offset = new Vector2(Time.time * speed, 0);
		GetComponent<Renderer>().material.mainTextureOffset = offset;       
	}﻿
}
