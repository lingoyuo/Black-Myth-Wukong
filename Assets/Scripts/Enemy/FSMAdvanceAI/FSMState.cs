using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class FSMState
{
    protected Dictionary<Transition, FSMStateID> map = new Dictionary<Transition, FSMStateID>();
    protected FSMStateID stateID;
    protected Animator anim;
    protected SensorDetectEnviromentBear envi_detect;

    public FSMStateID ID { get { return stateID; } }

    /// <summary>
    /// Add pair transion and id in this map
    /// </summary>
    public void AddTransition(Transition transition,FSMStateID id)
    {
        if(transition == Transition.None || id == FSMStateID.None)
        {
           // Debug.LogWarning("FSMState: Null transtion not allowed!");
            return;
        }

        if(map.ContainsKey(transition))
        {
           // Debug.LogWarning("FSMState Error: transition is already inside in map! ");
            return;
        }

        map.Add(transition, id);
    }

    /// <summary>
    /// This method delete pair transition in this map
    /// </summary>

    public void DeleteTransition(Transition trans)
    {
        if(trans == Transition.None)
        {
           // Debug.LogError("FSM error: Null transition not allowed");
            return;
        }

        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }

        //Debug.LogError("FSMstate error: Transition passed was not on this state's list");
    }

    /// <summary>
    /// This method return new state if this state receive transtion
    /// </summary>
    public FSMStateID GetOutputState(Transition trans)
    {

        if(trans == Transition.None)
        {
           // Debug.LogError("FSMstate error: Nulltransition is not allowed");
            return FSMStateID.None;
        }

        if (map.ContainsKey(trans))
            return map[trans];

       // Debug.LogError("FSMstate error: " + trans + " Transition passed to the State not on list");

        return FSMStateID.None;
    }
    /// <summary>
    /// This method will decide if state will transiton to another on its list
    /// </summary>
    public abstract void Reason(Transform npc);

    /// <summary>
    /// This moethod will control behaviour NPC 
    /// </summary>
    public abstract void Act(Transform npc);



}
