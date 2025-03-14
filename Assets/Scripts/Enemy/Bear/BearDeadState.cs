using UnityEngine;
using System.Collections;
using System;

public class BearDeadState : FSMState {

	public BearDeadState (Animator anim)
    {
        this.anim = anim;
        stateID = FSMStateID.Dead;
    }

    public override void Act(Transform npc)
    {
        Debug.Log("dada");
        anim.SetTrigger("dead");
    }

    public override void Reason(Transform npc)
    {
        
    }
}
