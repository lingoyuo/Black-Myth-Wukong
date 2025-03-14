using UnityEngine;
using System.Collections;

public class SocialButtonController : MonoBehaviour {

	public Animator anim;
		
	bool clickSocial;
	public GameObject invite;

	void Start(){
		clickSocial = false;
	}

	public void ButtonSocial(){
		if (clickSocial) {
			anim.SetBool ("max", false);
			clickSocial = false;
		} else {
			anim.SetBool("max",true);
			clickSocial = true;
		}
	}

	public void MouseExit(){
		clickSocial = false;
		anim.SetBool ("max", false);
	}

	public void InviteShow(){
		invite.SetActive (true);
	}

    public void CloseInviteFB()
    {
        invite.SetActive(false);
    }
}
