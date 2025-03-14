using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisappearFillArea : MonoBehaviour {

    public bool DeativeZeroHealth;

    Slider slider;

    GameObject fillArea;

    void Start()
    {
        slider = GetComponent<Slider>();
        fillArea = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (slider.value <= 0 && !DeativeZeroHealth)
            gameObject.SetActive(false);
        else if (slider.value <= 0 && DeativeZeroHealth)
            fillArea.SetActive(false);

    }
}
