using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextInGame : MonoBehaviour {

    [Space(10)]
    [Header("Number value text:")]
    public int number;                                                  // number string in value text display

    [Space(10)]
    [Header("Value text")]
    public int value;                                                   // value text will display                                   

    [Space(10)]
    [Header("Speed settting: ")]
    public float speed = 100.0f;                                        // speed run text

    public int GET_currentValue { get { return currentValue; } }
    private Text text;
    private int zero_remain;
    private int currentValue;

    void Awake()
    {
        text = GetComponent<Text>();

        //// first setting text
        //for (int i = 0; i < number; i++)
        //    text.text += "0";
        //currentValue = value;
    }


    void Update()
    {
        if(currentValue != value)
        {
            text.text = null;
            zero_remain = RemainZero(value);

            for(int i=0;i<zero_remain;i++)
            {
                text.text += "0";
            }

            Text_Value();

            text.text += Text_Value();
        }
    }

    // value convert to string
    string Text_Value()
    {
        currentValue = (int) Mathf.MoveTowards(currentValue, value, Time.deltaTime *speed);
        
        return currentValue.ToString();      
    }

    // check remain zero
    int RemainZero(int value)
    {
        return number - value.ToString().Length;
    }


    // this function call intal outside class
    public void IntialValue(int intial_value)
    {
        text = GetComponent<Text>();
        text.text = null;

        for (int i = 0; i < RemainZero(intial_value); i++)
            text.text += "0";

        value = intial_value;
        currentValue = intial_value;
        text.text += currentValue.ToString();
    }
}

