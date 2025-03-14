using UnityEngine;
using System.Collections;

public class BearDetectPlayer : MonoBehaviour {

    public Transform Player { get { return player; } }
    public float PosDetectPlayer { get { return posDetectPlayer; } }

    [HideInInspector]
    public bool detected;

    private Transform player;
    private float posDetectPlayer;

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" )
            player = other.transform.GetComponentInParent<BoxCollider2D>().transform;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            player = null;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" )
        {          
            if (other.transform.position.x < transform.position.x)
                posDetectPlayer = other.transform.position.x - other.GetComponent<BoxCollider2D>().size.x/2 + 0.01f;
            else
                posDetectPlayer = other.transform.position.x + other.GetComponent<BoxCollider2D>().size.x/2 - 0.01f;

            detected = true;
        }
    }
}
