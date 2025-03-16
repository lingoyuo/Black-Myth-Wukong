using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

// Struct skill in game 
public struct SkillGamePlay
{
    public ItemPlayer item;                                     // this items
    public GameObject UISkill;                                  // UI use for this skill
    public Text text;                                           // UI text

    public ItemPlayer nextItem;                                 // skill next change
    public ItemPlayer firstItem;

    public bool active;

    // constructor
    public SkillGamePlay(ItemPlayer item,GameObject UISkill,ItemPlayer newItem,ItemPlayer firstItem,Text text,bool active)
    {
        this.item = item;
        this.UISkill = UISkill;
        this.nextItem = newItem;
        this.firstItem = firstItem;
        this.text = text;
        this.active = active;
    }
}


public class AttackSkillManager : MonoBehaviour {

    public GameObject UIStick;
    public GameObject UIBumerang;
    public GameObject UIBoom;
    public GameObject UIRock;

    public Text textBumerang;
    public Text textBoom;
    public Text textRock;

	AudioSource audioGame;

    public ItemPlayer Get_currentSkill { get { return currentSkill.item; } }

    private List<SkillGamePlay> listSkills;
    private SkillGamePlay currentSkill;

    private ItemManager itemList;

    public static AttackSkillManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        listSkills = new List<SkillGamePlay>();

        itemList = (ItemManager)GameObject.FindObjectOfType(typeof(ItemManager));

        var bumerangItem = itemList.FindItemsInList(LocalAccessValue.bumerang);
        var rockItem = itemList.FindItemsInList(LocalAccessValue.rock);
        var boomItem = itemList.FindItemsInList(LocalAccessValue.boom);
        var stickItem = itemList.FindItemsInList("STICK");

        SkillGamePlay bumerang = new SkillGamePlay(bumerangItem, UIBumerang, stickItem,boomItem,textBumerang,false);
        SkillGamePlay rock = new SkillGamePlay(rockItem, UIRock, boomItem,stickItem,textRock,false);
        SkillGamePlay boom = new SkillGamePlay(boomItem, UIBoom, bumerangItem,rockItem,textBoom,false);
        SkillGamePlay stick = new SkillGamePlay(stickItem, UIStick, rockItem,bumerangItem,null,true);

        // Intial list
        listSkills.Add(stick);
        listSkills.Add(bumerang);
        listSkills.Add(rock);
        listSkills.Add(boom);

        SetCurrentSkill(stick);      

    }

    void Update()
    {
        // not optimztion, set text skill
        if (currentSkill.item.Get_Name != "STICK")
            currentSkill.text.text = currentSkill.item.Get_AmountSkill.ToString() + "/" + currentSkill.item.Get_LimitNumberItem.ToString();
    }

    // set current skill
    void SetCurrentSkill(SkillGamePlay skill)
    {
        currentSkill = skill;

        if (!currentSkill.UISkill.activeSelf)
        {
            currentSkill.UISkill.SetActive(true);
			audioGame = currentSkill.UISkill.GetComponent<AudioSource>();
            AudioManager.Instances.PlayAudioEffect(audioGame);

            if (currentSkill.item.Get_Name != "STICK")
                if (currentSkill.item.Get_AmountSkill > 0)
                    // Active UI skill
                    ActiveUICurrentSKill();
        }
    }

    void ActiveUICurrentSKill()
    {
        // Only active when UI not active
        if(!currentSkill.active)
        {
            currentSkill.UISkill.GetComponent<Animator>().enabled = true;
            currentSkill.UISkill.transform.GetChild(0).gameObject.SetActive(true);
            currentSkill.UISkill.transform.GetChild(1).gameObject.SetActive(true);
            currentSkill.active = true;
        }
    }

    void DeactiveUICurrentSkill()
    {
        // Only deactive when UI active
        if (currentSkill.active)
        {
            currentSkill.UISkill.GetComponent<Animator>().enabled = false;
            currentSkill.UISkill.transform.GetChild(0).gameObject.SetActive(false);
            currentSkill.UISkill.transform.GetChild(1).gameObject.SetActive(false);
            currentSkill.active = false;
        }
    }

    // Change current skill
    public void ChangeSkill()
    {
        currentSkill.UISkill.SetActive(false);
        
        foreach (SkillGamePlay skill in listSkills)
        {
            if (skill.item.Get_Name == currentSkill.nextItem.Get_Name)
            {
                SetCurrentSkill(skill);
                break;
            }
        }       
    }

    // Decrease number skill
    public void DecreaseNumberCurrentSkill()
    {
        currentSkill.item.Set_AmountSkill = currentSkill.item.Get_AmountSkill - 1;

        //itemList.DecreaseItems(currentSkill.item.Get_Name);

       // print(currentSkill.item.Get_Name + " " + currentSkill.item.Get_AmountSkill);
    }

    // Check skill
    public void CheckSkill()
    {
        if (currentSkill.item.Get_AmountSkill <= 0)
        { 
            // Deactive UI skill        
            DeactiveUICurrentSkill();

            currentSkill.UISkill.SetActive(false);
            // set skill to stick when amount items is zero
            SetCurrentSkill(listSkills[0]);
        }
    }

    // Find skill with item
    private SkillGamePlay FindSkillInList(ItemPlayer item)
    {
        foreach (SkillGamePlay skill in listSkills)
        {
            if (skill.item.Get_Name == item.Get_Name)
                return skill;
        }
        return listSkills[0]; // not expect
    }

    
}
