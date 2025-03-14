using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This cript intial in game start, it will not destroy and exits all the time in gameplay
// Script have data: gold, all items list, current items will use in gameplay
// Shop will access direct to method and modify number items and gold that player have
public class ItemManager : MonoBehaviour {
    
    public ItemPlayer Get_CurrentItem { get { return currentItem; } }
    
    public int GetGold { get { return gold; } }

    public List<ItemPlayer> ListItems;

    private ItemPlayer currentItem;

    private LocalAccessValue localAccessValue;
    private ConstantPriceShop priceItems;

	public static int gold;
	void Awake()
    {
       // DontDestroyOnLoad(this);
        localAccessValue =(LocalAccessValue) FindObjectOfType(typeof(LocalAccessValue));
        priceItems = (ConstantPriceShop)FindObjectOfType(typeof(ConstantPriceShop));
        IntialListItems();      
    }


    void Update()   
    {
        // print(ListItems[2].Get_Name + " " + ListItems[2].Get_AmountSkill);

       // print(ListItems[4].Get_AmountSkill);
    }
    // Intial list items to manager
    public void IntialListItems()
    {
        ListItems = new List<ItemPlayer>();

        // Inital first items use in gameplay
        currentItem = new ItemPlayer();

        // First, we need get gold player in game
        if (localAccessValue.GetValue(LocalAccessValue.gold) == -1)
        {
           // gold = 9000;
			LocalAccessValue.SetValue(LocalAccessValue.gold, 0);
        }
        else
        {
            gold = localAccessValue.GetValue(LocalAccessValue.gold);
           // gold = 9000;
        }

        // we need get total score

        // Call item when start game

        // If current don't have skill , we need create value item to element before give it to list

        // Intial stick
        ItemPlayer stickItems = new ItemPlayer();

        // Intial bom item
        BoomSkill boomItem = new BoomSkill(priceItems.boom_countdown,priceItems.boom_cost,priceItems.boom_limit);  

        // Intial bumerang skill
        BumerangSkill bumerangItem = new BumerangSkill(LocalAccessValue.bumerang,0,priceItems.bumerang_cost,priceItems.bumerang_limit);

        // Intial rock skill
        RockSkill rockItem = new RockSkill(LocalAccessValue.rock, 0, priceItems.rock_countdown, priceItems.rock_cost, priceItems.rock_limit);

        // Intial defense items
        DefenseItems defenseItem = new DefenseItems(priceItems.defense_cost, priceItems.defense_time_live, priceItems.defense_limit);

        // Intial health items
        HealthItems healthItems = new HealthItems(priceItems.health_cost, priceItems.health_time_live, priceItems.health_limit);

        // Intial shoe speed items
        ShoeSpeedItems shoeItems = new ShoeSpeedItems(priceItems.shoe_cost, priceItems.shoe_time_live, priceItems.shoe_limit);

        // Inital bonus gold items
        BonusGoldItems bonusGoldItems = new BonusGoldItems(priceItems.bonus_gold_cost, priceItems.bonus_gold_time_live, priceItems.bonus_gold_limit);

        //Inital bonus time items
        BonusTimeItems bonusTimeItems = new BonusTimeItems(priceItems.bonus_time_cost, priceItems.bonus_time_items_time_live, priceItems.bonus_time_limit);

        // Add skill to list manager skill
        ListItems.Add(stickItems);
        ListItems.Add(boomItem);
        ListItems.Add(bumerangItem);
        ListItems.Add(rockItem);
        ListItems.Add(healthItems);
        ListItems.Add(defenseItem);
        ListItems.Add(shoeItems);
        ListItems.Add(bonusGoldItems);
        ListItems.Add(bonusTimeItems);

        // Intial value form itial
        foreach(ItemPlayer item in ListItems)
        {
            if(localAccessValue.GetValue(item.Get_Name) == -1)
            {
				LocalAccessValue.SetValue(item.Get_Name, 0);
                item.Set_AmountSkill = 0;
            }
            else
                item.Set_AmountSkill = localAccessValue.GetValue(item.Get_Name);
        }
    }

    /// <summary>
    /// Active
    /// </summary>
    public void ActiveAttackSkill(GameObject player)
    {
        currentItem.Active(player);
    }

    /// <summary>
    /// Decrease number current items
    /// </summary>
    public void DecreaseNumberCurrentSkill(int number)
    {
        currentItem.Set_AmountSkill = currentItem.Get_AmountSkill - number;
    }

    /// <summary>
    /// Use in Shop
    /// This method to buy from shop. 
    ///     Return -1: Not enough gold
    ///     Return 0 : Number items reach to limited
    ///     Return 1 : Buy Success then update value to local value
    /// </summary>
    public int BuyItemsFromShop(string name_items,int number)
    {
        foreach(ItemPlayer item in ListItems)
        {
            if(item.Get_Name == name_items)
            {
                if (gold - item.Get_CostItem * number >= 0)
                {
                    // Check limit items
                    if (item.Get_AmountSkill + number > item.Get_LimitNumberItem)
                        return 0;

                    // Update gold value
                    gold = gold - item.Get_CostItem * number;
					LocalAccessValue.SetValue(LocalAccessValue.gold, gold);

                    // Update number skill to current items
                    item.Set_AmountSkill = item.Get_AmountSkill + number;
                    item.SaveItemToLocalValue();

                    return 1;
                }
                else
                    return -1;
            }
        }
        return -2; // not expect this kind value, only return when not search items from list
    }

    /// <summary>
    /// Use this menthod when finish level, bonus gold.
    ///     When save gold, we need save data to local value
    /// </summary>
	public static void AddGold(int number)
    {
        gold = number + gold;

        // Save value
		LocalAccessValue.SetValue(LocalAccessValue.gold, gold);
    }

	public void AddGolds(int number)
	{
		AddGold (number);
	}

    // This method to find items by name in list
    public ItemPlayer FindItemsInList(string name)
    {
        //IntialListItems();

        foreach (ItemPlayer item in ListItems)
        {
            //print(item);
            if (item.Get_Name == name)
                return item;
        }

        Debug.LogError("Can not find " + name + " item in list!");

        return null;
    }

    /// <summary>
    /// This method use when finish level, we need update number skill to local value
    /// </summary>
    public void SaveItemsToLocalData()
    {
        foreach (ItemPlayer item in ListItems)
        {
			LocalAccessValue.SetValue(item.Get_Name, item.Get_AmountSkill);
        }
    }

    public void DecreaseItems(string name)
    {
        FindItemsInList(name).Set_AmountSkill = FindItemsInList(name).Get_AmountSkill - 1;
    }

	public void CheckPurchase(string id)
	{
		
	}
}
