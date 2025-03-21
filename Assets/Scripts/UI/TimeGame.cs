using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeGame : MonoBehaviour {

	public float timeBonus,currentTime1,currentTime2;
	float t,timeGame;
	public Text text;

    public int Get_Time { get { return end; } }

	int end;

	GameManager gameManager;

	public bool checkCount,checkTimeBonus;
	public GameObject gameOver;

	ItemManager items;

	void Awake(){
		gameManager = FindObjectOfType<GameManager> ();
		timeGame = gameManager.time_live;
    }

	void Start(){
		items = (ItemManager)FindObjectOfType(typeof(ItemManager));
		end = 1;
		checkCount = false;
		text.text = timeGame.ToString ();

		foreach(ItemPlayer item in items.ListItems){
			if(item.Get_Name == "BONUS_TIME"){
				if(item.Get_AmountSkill > 0){
					checkTimeBonus = true;
					timeBonus = 30;
				}
				else{
					checkTimeBonus = false;
					timeBonus = 0;
					checkCount = true;
				}
			}
		}
	}

	void Update(){
		if(checkTimeBonus){
			t += Time.deltaTime;
			currentTime1 = t;
			if(t >= timeBonus){
				checkTimeBonus = false;
				t = 0;
				checkCount = true;
			}
		}

		if (checkCount) {
			t += Time.deltaTime;
			end = Mathf.FloorToInt (timeGame - t);
			text.text = end.ToString ();
			currentTime2 = end - timeGame;
			if (end <= 0) {
				checkCount = false;
				gameOver.SetActive(true);
			}
		} 
	}
}
