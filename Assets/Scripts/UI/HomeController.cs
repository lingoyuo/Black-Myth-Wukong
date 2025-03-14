using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HomeController : MonoBehaviour {

	public Animator anim;
	public GameObject fadeScene,bee,snow,TabToPlay,title,music;
    public GameObject tutorial;
	public GameObject removeAds;
    bool tabToPlay = false;

    public static int tab = 0;

	[Space(10)]
	[Header("Audio Button")]
	public GameObject btOnSound;
	public GameObject btOffSound;
	public GameObject btOnEffect;
	public GameObject btOffEffect;

    void Start()
    {
       // title.SetActive(false);

        if (tab == 1 ) {
            anim.SetBool("max", true);
            StartCoroutine(TimeBee());
            TabToPlay.SetActive(false);
          	music.SetActive(true);
            title.SetActive(true);
			//removeAds.SetActive (true);
        }
		else
		{
			removeAds.SetActive (false);
		}

        CheckSoundButton();
    }

	void Update(){
		if(Input.GetKeyUp (KeyCode.Escape))
		{
			Application.Quit ();
		}
		if(tabToPlay == false && tab == 0){
			if(Input.GetMouseButtonDown(0)){
				anim.SetBool ("max", true);
				StartCoroutine(TimeBee());
				TabToPlay.SetActive(false);
				//removeAds.SetActive (true);
				music.SetActive(true);
				CheckSoundButton();
                title.SetActive(true);
				tabToPlay = true;
                tab = 1;
			}
		}
	}

	public void Play(){
        music.SetActive(false);
		removeAds.SetActive (false);
        bee.SetActive (false);
		snow.SetActive (false);
       // title.SetActive(false);
		fadeScene.SetActive (true);
	}

    public void Exit() {
        tutorial.SetActive(false);
        bee.SetActive(true);
        snow.SetActive(true);
    }

    public void Tutorial() {
        bee.SetActive(false);
        snow.SetActive(false);
        tutorial.SetActive(true);
    }

    public void ButtonOnSound(){
		//AudioManager.Instances.PlayAudioEffect (onSound.GetComponent<AudioSource> ());
		GameObject.Find ("AudioManager").GetComponent<AudioSource> ().mute = true;
		//AudioManager.Instances.FindAllAudioMute();
		AudioManager.audioGround = false;
		btOnSound.SetActive (false);
		btOffSound.SetActive (true);
	}

	public void ButtonOffSound(){
		AudioManager.Instances.FindAllAudioOn ();
        GameObject.Find("AudioManager").GetComponent<AudioSource>().mute = false;
        AudioManager.audioGround = true;
		btOnSound.SetActive (true);
		btOffSound.SetActive (false);
	}

	public void ButtonOnEffect(){
		AudioManager.Instances.FindAllAudioOn ();
		AudioManager.audioEffect = true;
		btOnEffect.SetActive (true);
		btOffEffect.SetActive (false);

	}

	public void ButtonOffEffect(){
		AudioManager.Instances.FindAllAudioMute ();
		AudioManager.audioEffect = false;
		btOnEffect.SetActive (false);
		btOffEffect.SetActive (true);
	}

	public void CheckSoundButton(){

        if (AudioManager.audioGround) {
			btOnSound.SetActive (true);
			btOffSound.SetActive (false);
		} else {
			btOnSound.SetActive (false);
			btOffSound.SetActive (true);
		}

		if (AudioManager.audioEffect) {
			btOnEffect.SetActive (true);
			btOffEffect.SetActive (false);
		} else {
			btOnEffect.SetActive (false);
			btOffEffect.SetActive (true);
		}
	}

	IEnumerator TimeBee(){
		yield return new WaitForSeconds(0.7f);
		bee.SetActive (true);
	}

	public void Rank()
	{
		//UltimateSevice.Instances.ShowLeadboad ();
		Service.instance.ShowLeadboard ();
	}
		
}
