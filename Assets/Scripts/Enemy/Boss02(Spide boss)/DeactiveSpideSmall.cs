using UnityEngine;
using System.Collections;

public class DeactiveSpideSmall : MonoBehaviour {

    float timer;
    bool spide_dead;

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "WeaponPlayer")
        {
            GetComponent<InforStrength>().LoseHealth(1);
            GetComponent<Animator>().SetBool("die", true);       
            spide_dead = true;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponentInParent<SmallSpide>().setState = SmallSpide.State.Dead;
        }
    }

    void Update()
    {
        if(spide_dead)
        {
            timer += Time.deltaTime;
            if(timer > 4.0f)
            {
                timer = 0.0f;
                spide_dead = false;
                GetComponent<CircleCollider2D>().enabled = true;
                GetComponentInParent<SmallSpide>().Deactive();
            }
        }
    }
}
