using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseExitButton : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler{
	
	Animator anim;
	
	void Start(){
		anim = GetComponent<Animator> ();
	}

	#region IPointerEnterHandler implementation
	void IPointerEnterHandler.OnPointerEnter (PointerEventData eventData)
	{
		anim.Play ("Highlighted");
	}
	#endregion


	#region IPointerExitHandler implementation
	public void OnPointerExit (PointerEventData eventData)
	{
		anim.Play("Normal");
	}
	#endregion



}
