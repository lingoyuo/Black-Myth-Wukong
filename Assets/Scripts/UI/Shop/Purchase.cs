using UnityEngine;
using System.Collections;

public class Purchase : MonoBehaviour {

	ItemManager itemsManager;
	UIShop uiShop;
	public GameObject limited,notEnough;
	public GameObject buttonMoreCoin;

	[Space(10)]
	[Header("Audio Button")]
	public GameObject btPurchase;
	public GameObject btExit;
	AudioSource audioGame;

	void Start(){
		itemsManager = (ItemManager)FindObjectOfType(typeof(ItemManager));
		uiShop = FindObjectOfType<UIShop> ();
	}

	public void BuyItem(){
		audioGame = btPurchase.GetComponent<AudioSource> ();
		AudioManager.Instances.PlayAudioEffect (audioGame);
		StartCoroutine (BuyItemShop ());
	}

	public void ExitPurchase(){
		audioGame = btExit.GetComponent<AudioSource> ();
		AudioManager.Instances.PlayAudioEffect (audioGame);

		StartCoroutine (SoundExit());
	}

	IEnumerator BuyItemShop(){
		yield return new WaitForSeconds (0.1f);
		int resuil = itemsManager.BuyItemsFromShop (uiShop.nameItem, uiShop.number);
		if(resuil == 1){
			gameObject.SetActive(false);
			uiShop.CheckCoin();
			uiShop.CheckItem();
		}else if(resuil == 0){
			gameObject.SetActive(false);
			limited.SetActive(true);
		}else if(resuil == -1){
			gameObject.SetActive(false);
			notEnough.SetActive(true);
		}
	}

	IEnumerator SoundExit(){
		yield return new WaitForSeconds(0.1f);
		gameObject.SetActive (false);
	}

}
