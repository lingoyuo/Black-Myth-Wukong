using UnityEngine;
using System.Collections;
using System;

class BearIdleState : FSMState
{
    BearDetectPlayer sensor;
    float TIME_WAIT = float.MaxValue;
    float timer;

    public BearIdleState( Animator anim,BearDetectPlayer sensor)
    {
        this.anim = anim;
        stateID = FSMStateID.Idling;
        this.sensor = sensor;
    }

    public override void Act(Transform npc)
    {
        timer += Time.deltaTime;
    }

    public override void Reason(Transform npc)
    {       
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("bear_idle"))
            TIME_WAIT = anim.GetCurrentAnimatorStateInfo(0).length;

        if (timer>TIME_WAIT)
        {
            timer = 0;
            npc.GetComponent<BearController>().SetTransition(Transition.PatrolContinue);
        }

        if (sensor.Player)
        {
            anim.SetTrigger("move");
            npc.GetComponent<BearController>().SetTransition(Transition.SawPlayer);
        }
    }
}

