using UnityEngine;
using System.Collections;


public class ProtectAura : MonoBehaviour {

    public float forcePushBack = 300.0f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var dir = other.transform.position - transform.position;
            other.GetComponent<Rigidbody2D>().AddForce(dir * forcePushBack);
        }
    }
}
