using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class UIEndGame : MonoBehaviour {

	//ItemManager itemsManager;

    public TextInGame txtScore,textTime, txtGold;

    public float time_wait = 2.0f;

    public int number_score_bonus_each_time = 100;

    public GameObject bonus;

    [Space(10)]
    [Header("Audio coin run:")]
    public AudioClip audio_bonus_coin;
    public AudioClip audio_coin_count;
    public AudioSource audio_source;

	AudioSource audioOver,audioRePlay,audioNext;
	//AudioManager audioManager;

    [Space(10)]
    [Header("Botton :")]
    public GameObject botton01;
    public GameObject botton02;

    public GameObject[] starGame;

    TimeGame timegame;
	int timeInGame,timeStar;
	GameManager gameManager;
	
	ItemManager itemsManager;

	[Space(10)]
	[Header("Gift Coin: ")]
	public GameObject GiftBox;
    public float percentage;

    private bool caculate;                          // check caculate state
    private bool caculate_gold;

	void Awake(){
		GetComponent<Animator> ().Play ("LevelClear");
		itemsManager = (ItemManager)FindObjectOfType (typeof(ItemManager));
		timegame = FindObjectOfType<TimeGame> ();
		gameManager = FindObjectOfType<GameManager> ();
		timeInGame = (Mathf.FloorToInt (timegame.Get_Time));

		textTime.IntialValue (timeInGame);
		timeStar = Mathf.FloorToInt (-timegame.currentTime2);
		txtScore.IntialValue (gameManager.GET_SCORE);
		txtGold.IntialValue (gameManager.GET_GOLD);

		timegame.checkCount = false;
		timegame.checkTimeBonus = false;

		if (timeStar <= gameManager.time_3_star) {
			starGame [2].SetActive (true);
			gameManager.newStar = 3;
		} else if (timeStar <= gameManager.time_2_star){
			starGame [1].SetActive (true);
		gameManager.newStar = 2;
		}else {
			starGame[0].SetActive(true);
			gameManager.newStar = 1;
		}

        StartCoroutine(BonusScore());
	}

	void Start(){
		audioOver = gameObject.GetComponent<AudioSource> ();
		
		AudioManager.Instances.PlayAudioEffect (audioOver);
	}

    void Update()
    {
        // player not wait when game caculate
        if(Input.GetMouseButton(0) && caculate)
        {
            txtScore.speed = 100000000.0f;
            textTime.speed = 10000000.0f;
            caculate = false;

            gameManager.UpDateData();                                       // update data

            audio_source.Stop();

            StartCoroutine(ActiveButton());
        }

        // check when caculate, button appear then it finish
        if(caculate)
        {
            if(!audio_source.isPlaying)
            {
                audio_source.clip = audio_coin_count;
                AudioManager.Instances.PlayAudioEffect(audio_source);
            }

            if (txtScore.GET_currentValue == txtScore.value)
            {
                caculate = false;
                StartCoroutine(ActiveButton());
                audio_source.Stop();
				AudioManager.Instances.StopAudioEffect (audio_source);

                gameManager.UpDateData();                                   // update data
            }
        }

        if(caculate_gold)
        {
            if(txtGold.value == txtGold.GET_currentValue)
            {
                caculate_gold = false;
                audio_source.Stop();             
            }
        }
    }
    // bonus score with time remain
    IEnumerator BonusScore()
    {
        yield return new WaitForSeconds(time_wait);

        // Start coin run audio
        audio_source.clip = audio_bonus_coin;
		AudioManager.Instances.PlayAudioEffect (audio_source);
        
        txtScore.value = txtScore.value + number_score_bonus_each_time * textTime.value;
        gameManager.SET_SCORE = txtScore.value;

        if (bonus.activeSelf)
        {
            txtGold.value = (int)(txtGold.value * 1.5f);
            gameManager.SET_GOLD = txtGold.value;
        }

       // print(gameManager.GET_SCORE);
        textTime.value = 0;

        caculate = true;
    }

    // active order button
    IEnumerator ActiveButton()
    {
        yield return new WaitForSeconds(.4f);
        botton01.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        // Loading done, show
            RandomGift();

        yield return new WaitForSeconds(0.2f);
        botton02.SetActive(true);
    }


	public void Replay(){
		gameManager.UpDateData();
		Application.LoadLevel (Application.loadedLevel);
		audioRePlay = botton01.GetComponent<AudioSource> ();
		AudioManager.Instances.PlayAudioEffect (audioRePlay);
		Time.timeScale = 1;
	}

	public void NextLevelSaga(){
		gameManager.SetStar();
		gameManager.CheckLevelUnlock();
        audioNext = botton01.GetComponent<AudioSource> ();
		AudioManager.Instances.PlayAudioEffect (audioNext);
		Application.LoadLevel ("saga");
	}

	public void RandomGift(){
		int random = UnityEngine.Random.Range(0, 100);
		//print (random);
		if (random <=percentage)
			GiftBox.SetActive (true);
		else
			GiftBox.SetActive (false);
	}

    public void BonusCoinShowVideo()
    {
        caculate_gold = true;
        GiftBox.SetActive(false);
        audio_source.clip = audio_coin_count;
        AudioManager.Instances.PlayAudioEffect(audio_source);
        txtGold.value = txtGold.value + 100;  
    }
}
