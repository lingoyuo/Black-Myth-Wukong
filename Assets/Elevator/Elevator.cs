using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

    const float RANGE = 1.2f;

    public Transform posConnect;
    public float speed = 0.1f;
    [HideInInspector]
    public bool active;
    [HideInInspector]
    public bool playerInElevator;

    private Transform player;
    private SpringJoint2D spr;
    private Rigidbody2D rig;
    
    private bool activing;

    void Update()
    {
        if (player )
        {
            if (active && !activing)
                ActiveElevator();

            if (spr)
            {
                spr.distance = Mathf.Lerp(spr.distance, 0, Time.deltaTime * speed);

                // Check disactive elevator
                if (spr.distance < 0.1f)
                    DiactiveElevator();
            }
        }
    }

    // Active elevator
    void ActiveElevator()
    {
        rig = gameObject.AddComponent<Rigidbody2D>();
        spr = gameObject.AddComponent<SpringJoint2D>();

        rig.mass = 1000;
        rig.constraints = RigidbodyConstraints2D.FreezeRotation;

        spr.connectedAnchor = posConnect.position;
        spr.distance = posConnect.position.y - transform.position.y;
        spr.frequency = 0.8f;

        gameObject.GetComponentInChildren<PlatformEffector2D>().enabled = false;

        player.parent = gameObject.transform;
        player.gameObject.layer = 4;

        activing = true;
    }

    void DiactiveElevator()
    {
        Destroy(spr);
        Destroy(rig);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject.transform;
            playerInElevator = true;
        }

    }
}
