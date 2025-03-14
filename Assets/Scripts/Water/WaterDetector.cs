using UnityEngine;
using System.Collections;

public class WaterDetector : MonoBehaviour {

    private Water water;
    private Rigidbody2D hit_rid;
    void Start()
    {
        water = transform.parent.GetComponent<Water>();
    }
    
    void OnTriggerEnter2D(Collider2D Hit)
    {
        hit_rid = Hit.GetComponent<Rigidbody2D>();
        if (hit_rid && Hit.tag == "Player")
        {
            water.Splash(transform.position.x, hit_rid.linearVelocity.y * hit_rid.mass / 200f);
        }
    }

    /*void OnTriggerStay2D(Collider2D Hit)
    {
        //print(Hit.name);
        if (Hit.rigidbody2D != null)
        {
            int points = Mathf.RoundToInt(Hit.transform.localScale.x * 15f);
            for (int i = 0; i < points; i++)
            {
                transform.parent.GetComponent<Water>().Splish(Hit.transform.position.x - Hit.transform.localScale.x + i * 2 * Hit.transform.localScale.x / points, Hit.rigidbody2D.mass * Hit.rigidbody2D.velocity.x / 10f / points * 2f);
            }
        }
    }*/

}
