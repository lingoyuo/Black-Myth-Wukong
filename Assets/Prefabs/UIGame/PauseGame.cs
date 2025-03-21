using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

	public GameObject mobileController;

	public void StopGame(){
		//if (mobileController.activeSelf)
		//	mobileController.SetActive (false);
		Time.timeScale = 0;
	}

    public void ShowFullBanner()
    {
    }
}
