using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestUIGamePlay : MonoBehaviour {

	UISkill test;
	ItemManager itemsManager;
	ItemPlayer itemPlayer;

	public GameObject countdown1,countdown2,countdown3;
	public Button bt1,bt2,bt3;

	void Awake(){
		itemsManager = (ItemManager)FindObjectOfType(typeof(ItemManager));
		itemPlayer = FindObjectOfType<ItemPlayer> ();
	}
	
	public void ButtonSkill(int i){
		print (i);
	}

	public void ButtonItem1(){
		bt1.interactable = false;
		countdown1.GetComponent<UISkill> ().check = true;
		countdown1.GetComponent<UISkill> ().numberOfSkill--;
		countdown1.SetActive (true);
	}

	public void ButtonItem2(){
		bt2.interactable = false;
		countdown2.GetComponent<UISkill> ().check = true;
		countdown2.GetComponent<UISkill> ().numberOfSkill--;
		countdown2.SetActive (true);
	}

	public void ButtonItem3(){
		bt3.interactable = false;
		countdown3.GetComponent<UISkill> ().check = true;
		countdown3.GetComponent<UISkill> ().numberOfSkill--;
		countdown3.SetActive (true);
	}
	
}
