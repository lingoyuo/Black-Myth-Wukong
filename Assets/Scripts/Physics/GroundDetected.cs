using UnityEngine;
using System.Collections;

public class GroundDetected : MonoBehaviour {

    public HandlePlatform handle;

    private BoxCollider2D box;
    private Rigidbody2D rid;
    private PlayerController player;

    private Vector2 center;
    private float offset;

    private RaycastHit2D[] rayHits;
    private Vector2 rayPoints;

    void Start()
    {
        rid = GetComponentInParent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        center = new Vector3(0,- box.size.y - offset);
        offset = 0.1f;
        rayHits = new RaycastHit2D[3];

        player = GetComponentInParent<PlayerController>();
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        if (transform.position.x > other.transform.position.x)
    //            rid.AddForce(Vector2.right * 500);
    //        else
    //            rid.AddForce(-Vector2.right * 500);
    //    }
    //}

    void FixedUpdate()
    {
        RaycastDect();
        CheckGround();
    }

    void RaycastDect()
    {
        rayPoints = new Vector2(center.x - box.size.x / 2 , center.y);
        rayHits[0] = Physics2D.Raycast(transform.TransformPoint(rayPoints), Vector3.up, offset, 1 << 13);
        rayHits[1] = Physics2D.Raycast(transform.TransformPoint(center), Vector3.up, offset, 1 << 13);
        rayPoints = new Vector2(center.x + box.size.x / 2 , center.y);
        rayHits[2] = Physics2D.Raycast(transform.TransformPoint(rayPoints), Vector3.up, offset, 1 << 13);
    }

    void CheckGround()
    {
        foreach (RaycastHit2D _ray in rayHits)
        {
            if (_ray.collider)
            {
                if (_ray.collider.gameObject.tag == "MovePlatform")
                {
                    player.grounded = true;
                    player.ground_plaform_move = true;
                    handle.StandingOn = _ray.collider.gameObject;
                    return;
                }
                else if (_ray.collider.gameObject.tag == "Platform")
                {
                    player.grounded = true;
                    return;
                }
            }
        }
        player.ground_plaform_move = false;
        handle.StandingOn = null;
        player.grounded = false;

    }

    //void OnDrawGizmos()
    //{
    //    offset = 0.02f;
    //    box = GetComponent<BoxCollider2D>();
    //    center = new Vector3(0, -box.size.y / 2 - offset);
    //    var ray = new Vector2(center.x - box.size.x / 2 + offset, center.y);
    //    Debug.DrawRay(transform.TransformPoint(ray), Vector3.up * offset, Color.red);
    //}

}
