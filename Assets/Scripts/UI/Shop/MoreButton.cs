using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoreButton : MonoBehaviour {

	public GameObject[] moreButtons;

	public void MoreItem(int value){
		moreButtons [value].GetComponent<BottonShopControllAnimation> ().DeactiveAnimator ();
	}
}
