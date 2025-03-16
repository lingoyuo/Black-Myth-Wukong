using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfomationGame : MonoBehaviour
{

    public GameObject pause, item1, item2;
    public Animator anim;

    GameManager items;
    ItemManager itemsManager;

    public TextInGame txtScore, txtGold;
    float t;

    public GameObject bonusTime, bonusCoin, mobileController;
    public GameObject bnTime, bnGoldClear, bnGoldOver;

    public GameObject offSound, onSound, offEffect, onEffect;

    AudioSource audioSource;
    //AudioManager audioManager;

    public static InfomationGame Instances { get; private set; }

    void Awake()
    {
        Instances = this;
    }

    void Start()
    {
        items = FindObjectOfType<GameManager>();
        itemsManager = (ItemManager)FindObjectOfType(typeof(ItemManager));
        items.IntialScore();
        InitalValue();
        foreach (ItemPlayer item in itemsManager.ListItems)
        {
            if (item.Get_Name == "BONUS_GOLD")
            {
                if (item.Get_AmountSkill > 0)
                {
                    bonusCoin.SetActive(true);
                    item.Set_AmountSkill = 0;
                    bnGoldClear.SetActive(true);
                    bnGoldOver.SetActive(true);
                }
                else
                    bonusCoin.SetActive(false);
            }
            if (item.Get_Name == "BONUS_TIME")
            {
                if (item.Get_AmountSkill > 0)
                {
                    bonusTime.SetActive(true);
                    bnTime.SetActive(true);
                    item.Set_AmountSkill = 0;
                }
                else
                    bonusTime.SetActive(false);
            }
        }
    }

    void InitalValue()
    {
        txtScore.IntialValue(items.GET_SCORE);
        txtGold.IntialValue(items.GET_GOLD);
    }

    public void CheckItem()
    {
        //txtScore.text = items.GET_SCORE.ToString ();
        //txtGold.text = items.GET_GOLD.ToString ();
        txtScore.value = items.GET_SCORE;
        txtGold.value = items.GET_GOLD;
    }

    public void Pause()
    {

        audioSource = GameObject.Find("Pause").GetComponent<AudioSource>();
        AudioManager.Instances.PlayAudioEffect(audioSource);

        if (mobileController.activeSelf)
            mobileController.SetActive(false);

        if (item1 != null && item2 != null)
        {
            item1.SetActive(false);
            item2.SetActive(false);
        }
        pause.SetActive(true);
        anim.SetBool("max", true);

        CheckSound();

    }

    public void Replay()
    {

        audioSource = GameObject.Find("Playbt").GetComponent<AudioSource>();
        AudioManager.Instances.PlayAudioEffect(audioSource);

        Time.timeScale = 1;
        anim.SetBool("max", false);
        StartCoroutine(TimeLife());
    }

    public void Reload()
    {

        audioSource = GameObject.Find("btReload").GetComponent<AudioSource>();
        AudioManager.Instances.PlayAudioEffect(audioSource);

        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1;
        items.UpDateData();
    }

    public void Menu()
    {

        audioSource = GameObject.Find("Menu").GetComponent<AudioSource>();
        AudioManager.Instances.PlayAudioEffect(audioSource);

        Time.timeScale = 1;
        Application.LoadLevel("saga");
        items.UpDateData();
    }

    IEnumerator TimeLife()
    {
        yield return new WaitForSeconds(0.2f);
        pause.SetActive(false);
        mobileController.SetActive(true);
        item1.SetActive(true);
        item2.SetActive(true);
    }

    // Off background
    public void ClickOnSound()
    {
        AudioManager.Instances.PlayAudioEffect(onSound.GetComponent<AudioSource>());
        GameObject.Find("AudioManager").GetComponent<AudioSource>().mute = false;
        AudioManager.audioGround = true;
        onSound.SetActive(true);
        offSound.SetActive(false);
    }

    // On background
    public void ClickOffSound()
    {
        AudioManager.Instances.PlayAudioEffect(offSound.GetComponent<AudioSource>());
        GameObject.Find("AudioManager").GetComponent<AudioSource>().mute = true;
        AudioManager.audioGround = false;
        onSound.SetActive(false);
        offSound.SetActive(true);
    }

    // On effect
    public void ClickOnEffect()
    {
        AudioManager.Instances.PlayAudioEffect(onEffect.GetComponent<AudioSource>());
        AudioManager.Instances.FindAllAudioOn();
        AudioManager.audioEffect = true;
        onEffect.SetActive(true);
        offEffect.SetActive(false);
    }

    // Off effect
    public void ClickOffEffect()
    {
        AudioManager.Instances.PlayAudioEffect(offEffect.GetComponent<AudioSource>());
        AudioManager.Instances.FindAllAudioMute();
        AudioManager.audioEffect = false;
        onEffect.SetActive(false);
        offEffect.SetActive(true);
    }

    public void CheckSound()
    {
        if (AudioManager.audioEffect)
        {
            onEffect.SetActive(true);
            offEffect.SetActive(false);
        }
        else
        {
            onEffect.SetActive(false);
            offEffect.SetActive(true);
        }

        if (AudioManager.audioGround)
        {
            onSound.SetActive(true);
            offSound.SetActive(false);
        }
        else
        {
            onSound.SetActive(false);
            offSound.SetActive(true);
        }
    }

}
