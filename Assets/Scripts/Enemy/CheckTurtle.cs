﻿using UnityEngine;
using System.Collections;

public class CheckTurtle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "WeaponPlayer")
			transform.GetComponentInParent<Turtle> ().speed *= -1;
	}
}
