using UnityEngine;
using System.Collections;


// This cript help rock not thorugh to ground
public class RockDetectGround : MonoBehaviour {

    CircleCollider2D col;
    Vector2 center_col;

    RaycastHit2D hit;

    void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        center_col = transform.TransformPoint(col.offset);
    }

    void FixedUpdate()
    {
        Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, -90) * Vector2.right);
        center_col = transform.TransformPoint(col.offset);
        hit = Physics2D.Raycast(center_col, dir.normalized, col.radius * transform.localScale.x, 1 << 13);

        if(hit)
        {
            var offset = transform.position.y - hit.point.y;
            offset = col.radius * transform.localEulerAngles.x - offset;
            transform.position = new Vector2(transform.position.x, transform.position.y + offset+col.radius*transform.localScale.x);
        }
    }

    //void OnDrawGizmos()
    //{
    //    var cirCol = GetComponent<CircleCollider2D>();
    //    Vector2 center = transform.TransformPoint(cirCol.offset);


    //    Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, -90) * Vector2.right);

    //    Debug.DrawRay(center, dir.normalized * (cirCol.radius * transform.localScale.x), Color.red);
    //    //hit = Physics2D.Raycast(center, dir.normalized, cirCol.radius * transform.localScale.x, 1 << 13);

    //    //if (hit)
    //    //{
    //    //    print(hit.transform.gameObject);
    //    //    var offset = transform.position.y - hit.point.y;
    //    //    offset = cirCol.radius * transform.localEulerAngles.x - offset;
    //    //    transform.position = new Vector2(transform.position.x, transform.position.y + offset + cirCol.radius*transform.localScale.x);
    //    //}


    //}
}
