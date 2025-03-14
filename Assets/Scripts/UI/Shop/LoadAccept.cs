using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadAccept : MonoBehaviour {

	private UIShop uiShop;
	public GameObject[] itemInfo;
    public Text textDecriptions;

    private string text_display;
    

	void Awake(){
		uiShop = FindObjectOfType<UIShop> ();
		for (int i = 0; i < itemInfo.Length; i++) {
			itemInfo[i].SetActive(false);
		}
	}

	void Update(){
	
		itemInfo [uiShop.viewIcon].SetActive (true);
      
		for (int i = 0; i < itemInfo.Length; i++) {
			if(itemInfo[uiShop.viewIcon] != itemInfo[i]){
				itemInfo[i].SetActive(false);
			}
		}
	}
    void OnEnable()
    {
        textDecriptions.text = uiShop.decriptionItems + "\nDo you want buy \n this items ?";
    }

    void OnDisable()
    {
        uiShop.decriptionItems = null;
    }


}
