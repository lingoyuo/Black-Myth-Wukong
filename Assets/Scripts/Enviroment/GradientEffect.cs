using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GradientEffect : MonoBehaviour {

    public SpriteRenderer background;

    public List<SpriteRenderer> enviroments;

    public float pos_active;

    private Color color_night;

    private Color color_night_enviroment;

    void Start()
    {
        color_night = new Color32(146 , 146 , 146 , 255);

        color_night_enviroment = new Color32(118, 118, 118, 255);
    }

    void Update()
    {
        if (transform.position.x > pos_active)
        {
            background.color = Color32.Lerp(background.color, color_night, Time.deltaTime * 0.1f);

            foreach (SpriteRenderer envi in enviroments)
            {
                envi.color = Color32.Lerp(envi.color, color_night_enviroment, Time.deltaTime * 0.1f);
            }
        }    
    }
}
