using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
	InforStrength player;

	// Use this for initialization
	void Awake() {
		//player = GameObject.FindGameObjectWithTag("Player").GetComponent<InforStrength> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D coll) {
        if (coll.gameObject.tag == "Player")
        {
           // player.Health = 0;
            Application.LoadLevel("level1");
        }
	}



}
