using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BottonShopControllAnimation : MonoBehaviour {
	
	public bool Active;
	
	private Animator anim;
	private SMB_Botton smb_bottom;
	private ShopControllActiveButton shopControl;
	private Button button;
	private Image image;

	public GameObject tabControl;
	
	void Start(){
		anim = GetComponent<Animator> ();
		smb_bottom = anim.GetBehaviour <SMB_Botton> ();
		button = GetComponent<Button> ();
		smb_bottom.control = this;
		shopControl = GetComponentInParent<ShopControllActiveButton> ();
		image = GetComponent<Image> ();
		if (Active) {
			
			GetComponent<Image> ().color = new Color (1f, 1f, 0f);
			//button.interactable = false;
		}
	}
	
	void Update(){
		image.color = new Color (1f, 1f, 0f);
		if (Active) {
			image.color = new Color (1f, 1f, 0f);
		}else{
			image.color = new Color (1, 1, 1);
		}
	}
	
	
	// ACtive botton and deactive animator
	public void DeactiveAnimator(){
		Active = true;
		tabControl.transform.SetAsLastSibling ();
		tabControl.SetActive (true);
		shopControl.DeactiveRemainBotton (this.gameObject);
	}
	
	// Deactive this botton
	public void ActiveAnimator(){
		Active = false;
		tabControl.SetActive (false);
	}
	
	
}
