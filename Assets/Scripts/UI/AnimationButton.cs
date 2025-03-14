using UnityEngine;
using System.Collections;

public class AnimationButton : MonoBehaviour {

	public void PressButton()
    {
        GetComponent<Animator>().SetTrigger("Pressed");
    }

    public void NormalButton()
    {
        GetComponent<Animator>().SetTrigger("Normal");
    }
        

}
