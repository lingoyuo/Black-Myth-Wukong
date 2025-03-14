using UnityEngine;
using System.Collections;

public abstract class EnemyStateController : MonoBehaviour {

	public enum State
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK,
        PLAYDEAD
    }

    public State CurrentStateEnemy = State.IDLE;

    abstract public void ChangeState();

    abstract public void ActionSate();

    virtual public void Update()
    {
        ChangeState();
        ActionSate();
    }
}
