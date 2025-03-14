using UnityEngine;
using System.Collections;

public class TakeBackPlayer : MonoBehaviour {

    public InforStrength obj;

    void Update()
    {
        if (obj.Get_Health <= 0)
            gameObject.SetActive(false);
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (transform.position.x > other.transform.position.x)
                other.GetComponent<Rigidbody2D>().AddForce(-Vector2.right * 2000.0f);
            else
                other.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 2000.0f);
        }
    }
}
