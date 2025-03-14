using UnityEngine;
using System.Collections;

public class SpideBoss02HitDamage : MonoBehaviour {

    Boss02_spide bossBrain;
    float timer;

    void Start()
    {
        bossBrain = GetComponentInParent<Boss02_spide>();
    }
    void Update()
    {
        if(timer < 10.0f)
            timer += Time.deltaTime;
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "WeaponPlayer" & timer > 3.0f 
            && bossBrain.stateSkill == Boss02_spide.StateSkill.None 
            && bossBrain.GetStateBoss !=Boss02_spide.State.idle 
            && bossBrain.GetStateBoss != Boss02_spide.State.spawnSilk)
        {
            GetComponent<InforStrength>().LoseHealth(1);
            timer = 0;
            GetComponentInParent<Boss02_spide>().setState = Boss02_spide.State.pullSilk;

            int rand_num = Random.Range(0, 100);

            if(rand_num < 50)
               bossBrain.stateSkill = Boss02_spide.StateSkill.Normal;
            else
               bossBrain.stateSkill = Boss02_spide.StateSkill.Special;
        }
    }
}
