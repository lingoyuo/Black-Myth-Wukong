using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISkill : MonoBehaviour {

    public ParticleSystem effect;
    public ParticleSystem effect_UI;

	float currenLength,currentNumber,t;
	public float speed,timeOfSkill;

	public Image image;
	public Text text,textSkill;
	public Button bt;

	public bool check,isOnSkill;

	public int numberOfSkill,numberOfButton,limitItem;
	ItemManager itemsManager;
	SupportSkillManager support;

	public string nameItem;

	void Start(){
		isOnSkill = false;
		support = FindObjectOfType<SupportSkillManager> ();
		itemsManager = (ItemManager)FindObjectOfType(typeof(ItemManager));
		check = true;

        // print(itemsManager.ListItems.Count);
        //itemsManager.IntialListItems();

		CheckItem ();
		speed = 1 / timeOfSkill;
		currenLength = 0;
		currentNumber = 1 / speed;

       
    }

	void Update(){
		if (check) {
			if(currenLength <= 1){
				currenLength += speed * Time.deltaTime;
				image.fillAmount = currenLength;
				t+= Time.deltaTime;
				text.text = (Mathf.FloorToInt(currentNumber + 1 - t)).ToString();
				textSkill.text = numberOfSkill.ToString() +"/"+limitItem.ToString();
             //   print(numberOfSkill.ToString() + "/" + limitItem.ToString());

				if (image.fillAmount >=1){
					check = false;
					t= 0;
					currenLength =0;
					image.fillAmount = 0;
					CheckButton();

                    DeactiveEffect();

					gameObject.SetActive(false);

					if(isOnSkill == true){
						support.DeactiveItems(numberOfButton);
						isOnSkill=false;
                    }
				}
			}
		}
	}

    // Deactive effect
    void DeactiveEffect()
    {
        if (effect)
        {
            effect.Stop();
            effect.gameObject.SetActive(false);
        }
    }

	public void CheckButton(){
		if (numberOfSkill > 0 && !check) {
			bt.interactable = true;

            // active ui effect
            if (!effect_UI.gameObject.activeSelf)
            {
                effect_UI.gameObject.SetActive(true);
                effect_UI.Stop();
                effect_UI.Play();
            }
        } else {
			bt.interactable = false;
		}
	}

	public void CheckItem(){
        foreach (ItemPlayer items in itemsManager.ListItems)
		{
			if(items.Get_Name == nameItem){
				numberOfSkill = items.Get_AmountSkill;
				timeOfSkill = items.Get_TimeLive;
				limitItem = items.Get_LimitNumberItem;

               // if(items.Get_Name == LocalAccessValue.health_item)
                   // print(items.Get_Name);             
			}
		}
	}
}
