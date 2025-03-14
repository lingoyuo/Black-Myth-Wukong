using UnityEngine;
using System.Collections.Generic;
using System;

class BearAttackState : FSMState
{
    BearController bear;
    BearDetectPlayer sensor;

    public BearAttackState( Animator anim,BearDetectPlayer sensor)
    {
        this.anim = anim;
        this.sensor = sensor;
        stateID = FSMStateID.Attacking;
        bear = anim.gameObject.GetComponent<BearController>();
    }

    public override void Act(Transform npc)
    {
        anim.speed = 1.0f;
        if (bear.timer > npc.GetComponent<InforStrength>().AttackSpeed && !bear.get_hit)
        {
                if (sensor.Player.position.x > npc.transform.position.x + 0.5f)
                    npc.transform.localScale = new Vector3(1, 1, 1);
                else if (sensor.Player.position.x < npc.transform.position.x - 0.5f)
                    npc.transform.localScale = new Vector3(-1, 1, 1);

            if (!anim.GetCurrentAnimatorStateInfo(1).IsName("bear_get_hit"))
            {
                bear.timer = 0;
                anim.SetTrigger("attack");
            }
        }
    }

    public override void Reason(Transform npc)
    {
        if (!sensor.Player)
        {
            anim.SetTrigger("move");
            npc.GetComponent<BearController>().SetTransition(Transition.LostPlayer);
        }
    }
}

