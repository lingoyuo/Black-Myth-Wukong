using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public struct SupportItemsGamePlay
{
    public ItemPlayer item;
    public GameObject UIitem;
    public ParticleSystem effect;

    public SupportItemsGamePlay(ItemPlayer item,GameObject UI,ParticleSystem effect)
    {
        this.item = item;
        this.UIitem = UI;
        this.effect = effect;
    }
}

public class SupportSkillManager : MonoBehaviour {

    public GameObject player;
	public GameObject[] countDown;

    [Space(10)]
    [Header("UI Assign")]
    public GameObject UIHealth;
    public GameObject UIShoe;
    public GameObject UIShile;

    [Space(10)]
    [Header("Effect")]
    public ParticleSystem effect_health_active;
    public ParticleSystem effect_shoe_active;
    public ParticleSystem effect_shield_active;

    public List< SupportItemsGamePlay> listItemsSupports;
	
	AudioSource audioSource;
	public AudioClip audioClip,audioHealth;

    public static SupportSkillManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        listItemsSupports = new List<SupportItemsGamePlay>();

        var itemLists = FindObjectOfType<ItemManager>();

        var health = itemLists.FindItemsInList(LocalAccessValue.health_item);
        var shoe = itemLists.FindItemsInList(LocalAccessValue.shoe_item);
        var shield = itemLists.FindItemsInList(LocalAccessValue.defense_item);

        SupportItemsGamePlay health_item = new SupportItemsGamePlay(health, UIHealth,effect_health_active);
        SupportItemsGamePlay shoe_item = new SupportItemsGamePlay(shoe, UIShoe,effect_shoe_active);
        SupportItemsGamePlay shield_item = new SupportItemsGamePlay(shield, UIShile,effect_shield_active);

        listItemsSupports.Add(health_item);
        listItemsSupports.Add(shoe_item);
        listItemsSupports.Add(shield_item);
    }


    public void ActiveItems(int order)
    {
        if (listItemsSupports[order].item.Get_AmountSkill <= 0)
            return;
        listItemsSupports[order].item.Active(player);                                                       // active items
        listItemsSupports[order].item.Set_AmountSkill = listItemsSupports[order].item.Get_AmountSkill - 1;  // decrease amount items   
		listItemsSupports [order].UIitem.GetComponent<Button> ().interactable = false;

		countDown [order].SetActive (true);
		countDown [order].GetComponent<UISkill> ().check = true;
		countDown [order].GetComponent<UISkill> ().numberOfSkill--;
		countDown [order].GetComponent<UISkill> ().isOnSkill = true;

        if (!listItemsSupports[order].effect.gameObject.activeSelf)
        {
            listItemsSupports[order].effect.gameObject.SetActive(true);
            if (!listItemsSupports[order].effect.isPlaying)
                listItemsSupports[order].effect.Play();
        }

		if (order == 0) {
			audioSource = listItemsSupports [order].UIitem.GetComponent<AudioSource> ();
			audioSource.clip = audioHealth;
			AudioManager.Instances.PlayAudioEffect (audioSource);
		} else {
			audioSource = listItemsSupports [order].UIitem.GetComponent<AudioSource> ();
			audioSource.clip = audioClip;
			AudioManager.Instances.PlayAudioEffect (audioSource);
		}

//        // Test
//        if (listItemsSupports[order].item.Get_AmountSkill <= 0)
//            listItemsSupports[order].UIitem.SetActive(false); 
    }

    public void run1()
    {
        print("run1");
    }

    public void run2()
    {
        print("run2");
    }

    public void DeactiveItems(int order)
    {
        listItemsSupports[order].item.Deactive(player);
    }

}
