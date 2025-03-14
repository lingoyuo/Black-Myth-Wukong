using UnityEngine;
using System.Collections;
using System;

public class BeeStateController : EnemyStateController
{

    public enum DirectMove
    {
        left,
        right
    }

    public float RangeMove = 3.0f;
    public DirectMove direct = DirectMove.right;

    CheckEnemy findPlayer;

    float centerPos;
    

    void Awake()
    {
        findPlayer = transform.GetChild(0).GetComponent<CheckEnemy>();
        centerPos = transform.position.x;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<BeeMove>().enabled = false;
    }

    public override void ChangeState()
    {
        if (findPlayer.DetectPlayer) 
            CurrentStateEnemy = State.ATTACK;
    }

    public override void ActionSate()
    {
        if (CurrentStateEnemy == State.IDLE)
            Patrol();
        else
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<BeeMove>().enabled = true;
        }
    }

    void Patrol()
    {
        if (direct == DirectMove.right)
            MoveRight();
        else
            MoveLeft();
    }

    void MoveLeft()
    {
        transform.Translate(-transform.right*Time.deltaTime);
        transform.localScale = new Vector3(-Mathf.Abs( transform.localScale.x), transform.localScale.y, transform.localScale.z);
        if (transform.position.x < centerPos - RangeMove)
            direct = DirectMove.right;
    }

    void MoveRight()
    {
        transform.Translate(transform.right*Time.deltaTime);
        transform.localScale = new Vector3( Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        if (transform.position.x > centerPos + RangeMove)
            direct = DirectMove.left;
    }
}
