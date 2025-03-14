using UnityEngine;
using System.Collections;

public class Boss01Combat : MonoBehaviour {

    public enum State
    {
        Idle,
        Attack,
        Dead
    }

    public Transform PosSpawnBullet;

    public float posDitectPlayer = 0.0f;
    public State StateBoss { get { return stateBoss; } }

    public float rangeSpawnBullet = 1.0f;

    public GameObject effectLeaf;
    public PoolManager bulletFrame;
    public PoolManager effectExplosionBullet;

    public GameObject fus01;
    public GameObject fus02;

    public Transform posMoveWhenDie;
    int index_pos;
    const float TIME_WAIT = 2.0f;
    float time_count;


    State stateBoss;

    Transform player;
    Boss01Move checkAttack;
    InforStrength Strength;
    Animator anim;

    float timer;


    void Start()
    {
        anim = GetComponent<Animator>();

        stateBoss = State.Idle;

        checkAttack = GetComponent<Boss01Move>();

        Strength = GetComponent<InforStrength>();

        index_pos = 0;

        if (!bulletFrame || !effectExplosionBullet)
            print("Error! null obj pull");
    }

    void Update()
    {

        timer += Time.deltaTime;

        if (Strength.Get_Health <= 0)
            stateBoss = State.Dead;

        switch (stateBoss)
        {
            case State.Idle:
                StateIDle();
                break;
            case State.Attack:
                StateAttack();
                break;
            case State.Dead:
                StateDead();
                break;
        }
    }
    
    void StateIDle()
    {
        if (FindPlayer())
            stateBoss = State.Attack;
    }


    // Tire state : move to pos path
    void StateDead()
    {
        if (index_pos >= posMoveWhenDie.childCount)
        {
            gameObject.SetActive(false);
            return;
        }

        time_count += Time.deltaTime;

        anim.SetTrigger("die");
        anim.speed = 1.5f;

        GetComponent<PolygonCollider2D>().enabled = false;

        if (time_count > TIME_WAIT)
        {

            if (index_pos == 0)
                transform.position = Vector3.MoveTowards(transform.position, posMoveWhenDie.GetChild(index_pos).position, Time.deltaTime * 5);
            else
                transform.position = Vector3.MoveTowards(transform.position, posMoveWhenDie.GetChild(index_pos).position, Time.deltaTime * 8);

            if (transform.position.x > posMoveWhenDie.GetChild(index_pos).position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);

            if (Vector2.Distance(transform.position, posMoveWhenDie.GetChild(index_pos).position) < 0.1f)
                index_pos++;
        }
    }

    // Boss in attack state
    void StateAttack()
    {
        anim.SetTrigger("fly");

        effectLeaf.GetComponent<Animator>().enabled = true;

        if(timer > Strength.AttackSpeed && checkAttack.Attack && GameObject.FindGameObjectWithTag("Player"))
        {
            anim.SetTrigger("attack");
            timer = 0;
        }

    }

    bool FindPlayer()
    {

        if (GameObject.FindGameObjectWithTag("Player"))
        {

            player = GameObject.FindGameObjectWithTag("Player").transform;

            if (player.transform.position.x > posDitectPlayer)
            {
                return true;
            }           
        }
        return false;
    }

    void SpawnBullet(Vector3 target)
    {
        var bullet = (GameObject)bulletFrame.GetObjPool(PosSpawnBullet.position);     

        if (bullet)
        {
            bullet.GetComponent<ParabolMove>().target = target;
            bullet.GetComponent<ParabolMove>().InitialPos();
            bullet.GetComponent<DeactiveSelfBullet>().ResetTimer();
            bullet.GetComponent<RotateDirection>().CheckDirection();

        }
        else
        {
            bullet = (GameObject)bulletFrame.RequestObjPool(PosSpawnBullet.position);
            bullet.GetComponent<ParabolMove>().target = target;
        }
        bullet.transform.GetChild(0).gameObject.SetActive(true);                                                                    // active partical effect bullet frame
        bullet.GetComponent<DeactiveSelfBullet>().poolEffect = effectExplosionBullet;
    }

    void BossFire()
    {
        var pos = player.transform.position;

        SpawnBullet(pos);
        SpawnBullet(new Vector2(pos.x + rangeSpawnBullet, pos.y));
        SpawnBullet(new Vector2(pos.x - rangeSpawnBullet, pos.y));
    }
    
    void SpawnFus()
    {
        if (stateBoss != State.Dead)
            return;

        Vector2 rand = (Vector2) transform.GetChild(3).position + Random.insideUnitCircle;
        Quaternion rotaRandom = Quaternion.Euler(new Vector3(0, 0, Random.Range(0.0f, 90.0f)));

        Instantiate(fus01, rand, rotaRandom);

        rand = (Vector2)transform.GetChild(3).position + Random.insideUnitCircle;
        rotaRandom = Quaternion.Euler(new Vector3(0, 0, Random.Range(0.0f, 90.0f)));

        Instantiate(fus02, rand, rotaRandom);
    }

}
