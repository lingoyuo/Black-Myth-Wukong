using UnityEngine;
using System.Collections;

public class AnimButton : MonoBehaviour {

	public Animator anim;

	void Start(){
		anim = GetComponent<Animator> ();
	}

	public void Show(){
		GetComponent<Animator> ().SetTrigger ("Highlighted");
	}
}
