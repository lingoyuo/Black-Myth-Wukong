using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
    public static int coin;
    public ParticleSystem parGetCoin;
    public GameObject coinSprite;

    BoxCollider2D boxCoin;
    // Use this for initialization
    void Awake () {
        boxCoin = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coin += 1;
            boxCoin.enabled = false;
            parGetCoin.Play();
            Invoke("PlayerGetCoin", 0.2f);
        }
    }

    void PlayerGetCoin()
    {
        coinSprite.SetActive(false);
        Destroy(gameObject, 0.8f);
    }
}
