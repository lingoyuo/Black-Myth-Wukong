using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIShop : MonoBehaviour {

	ItemManager itemsManager;
	ItemPlayer itemPlayer;

	public GameObject shop;

	public int viewIcon;
	public int number;
	public string nameItem;
    public string decriptionItems;

	public GameObject acceptPurchase;
	public GameObject[] limitedItem;
	public GameObject[] saga;
	public Text textCoin,txtBumerang,txtBoom, txtRock,textCoin1;

	[Space(10)]
	[Header("Audio shop")]
	public GameObject backSaga;
	public GameObject shopBt;
	AudioSource audioGame;

	
	void Start(){
		itemsManager = (ItemManager)FindObjectOfType(typeof(ItemManager));
        itemsManager.IntialListItems();
		itemPlayer = FindObjectOfType<ItemPlayer> ();
		number = 0;
		CheckItem ();
		CheckCoin ();
	}

	void Update(){
		textCoin.text = itemsManager.GetGold.ToString ();
		textCoin1.text = itemsManager.GetGold.ToString ();
		if(Input.GetKey (KeyCode.Escape))
		{
			if(shop.activeSelf)
			{
				BackSaga ();
			}
			else
			{
				Home ();
			}
		}
	}

	public void Shop(){

		audioGame = shopBt.GetComponent<AudioSource> ();
		AudioManager.Instances.PlayAudioEffect (audioGame);

		StartCoroutine (ShopShow ());
	}

	public void BackSaga(){
		audioGame = backSaga.GetComponent<AudioSource> ();
		AudioManager.Instances.PlayAudioEffect (audioGame);

		StartCoroutine (HideShop ());

	}

	public void ButtonRock1(){
		viewIcon = 0;
		number = 1;
		nameItem = "ROCK";
		
		acceptPurchase.SetActive (true);
	}
	public void ButtonRock2(){
		viewIcon = 1;
		number = 2;
		nameItem = "ROCK";
		
		acceptPurchase.SetActive (true);
	}
	public void ButtonRock3(){
		viewIcon = 2;
		number = 3;
		nameItem = "ROCK";
		
		acceptPurchase.SetActive (true);
	}
	public void ButtonRock4(){
		viewIcon = 3;
		number = 4;
		nameItem = "ROCK";
		acceptPurchase.SetActive (true);
	}
	public void ButtonRock5(){
		viewIcon = 4;
		number = 5;
		nameItem = "ROCK";
		acceptPurchase.SetActive (true);
	}
	public void ButtonBum1(){
		viewIcon = 5;
		number = 1;
		nameItem = "BUMERANG";
		acceptPurchase.SetActive (true);
	}
	public void ButtonBum2(){
		viewIcon = 6;
		number = 2;
		nameItem = "BUMERANG";
		acceptPurchase.SetActive (true);
	}
	public void ButtonBum3(){
		viewIcon = 7;
		number = 3;
		nameItem = "BUMERANG";
		acceptPurchase.SetActive (true);
	}
	public void ButtonBum4(){
		viewIcon = 8;
		number = 4;
		nameItem = "BUMERANG";
		acceptPurchase.SetActive (true);
	}
	public void ButtonBum5(){
		viewIcon = 9;
		number = 5;
		nameItem = "BUMERANG";
		acceptPurchase.SetActive (true);
	}
	public void ButtonBoom1(){
		viewIcon = 10;
		number = 1;
		nameItem = "BOOM";
		acceptPurchase.SetActive (true);
	}
	public void ButtonBoom2(){
		viewIcon = 11;
		number = 2;
		nameItem = "BOOM";
		
		acceptPurchase.SetActive (true);
	}
	public void ButtonBoom3(){
		viewIcon = 12;
		number = 3;
		nameItem = "BOOM";
		acceptPurchase.SetActive (true);
	}
	public void ButtonBoom4(){
		viewIcon = 13;
		number = 4;
		nameItem = "BOOM";
		acceptPurchase.SetActive (true);
	}
	public void ButtonBoom5(){
		viewIcon = 14;
		number = 5;
		nameItem = "BOOM";
		acceptPurchase.SetActive (true);
	}
	public void ButtonItemShoes(){
		viewIcon = 15;
		number = 1;
        decriptionItems = "You gain double speed in 15s\n";
		nameItem = "SHOE";
		acceptPurchase.SetActive (true);
	}
	public void ButtonItemHealth(){
		viewIcon = 16;
		number = 1;
        decriptionItems = "You will restore all health \n";
		nameItem = "HEALTH";
		
		acceptPurchase.SetActive (true);
	}
	public void ButtonItemArmor(){
		viewIcon = 17;
		number = 1;
        decriptionItems = "You will protected in 10s\n";
		nameItem = "ARMOR";
		acceptPurchase.SetActive (true);
	}
	public void ButtonItemTime(){
		viewIcon = 18;
		number = 1;
        decriptionItems = "Bonus 30s when start level\n";
		nameItem = "BONUS_TIME";
		acceptPurchase.SetActive (true);
	}
	public void ButtonItemGold(){
		viewIcon = 19;
		number = 1;
        decriptionItems = "Gain bonus x1.5 gold when end level\n";
		nameItem = "BONUS_GOLD";
		acceptPurchase.SetActive (true);
	}
		
	public void CheckItem(){
		foreach(ItemPlayer items in itemsManager.ListItems)
		{
			//if(items.Get_AmountSkill == GetComponent< ConstantPriceShop>().
			if(items.Get_Name == "SHOE"){
				if(items.Get_AmountSkill == GetComponent<ConstantPriceShop>().shoe_limit)
					limitedItem[0].SetActive(true);
				else
					limitedItem[0].SetActive(false);
			}
			if(items.Get_Name == "HEALTH"){
				if(items.Get_AmountSkill == GetComponent<ConstantPriceShop>().health_limit)
					limitedItem[1].SetActive(true);
				else
					limitedItem[1].SetActive(false);
			}
			if(items.Get_Name == "ARMOR"){
				if(items.Get_AmountSkill == GetComponent<ConstantPriceShop>().defense_limit)
					limitedItem[2].SetActive(true);
				else
					limitedItem[2].SetActive(false);
			}
			if(items.Get_Name == "BONUS_TIME"){
				if(items.Get_AmountSkill == GetComponent<ConstantPriceShop>().bonus_time_limit)
					limitedItem[3].SetActive(true);
				else
					limitedItem[3].SetActive(false);
			}
			if(items.Get_Name == "BONUS_GOLD"){
				if(items.Get_AmountSkill == GetComponent<ConstantPriceShop>().bonus_gold_limit)
					limitedItem[4].SetActive(true);
				else
					limitedItem[4].SetActive(false);
			}
		}
	}

	public void CheckCoin(){
		foreach(ItemPlayer items in itemsManager.ListItems)
		{
			if(items.Get_Name == "BOOM")
				txtBoom.text = items.Get_AmountSkill.ToString() + "/" + items.Get_LimitNumberItem.ToString();
			if(items.Get_Name == "BUMERANG")
				txtBumerang.text = items.Get_AmountSkill.ToString() + "/" + items.Get_LimitNumberItem.ToString();
			if(items.Get_Name == "ROCK")
				txtRock.text = items.Get_AmountSkill.ToString() + "/" + items.Get_LimitNumberItem.ToString();
		}
	}

	public void Home(){
		Application.LoadLevel ("home");
	}

	IEnumerator HideShop(){
		yield return new WaitForSeconds(0.1f);
		shop.SetActive (false);
		for (int i = 0; i < saga.Length; i++) {
			saga[i].SetActive(true);
		}
	}

	IEnumerator ShopShow(){
		yield return new WaitForSeconds(0.1f);
		shop.SetActive (true);
		CheckItem ();
		for (int i = 0; i < saga.Length; i++) {
			saga[i].SetActive(false);
		}
	}

}
