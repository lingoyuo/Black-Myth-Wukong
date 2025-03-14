using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {

    public GameObject[] ListBackground;

    public Sprite Background;

    void Update()
    {
        MoveBackground();
    }

    void MoveBackground()
    {

        float realWidthMoveBackground = Background.bounds.size.x;

        for(int i = 0;i<ListBackground.Length;i++)
        {
            float distance = Camera.main.transform.position.x - ListBackground[i].transform.position.x;

            if(distance > realWidthMoveBackground)
            {
                ListBackground[i].transform.position = new Vector2(ListBackground[i].transform.position.x + realWidthMoveBackground * 3,ListBackground[i].transform.position.y);
            }
        }
    }

}
