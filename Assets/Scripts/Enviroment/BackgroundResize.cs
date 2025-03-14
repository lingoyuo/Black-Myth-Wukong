using UnityEngine;
using System.Collections;

public class BackgroundResize : MonoBehaviour {

    private SpriteRenderer bg_sprite;

    void Awake()
    {
        bg_sprite = GetComponent<SpriteRenderer>();
        if (!bg_sprite)
        {
            Debug.LogError("Please add sprite to background !");
            return;
        }

        // Scale height background fix with height screen according to othorgraphic size camera
        var scale = Camera.main.orthographicSize*2 / bg_sprite.bounds.size.y;
        transform.localScale = new Vector2(transform.localScale.x * scale, transform.localScale.y * scale);

        // Caculate real width
        var camera_width = Camera.main.orthographicSize * Screen.width / Screen.height;
        var bg_width = bg_sprite.bounds.size.x / 2;

        // Change local postion suitable with screen
        transform.localPosition = new Vector2(transform.localPosition.x + (bg_width - camera_width), transform.localPosition.y);


    }
}
