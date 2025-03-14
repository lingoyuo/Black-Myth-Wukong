using UnityEngine;
using System.Collections;

public class Gold : MonoBehaviour {
    [Header("Gold 5,10,15,20")]
    public int coin;
    public ParticleSystem parGetGold;
    public ParticleSystem selfEffect;
    public GameObject gold;

    PoolManager effectScoreGold;
    BoxCollider2D boxGold;

	GameManager gameManager;
	InfomationGame info;

	AudioSource audioGame;

	// Use this for initialization
	void Awake () {
		gameManager = FindObjectOfType<GameManager> ();
		info = FindObjectOfType<InfomationGame> ();
        boxGold = GetComponent<BoxCollider2D>();
        effectScoreGold = GameObject.FindGameObjectWithTag("EffectScoreGold").GetComponent<PoolManager>();
		//print(gameManager.GET_GOLD);
	}

	void Start(){
		audioGame = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            AudioManager.Instances.PlayAudioEffect(audioGame);
            Coin.coin += coin;
            boxGold.enabled = false;
            gameManager.SET_GOLD = gameManager.GET_GOLD + coin;

            if(info)
			    info.CheckItem();

            // Deactive seft effect
            selfEffect.Pause();
            selfEffect.gameObject.SetActive(false);

            // Active effect pick up
            if (!parGetGold.gameObject.activeSelf)
            {
                parGetGold.gameObject.SetActive(true);
                if (!parGetGold.isPlaying)
                    parGetGold.Play();
            }


            Invoke("PlayerGetGold", 0.0f);
            GameObject effectScore = effectScoreGold.GetObjPool(transform.position);

            //if (!effectScore)
            //    effectScore = effectScoreGold.RequestObjPool(transform.position);

            effectScore.GetComponent<ScoreText>().SetScoreText(coin);
        }
    }

    void PlayerGetGold ()
    {
        gold.SetActive(false);
        Invoke("DisActive", 0.5f);
    }

    //public void SetCoin(int coin)
    //{
    //    this.coin = coin;
    //    if (coin <= 10)
    //    {
    //        transform.localScale = new Vector3(0.7f, 0.7f, transform.localScale.z);
    //    }
    //}

    void DisActive()
    {        
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        selfEffect.gameObject.SetActive(true);
        boxGold.enabled = true;
        gold.SetActive(true);
    }
}
