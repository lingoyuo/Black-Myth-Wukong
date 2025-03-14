using UnityEngine;
using System.Collections;

public class FadeSceneIn : MonoBehaviour {

	public GameObject fadeScene;

	public void FadeScene(){
		fadeScene.SetActive (false);
	}
}
