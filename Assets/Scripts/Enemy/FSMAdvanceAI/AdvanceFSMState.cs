using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum Transition
{
    None = 0,
    ReachPatrolPoints,
    PatrolContinue,
    SawPlayer,
    ReachPlayer,
    LostPlayer,
    NoHealth,
}

public enum FSMStateID
{
    None = 0 ,
    Idling,
    Patrolling,
    Chasing,
    Attacking,
    Dead,
}

public class AdvanceFSMState : FSM {

    private List<FSMState> fsmStates;

    private FSMStateID currentStateID;

    public FSMStateID CurrentStateID { get { return currentStateID; } }

    private FSMState currentState;

    public FSMState CurrentState { get { return currentState; } }

    public AdvanceFSMState()
    {
        fsmStates = new List<FSMState>();
    }

    public void AddFSMState(FSMState fsmState)
    {
        if (fsmState == null)
            Debug.LogError("FSM ERROR: Null refernce is not allowed!");

        if(fsmStates.Count ==0)
        {
            fsmStates.Add(fsmState);
            currentState = fsmState;
            currentStateID = fsmState.ID;
            return;
        }

        foreach(FSMState state in fsmStates)
        {
            if(state.ID == fsmState.ID)
            {
                Debug.LogError("FSM error: Trying to add a state that was aldrealy inside the list!");
            }
        }

        fsmStates.Add(fsmState);
    }

    public void DeleteState(FSMStateID fsmState)
    {
        if(fsmState == FSMStateID.None)
        {
            Debug.LogError("FSM Error: null id is not allowed");
            return;
        }


        foreach(FSMState state in fsmStates)
        {
            if(state.ID == fsmState)
            {
                fsmStates.Remove(state);
                return;
            }
        }

        Debug.LogError("FSM error: The state passed can not on the lust. Impossible to delete!");
    }

    public void PerformTransition(Transition trans)
    {
        if(trans == Transition.None)
        {
            Debug.LogError("FSM Error: Null transition is not allowed");
            return;
        }

        FSMStateID id = currentState.GetOutputState(trans);

        if(id == FSMStateID.None)
        {
            Debug.LogError("FSM error: Current state does not have a target for this transiton!");
            return;
        }

        currentStateID = id;

        foreach (FSMState state in fsmStates)
        {
            if(state.ID == currentStateID)
            {
                currentState = state;
                break;
            }
        }
    }
}
