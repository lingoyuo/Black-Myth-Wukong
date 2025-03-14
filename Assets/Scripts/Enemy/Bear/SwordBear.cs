using UnityEngine;
using System.Collections;

public class SwordBear : MonoBehaviour {

    public float damage = 1.0f;

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            var player = other.gameObject.GetComponent<InforStrength>();

            if(!player.GetComponent<InforStrength>().shield_active)
                other.gameObject.GetComponent<PlayerController>().PlayerGetHitControlPhysics(transform.position);
            player.LoseHealth(damage);

        }
    }
}
