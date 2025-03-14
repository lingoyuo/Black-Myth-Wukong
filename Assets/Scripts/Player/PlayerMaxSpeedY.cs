using UnityEngine;
using System.Collections;

public class PlayerMaxSpeedY : MonoBehaviour {
	public float maxSpeed = 40;

	Rigidbody2D rigid;
	// Use this for initialization

	void Awake () {
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (rigid.linearVelocity.y <= -maxSpeed)
			rigid.linearVelocity = new Vector2 (rigid.linearVelocity.x, Mathf.Lerp(rigid.linearVelocity.y , -maxSpeed, 0.1f));
	}
}
