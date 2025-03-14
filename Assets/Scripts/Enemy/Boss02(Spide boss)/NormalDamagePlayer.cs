using UnityEngine;
using System.Collections;

public class NormalDamagePlayer : MonoBehaviour {

    public float damage = 1;

    public bool dead_point = false;

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            var infor = other.GetComponent<InforStrength>();

            // player not dead, we need give dame for player
            if (!other.GetComponent<PlayerController>().playerDead)
            {
                // if dead_points is true, break shield to set player dead
                if (dead_point)
                    infor.shield_active = false;

                // if shield still have, nothing to do
                if (!infor.shield_active)
                {
                    other.GetComponent<PlayerController>().PlayerGetHitControlPhysics(transform.position);
                    infor.LoseHealth(damage);
                }
            }
        }
    }
}
