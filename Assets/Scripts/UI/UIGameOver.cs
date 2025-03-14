using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour {

	public Text txtScore, txtGold;
	TimeGame timegame;
	GameManager gameManager;
	public GameObject[] enableGameObj;
	public GameObject bonusCoin;
	ItemManager itemsManager;

	AudioSource audioOver,audioReplay,audioMap;
	//AudioManager audioManager;

	public GameObject btRep, btMap;

	void Awake(){
		timegame = FindObjectOfType<TimeGame> ();
		gameManager = FindObjectOfType<GameManager> ();
		itemsManager = (ItemManager)FindObjectOfType(typeof(ItemManager));
		GetComponent<Animator> ().Play("GameOver");
		txtScore.text = gameManager.GET_SCORE.ToString ();
		txtGold.text = gameManager.GET_GOLD.ToString ();
		timegame.checkCount = false;
		timegame.checkTimeBonus = false;

		for (int i = 0; i < enableGameObj.Length; i++) {
			enableGameObj[i].SetActive(false);
		}
	}

	void Start(){
		audioOver = gameObject.GetComponent<AudioSource> ();

		AudioManager.Instances.PlayAudioEffect (audioOver);
	}

	public void Replay(){
		//gameManager.IntialScore ();
		audioReplay = btRep.GetComponent<AudioSource> ();
		AudioManager.Instances.PlayAudioEffect (audioReplay);
		gameManager.UpDateData ();
		Application.LoadLevel (Application.loadedLevel);
		Time.timeScale = 1;
	}

	public void Map(){
		audioMap = btMap.GetComponent<AudioSource> ();
		AudioManager.Instances.PlayAudioEffect (audioMap);
		gameManager.IntialScore ();
		gameManager.UpDateData ();
		Application.LoadLevel ("saga");
		Time.timeScale = 1;
	}

    public void FullBannerShow()
    {
//		if(GoogleMobileAdsDemoScript.instance != null)
//		{
//			GoogleMobileAdsDemoScript.instance.ShowInterstitial();
//		}
    }
}
