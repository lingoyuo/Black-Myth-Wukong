using UnityEngine;
using System.Collections;

public class Boss01Move : MonoBehaviour {

    public float MAX_HEIGHT_FLY = 5.0f;                     // Max height that boss can fly
    public float SpeedMove = 1.5f;
    public float TimeWait = 2.0f;

    public float PosCanAttack = 1f;

    public Transform PosEdgeLeft;
    public Transform PosEdgeRight;

    public bool Attack { get { return attack; } }

    private bool attack;

    private float distanceMove;                             // Distance that boss can move

    /*
    ** Equation: y = b - (x +c )^2 * a ------- < Equation parabol >  
    */
    float a;
    float b;
    float c;

    float curPosX;
    float curPosY;

    float lowestHeight;

    float timer;

    // check edge left and edge right  
    Vector2 edgeLeft;
    Vector2 edgeRight;
    Vector2 distToMove;

    bool edge;

    Boss01Combat combatActive;


    void Start()
    {
        var sizeCamera_w = Camera.main.orthographicSize * Screen.width / Screen.height;
        var sizeBoss_w = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        distanceMove = sizeCamera_w * 2 - sizeBoss_w;


        b = MAX_HEIGHT_FLY;
        c = distanceMove / 2 - transform.position.x;

        // with x = startPostion.x and  y = startPostion.y
        a = (b - transform.position.y) / Mathf.Pow(transform.position.x + c, 2);

        edgeLeft = PosEdgeLeft.position;
        edgeRight = PosEdgeRight.position;

        distToMove = edgeLeft;

        combatActive = GetComponent<Boss01Combat>();

    }

    void Update()
    {

        timer += Time.deltaTime;

        if (combatActive.StateBoss == Boss01Combat.State.Attack)
            Move();
    }

    void Move()
    {
        curPosX = transform.position.x;    

        if (timer < TimeWait || !CheckPosToAttacK())
            attack = false;
        else
            attack = true;

        if ((distToMove == edgeLeft && transform.position.x > distToMove.x) ||
            (distToMove == edgeRight && transform.position.x < distToMove.x))
        {
            if (distToMove == edgeLeft)
            {
                if(timer > TimeWait)
                    curPosX -= Time.deltaTime * SpeedMove;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                if(timer>TimeWait)
                    curPosX += Time.deltaTime * SpeedMove;
                transform.localScale = new Vector3(1, 1, 1);
            }

            curPosY = b - Mathf.Pow(curPosX + c, 2) * a;

            transform.position = new Vector3(curPosX, curPosY);
        }
        else
        {
            ChangeDirectMove();
        }
    }

    void ChangeDirectMove()
    {
        distToMove = (distToMove == edgeLeft) ? edgeRight : edgeLeft;
        timer = 0;         
    }

    bool CheckPosToAttacK()
    {
        if (transform.position.y > PosCanAttack)
            return true;
        else
            return false;
    }

}
