using UnityEngine;
using System.Collections;

public class CloundMoveEffect : MonoBehaviour {

    public float Speed = 5.0f;

    private Vector2 startPos;
    private SpriteRenderer color;
    private bool unactive;

    void Start()
    {
        startPos = transform.position;
        color = GetComponent<SpriteRenderer>();
        color.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

    }

    void Update()
    {
        // Move Clound
        transform.Translate(-Vector3.right * Time.deltaTime * Speed);

        if(color.color.a > 0.95f && !unactive)
            unactive = true;

        // Change color Clound
        if (!unactive)
            color.color = Color.Lerp(color.color, new Color(1.0f, 1.0f, 1.0f, 1.0f), Time.deltaTime * Speed);
        if (unactive)
            color.color = Color.Lerp(color.color, new Color(1.0f, 1.0f, 1.0f, 0.0f), Time.deltaTime * Speed);

        if(unactive && color.color.a < 0.05f)
        {
            unactive = false;
            transform.position = startPos;
            color.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        
    }

}
