using UnityEngine;
using System.Collections;

public class GroundFly : MonoBehaviour {

    public Transform Destination;
    public Transform Effect;
    public float DistanceMoveEffect;

    private Animator anim;
    private bool playerIn;
    private Vector2 desPosEffect;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerIn = false;

        if (Effect)
            desPosEffect = new Vector2(Effect.position.x + DistanceMoveEffect, Effect.position.y);
    }

    void Update()
    {
        if (playerIn)
            transform.position = Vector3.MoveTowards(transform.position, Destination.position, Time.deltaTime*2);

        if(transform.position == Destination.position)
        {
            if(Effect)
            {
                Effect.position = Vector2.MoveTowards(Effect.position, desPosEffect, Time.deltaTime*3);
            }
        }

    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("move");
            playerIn = true;

            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(null);
            playerIn = false;
        }
    }
}
