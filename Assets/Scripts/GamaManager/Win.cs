using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {
	public string level = "level1";
	// Use this for initialization


	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
			WinGame ();
	}

	void WinGame ()
	{
		Application.LoadLevel (level);
	}

}
