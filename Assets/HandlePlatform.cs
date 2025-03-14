using UnityEngine;
using System.Collections;

public class HandlePlatform : MonoBehaviour {

    enum movePlatform
    {
        stand,
        left,
        right
    }

    [HideInInspector]
    public GameObject StandingOn;

    [HideInInspector]
    public float horizontal;                                                    // Get input from use

    private PlatformMove path;

    private Vector3 currentPos;
    private bool active;
    private Vector2 velocityPlatform;

    private float isRight;

    // Constant force
    const float force_move = 4.5f;
    const float force_push = 1.0f;

    void Update()
    {
        HandlePlatforms();     
    }

    void HandlePlatforms()
    {
        if (StandingOn != null)
        {
            if(path == null)
            {
                path = GetComponentInParent<PlatformMove>();
            }
            
            if(path !=null)
            {
                if (path._moving == PlatformMove.TypeMoving.vertical || path._moving == PlatformMove.TypeMoving.right)
                    isRight = 1;
                else
                    isRight = -1;
            }       
            Move(horizontal);
        }
    }

    // Move by translate platform
    private void Move(float horizontal)
    {
        if (Mathf.Sign(horizontal * isRight) > 0)
            transform.Translate(Vector2.right * horizontal * force_move * Time.deltaTime);
        else
            transform.Translate(Vector2.right * horizontal * (force_move + force_push) * Time.deltaTime);
        Flip(horizontal);
    }

    // flip player
    void Flip(float horizontal)
    {
        if (horizontal != 0)
            transform.localScale = new Vector2(Mathf.Sign(horizontal) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }

}
