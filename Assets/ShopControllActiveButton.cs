using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopControllActiveButton : MonoBehaviour {

	public GameObject[] bottons;

	public void DeactiveRemainBotton(GameObject botton) {

		foreach (GameObject _botton in bottons) {
			if(_botton != botton) {
				_botton.GetComponent<BottonShopControllAnimation>().ActiveAnimator();
			}
		}
	}
}
