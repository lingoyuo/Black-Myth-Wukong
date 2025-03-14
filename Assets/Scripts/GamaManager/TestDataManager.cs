using UnityEngine;
using System.Collections;

public class TestDataManager : MonoBehaviour {

    ItemManager itemsManager;

	void Start()
    {
        itemsManager = (ItemManager)FindObjectOfType(typeof(ItemManager));
	
    }


    public void ShowListItems()
    {
        Debug.Log( "Current items type: " + itemsManager.Get_CurrentItem.Get_Name);

        Debug.Log(" Current number items: " + itemsManager.ListItems.Count);
        
        foreach(ItemPlayer items in itemsManager.ListItems)
        {
            print("Items : " + items.Get_Name+" amount: " + items.Get_AmountSkill+" cost: " + items.Get_CostItem  );
        }

        print("Current gold : " + itemsManager.GetGold);
    }

    public void AddGold()
    {
		ItemManager.AddGold(800);
        print("add 10000 gold !!");
    }

   public void ResetData()
    {
        // Reset gold
        PlayerPrefs.SetInt(LocalAccessValue.gold, 0);
		PlayerPrefs.SetInt(LocalAccessValue.shoe_item, 0);
		PlayerPrefs.SetInt(LocalAccessValue.health_item, 0);
		PlayerPrefs.SetInt(LocalAccessValue.boom, 0);
		PlayerPrefs.SetInt(LocalAccessValue.bumerang, 0);

        print("Reset complate!");
    }

    public void ChangeCurrentItems()
    {
       // itemsManager.Set_CurrentItem(LocalAccessValue.rock);
        //print("Current items change sucess to : " + LocalAccessValue.rock);
    }

    public void AddNumberItemsToCurrentItems()
    {
        itemsManager.Get_CurrentItem.Set_AmountSkill = 500;
        print("add scurress 500 items to " + itemsManager.Get_CurrentItem.Get_Name);
    }

    public void SaveData()
    {
        itemsManager.SaveItemsToLocalData();
    } 

    public void BuyItems()
    {
        int result = itemsManager.BuyItemsFromShop(itemsManager.Get_CurrentItem.Get_Name, 10);
        if (result == 1)
            print("Success buy 10 current items !");
        else if (result == 0)
            print("Limited!");
        else if(result == -1)
            print("Not enough money");
    }
}
