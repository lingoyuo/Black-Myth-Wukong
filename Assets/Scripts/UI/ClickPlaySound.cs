using UnityEngine;
using System.Collections;

public class ClickPlaySound : MonoBehaviour {

	//AudioManager audioManager;
	AudioSource audioSource;

	void Awake(){
		audioSource = GetComponent<AudioSource> ();
		//audioManager = (AudioManager)FindObjectOfType (typeof(AudioManager));
	}

	public void SoundOnClick(){
		AudioManager.Instances.PlayAudioEffect (audioSource);
	}
}
