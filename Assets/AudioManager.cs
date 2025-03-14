using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public static bool audioGround = true;
	public static bool audioEffect = true;

    //-----------------------------------
    // Property
    public static  AudioManager Instances
    {
        get { return instances; }
    }

    //------------------------------------
    // Instance class
    private static AudioManager instances;

    /// <summary>
    ///  Intial instance and check duplicate
    /// </summary>

    //------------------------------------
    // List audio background
    public List<AudioClip> list_audio;

    private AudioSource audio_source;

    private void InitialSigleton ()
    {
     
        if (instances)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instances = this;
            DontDestroyOnLoad(gameObject);

            // Play in first time home
            audio_source.clip = list_audio[Random.Range(0, list_audio.Count)];
            audio_source.Play(); 
        }
        
    }
	void Awake(){

        audio_source = GetComponent<AudioSource>();
        InitialSigleton();

        if (!audioGround)
            gameObject.GetComponent<AudioSource>().mute = true;
        if (!audioEffect)
            FindAllAudioMute();

    }

    void Update()
    {
        if(!audio_source.isPlaying)
        {
            audio_source.clip = list_audio[Random.Range(0, list_audio.Count)];
            audio_source.Play();
        }

		//print (audioEffect);
    }

    public void PlayAudioGround(AudioSource audioGame){
		if (audioGround)
			audioGame.Play ();
	}

	public void PlayAudioEffect(AudioSource audioGame){
		if (audioEffect)
			audioGame.Play ();
	}

	public void StopAudioEffect(AudioSource audioGame){
		if (audioEffect == false) {
			audioGame.Stop ();
		}
	}

    public void FindAllAudioMute()
    {
        foreach (var _audio in FindObjectsOfType<AudioSource>())
        {
            if (_audio.gameObject.GetInstanceID() != gameObject.GetInstanceID())
                _audio.mute = true;
        }
    }

    public void FindAllAudioOn()
    {
        foreach (var _audio in FindObjectsOfType<AudioSource>())
        {
            if (_audio.gameObject.GetInstanceID() != gameObject.GetInstanceID())
                _audio.mute = false;
        }
    }
}
