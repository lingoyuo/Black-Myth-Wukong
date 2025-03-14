using UnityEngine;
using System.Collections;

public class PanelLimited : MonoBehaviour {

	public GameObject btLimit;
	AudioSource audio;

	public void Back(){
		audio = btLimit.GetComponent<AudioSource> ();
		AudioManager.Instances.PlayAudioEffect (audio);
		
		StartCoroutine (SoundLimitOn ());
	}

	IEnumerator SoundLimitOn(){
		yield return new WaitForSeconds(0.1f);
		gameObject.SetActive (false);
	}
}
