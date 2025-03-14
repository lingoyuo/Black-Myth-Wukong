using UnityEngine;
using System.Collections;

public class FadeScene : MonoBehaviour {

	public GameObject fade;

	public void FadeScenesOut(){
		Application.LoadLevel ("saga");
	}
}
