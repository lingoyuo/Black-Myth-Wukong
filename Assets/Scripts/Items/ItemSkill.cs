using UnityEngine;
using System.Collections;

public class ItemSkill : MonoBehaviour {
    public enum ItemsName
    {
        BUMERANG,
        BOOM,
        ROCK,
        COIN,
        HEALTH,
    }

	AudioSource audioGame;

    public ItemsName itemName = ItemsName.BUMERANG;
    public ParticleSystem parSkil;
    public GameObject spriteSkill;
    UISkill uiSkill;

	ItemManager itemPlayer;

    BoxCollider2D boxSkill;

    Animator anim;

    // Use this for initialization
    void Awake()
    {
		itemPlayer = (ItemManager)GameObject.FindObjectOfType(typeof(ItemManager));
        boxSkill = GetComponent<BoxCollider2D>();

        anim = GetComponent<Animator>();
        anim.speed = 0;

        StartCoroutine(ActiveDeplayTime());
        parSkil.gameObject.SetActive(false);
		audioGame = gameObject.GetComponent<AudioSource> ();

    }

    void Start() {
        var list_UiSkill = FindObjectsOfType<UISkill>();
        foreach(UISkill ui in list_UiSkill)
            if(ui.nameItem == "HEALTH")
                uiSkill = ui;
    }

    IEnumerator ActiveDeplayTime()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
        anim.speed = 1.0f;
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            AudioManager.Instances.PlayAudioEffect(audioGame);

			foreach(ItemPlayer items in itemPlayer.ListItems)
			{
				if(items.Get_Name == itemName.ToString()){
					//items.Set_AmountSkill = items.Get_AmountSkill + 1;
					if(items.Get_AmountSkill < items.Get_LimitNumberItem){
						items.Set_AmountSkill = items.Get_AmountSkill + 1;
                        uiSkill.CheckItem();
                      //  print(items.Get_Name + " "+items.Get_AmountSkill);
					}else{
						items.Set_AmountSkill = items.Get_LimitNumberItem;
					}
				}
			}
			//print(itemName);
            boxSkill.enabled = false;

            parSkil.gameObject.SetActive(true);
            parSkil.Play();
            Invoke("PlayerGetCoin", 0.2f);
        }
    }

    void PlayerGetCoin()
    {
        spriteSkill.SetActive(false);
        Destroy(gameObject, 0.5f);
    }
}
