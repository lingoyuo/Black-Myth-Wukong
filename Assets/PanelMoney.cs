using UnityEngine;
using System.Collections;

public class PanelMoney : MonoBehaviour {

	public GameObject btMoreMoney,btExit;
	AudioSource audioGame;
	public GameObject btCoin;

	public void BtExit(){
		audioGame = btExit.GetComponent<AudioSource> ();
		AudioManager.Instances.PlayAudioEffect (audioGame);

		StartCoroutine (SoundMoneyOn ());
	}

	public void btMoreCoin(){
		audioGame = btMoreMoney.GetComponent<AudioSource> ();
		AudioManager.Instances.PlayAudioEffect (audioGame);

		StartCoroutine (SoundMoneyOn ());
		btCoin.GetComponent<BottonShopControllAnimation> ().DeactiveAnimator ();
	}

	IEnumerator SoundMoneyOn(){
		yield return new WaitForSeconds(0.1f);
		gameObject.SetActive (false);
	}

	IEnumerator SoundMoreCoin(){
		yield return new WaitForSeconds(0.1f);
		gameObject.SetActive (false);
	}
}
