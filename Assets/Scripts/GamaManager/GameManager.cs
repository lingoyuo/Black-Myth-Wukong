using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public enum StateGame
    {
        Starting,
        Playing,
        Losing,
        Ending
    }

    public GameObject player;
    public UIStartGame _UIStartGame;
    public UIGameOver _UIGameOver;
    public UIEndGame _UIEndGame;
    public TextInGame textGold;

    [Space(20)]
    [Header("Time in game")]
    public float time_live = 120;                                     // Game over
	public float time_3_star = 80;                                    // Time set 3 star
	public float time_2_star = 100;                                   // Time set 2 star
   

    [Space(20)]
    [Header("Normal map setting :")]

    public StateGame firstState = StateGame.Starting;
    [SerializeField] public int CurrentLevel;

    [HideInInspector]
    public StateGame state;

    [Space(20)]
    [Header("Boss map setting :")]
    public bool isBossMap = false;
    public string NameBoss = "Please stick boss_map and write exactly name scence!";

    public int SET_GOLD { set { gold_current_level = value; } }
    public int GET_GOLD { get { return gold_current_level; } }

    public int SET_SCORE { set { score = value; } }
    public int GET_SCORE{ get { return score; } }

    private ItemManager itemManager;
    private InforStrength playerHealth;

    // infor level
    private int score;
    private int gold_current_level;

    [HideInInspector]
    public int newStar;

    // Access local value
    private LocalAccessValue accessValue;

    void Awake()
    {
        // Load video reward
//		if (HeyzapControl.Instances != null && !HeyzapControl.Instances.isAvailable)
//			HeyzapControl.Instances.LoadVideos ();

        //// Load full banner
//		if (UltimateAds.Instances != null)
//        {
//			UltimateAds.Instances.LoadFull ();
//        }
    }
    void Start()
    {     
        accessValue = GetComponent<LocalAccessValue>();
        itemManager = GameObject.FindObjectOfType<ItemManager>();

        gold_current_level = 0;

        // Check
        if (!player)
            Debug.LogError("Please add player to game manager!!");
        else if (!_UIStartGame)
            Debug.LogError("Please add UI start game to manager !!");

        state = firstState;
        playerHealth = player.GetComponent<InforStrength>();
        playerHealth.InitialHealth();

        // Test set start = 3
       // newStar = 3;
    }
    
    // Call outside class to intial value gold and score
	public void IntialScore(){
		var local_access = GameObject.FindObjectOfType<LocalAccessValue>();

        // Inital score
		if(local_access.GetValue(LocalAccessValue.total_score) == -1)
		{
			score = 0;
			LocalAccessValue.SetValue(LocalAccessValue.total_score, 0);
		}
		else
			score = local_access.GetValue(LocalAccessValue.total_score);
    }

    void Update()
    {
        switch (state)
        {
            case StateGame.Starting:
                StartGame();
                break;
            case StateGame.Playing:
                Playing();
                break;
            case StateGame.Losing:
                Losing();
                break;
            case StateGame.Ending:
                Ending();
                break;
        }
    
    }

    /// <summary>
    /// Start game , we need have loading screen
    /// </summary>
    void StartGame()
    {
        // active screen fade UI
        if (!_UIStartGame.gameObject.activeSelf)
        {
            _UIStartGame.gameObject.SetActive(true);
        }

        // deactive when screen fader finish
        if (_UIStartGame.DoorManager.activeSelf)
        {
            _UIStartGame.gameObject.SetActive(false);
            // change state to playing when fader complete
            state = StateGame.Playing;
        }
    }


    // State playing: In current state, we need check when player dead to do something suitable
    void Playing()
    {
        if (playerHealth.Get_Health <= 0)
        {
            // do somthing when player dead:
            if (!isBossMap)
            {
                //StartCoroutine(WaitForAppearUIGameOVer(4.0f));
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControllerLimited>().enabled = false;         // camera not follow when player dead
                state = StateGame.Losing;                                                                                       // change state to losing
            }
            else
                RestartCurrentBossMap();                                                                                        // restart current boss map

        }
    }

    // Function wait time to appear game over UI
    IEnumerator WaitForAppearUIGameOVer(float timeWait)
    {
        yield return new WaitForSeconds(timeWait);
        if (!_UIGameOver.gameObject.activeSelf)
            _UIGameOver.gameObject.SetActive(true);
    }

    // Sate ending: In current state, we need set star and check level unlock and active UI end game
    void Ending()
    {
        SetStar();
        CheckLevelUnlock();
        if (!_UIEndGame.gameObject.activeSelf)
        {
			if(GoogleMobileAdsDemoScript.instance != null)
			{
				GoogleMobileAdsDemoScript.instance.ShowInterstitial();
			}

            _UIEndGame.gameObject.SetActive(true);
        }
    }

    // State losing: In current state, we need active UI game over
    void Losing()
    {

    }

    // Set new level unlock
    public void CheckLevelUnlock()
    {
        int totalLevelUnlock = accessValue.GetTotalLevelUnlock();
        if (totalLevelUnlock == CurrentLevel)
            accessValue.SetTotalLeLevelUnlock(totalLevelUnlock + 1);
    }

    // Set star level
    public void SetStar()
    {
        int currentStar = accessValue.GetStar(CurrentLevel);

        if (currentStar < newStar)
            accessValue.SetStarLevel(CurrentLevel, newStar);
    }


    // Restart current level
    void RestartCurrentLevel()
    {
        if (CurrentLevel < 10)
            Application.LoadLevel("Level0" + CurrentLevel);
        else
            Application.LoadLevel("Level" + CurrentLevel);
    }

    // Restart current boss map
    void RestartCurrentBossMap()
    {
        Application.LoadLevel(NameBoss);
    }

    public void SetPlayerDead()
    {
        player.GetComponent<InforStrength>().LoseHealth(player.GetComponent<InforStrength>().Get_Health);
    }

    // Update value to database
    public void UpDateData()
    {
        // save gold
		ItemManager.AddGold(gold_current_level );

        // save items
        itemManager.SaveItemsToLocalData();

        // save score
		LocalAccessValue.SetValue(LocalAccessValue.total_score, score);
		//UltimateSevice.Instances.SumitService (score);
		//Service.instance.ReportScore (score, "Test");
    }

    public void ShowUIEndGame()
    {
		if (!_UIGameOver.gameObject.activeSelf) {
			if(GoogleMobileAdsDemoScript.instance != null)
			{
				GoogleMobileAdsDemoScript.instance.ShowInterstitial();
			}
            
            _UIGameOver.gameObject.SetActive (true);
		}
    }

    // Show admobs
    public void ShowAdmob()
    {
        _UIEndGame.GetComponent<UIEndGame>().BonusCoinShowVideo();

    }
}
