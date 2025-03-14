using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadText : MonoBehaviour {

	public Text[] textRock;
	public Text[] textBoom;
	public Text[] textBum;

	public Text[] textRockAccept;
	public Text[] textBoomAccept;
	public Text[] textBumAccept;

	public Text textShoes,textShoes1,textArmor,textArmor1,
	textTime,textTime1,textGold,textGold1,textHealth,textHealth1;

	ConstantPriceShop constPrice;

	void Awake(){
		constPrice = FindObjectOfType<ConstantPriceShop>();
	}

	void Start(){

		textShoes.text = constPrice.shoe_cost.ToString ();
		textShoes1.text = constPrice.shoe_cost.ToString ();
		textArmor.text = constPrice.defense_cost.ToString ();
		textArmor1.text = constPrice.defense_cost.ToString ();
		textTime.text = constPrice.bonus_time_cost.ToString ();
		textTime1.text = constPrice.bonus_time_cost.ToString ();
		textGold.text = constPrice.bonus_gold_cost.ToString ();
		textGold1.text = constPrice.bonus_gold_cost.ToString ();
		textHealth.text = constPrice.health_cost.ToString ();
		textHealth1.text = constPrice.health_cost.ToString ();


		for (int i = 0; i < textRock.Length; i++) {
			textRock[i].text = (constPrice.rock_cost * (i + 1)).ToString();
			textRockAccept[i].text = (constPrice.rock_cost * (i + 1)).ToString();
		}
		for (int i = 0; i < textBoom.Length; i++) {
			textBoom[i].text = (constPrice.boom_cost * (i + 1)).ToString();
			textBoomAccept[i].text = (constPrice.boom_cost * (i + 1)).ToString();
		}
		for (int i = 0; i < textBum.Length; i++) {
			textBum[i].text = (constPrice.bumerang_cost * (i + 1)).ToString();
			textBumAccept[i].text = (constPrice.bumerang_cost * (i + 1)).ToString();
		}
	}
}
