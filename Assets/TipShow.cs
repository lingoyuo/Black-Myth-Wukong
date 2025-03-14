using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TipShow : MonoBehaviour {

    public List<string> list_tips;

    Text tip_text;

    void Awake()
    {
        tip_text = GetComponent<Text>();

        tip_text.text = "Tip:  " + list_tips[Random.Range(0, list_tips.Count)];
    }
}
