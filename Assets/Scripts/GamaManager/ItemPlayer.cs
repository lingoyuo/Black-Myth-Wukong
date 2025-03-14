using UnityEngine;
using System.Collections;

public class ItemPlayer : MonoBehaviour {

    // Type Item
    public enum TypeItem
    {
        None,
        Attack,
        Support
    }

    // Set default constructer item
    public ItemPlayer ()
    {
        typeItem = TypeItem.None;
        this.nameItems = "STICK";
    }
  
    // Property items
    public int      Get_CostItem { get { return cost; } }
    public TypeItem Get_TypeItem { get { return typeItem; } }
    public float    Get_CountDown { get { return countdown; } }
    public float    Get_TimeLive { get { return timeLive; } }
    public int      Get_AmountSkill { get { return amount; } }
    public int      Set_AmountSkill { set { amount = value; } }
    public string   Get_Name { get { return nameItems; } }
    public int      Get_LimitNumberItem { get { return limit_amount; } }

    // Information items
    protected int       amount;
    protected float     countdown;
    protected float     timeLive;
    protected int       cost;
    protected TypeItem  typeItem;
    protected string    nameItems;
    protected int       limit_amount;

    // Need overrite function
	public virtual void Active(GameObject targetActive)
    {
        
    }

    public virtual void Deactive(GameObject target)
    {

    }

    /// <summary>
    /// This method will use to save number this items to local value
    /// Save method use only when player buy from shop or end level
    /// </summary>
    public void SaveItemToLocalValue()
    {
        PlayerPrefs.SetInt(nameItems, amount);
    }
}
