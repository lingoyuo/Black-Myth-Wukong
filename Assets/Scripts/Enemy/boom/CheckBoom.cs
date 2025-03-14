using UnityEngine;
using System.Collections;

public class CheckBoom : MonoBehaviour {

    public float damage = 1.0f;

	void OnTriggerEnter2D (Collider2D coll)
    {
        if ((coll.gameObject.tag == "Player") || (coll.gameObject.tag == "Enemy"))
            coll.gameObject.GetComponent<InforStrength>().LoseHealth(damage);                         

    }
}
