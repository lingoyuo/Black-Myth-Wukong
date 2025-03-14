using UnityEngine;
using System.Collections;

public class RockRollWithBear : MonoBehaviour {

    enum State
    {
        Idle,
        RunToRock,
        BringRock,
        ThrowRock,
        RockRoll
    }

    public float SpeedRockRoll = 5.0f;

    private Transform pos_bring_rock;
    private Transform pos_rock_appear;
    private Transform pos__throw_rock;
    private Transform pos_bear_disappear;
    private Transform pos_rock_roll;
    private Transform posA_roll;
    private Transform posB_roll;

    private Transform bear;
    private Transform rock;
    private Transform rock_anim;

    private Animator anim_bear;
    private State state;

    private float timer;
    private float des;

    private Collider2D col;

    void Start()
    {
        rock = transform.GetChild(0);
        bear = transform.GetChild(1);
        rock_anim = bear.transform.GetChild(0);
        pos_bring_rock = transform.GetChild(2);
        pos_rock_appear = bear.GetChild(1);
        pos__throw_rock = transform.GetChild(3);
        pos_bear_disappear = transform.GetChild(4);
        pos_rock_roll = bear.GetChild(2);
        posA_roll = transform.GetChild(5);
        posB_roll = transform.GetChild(6);

        anim_bear = bear.GetComponent<Animator>();
        rock_anim.gameObject.SetActive(false);
        col = rock.GetComponent<Collider2D>();

        state = State.Idle;

        des = posB_roll.position.x;
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.RunToRock:
                RunToRock();
                break;
            case State.BringRock:
                BringRock();
                break;
            case State.ThrowRock:
                ThrowRock();
                break;
            case State.RockRoll:
                RockRoll();
                break;
        }
    }

    void RunToRock()
    {
        anim_bear.SetBool("move", true);
        bear.position = Vector3.MoveTowards(bear.position, pos_bring_rock.position, Time.deltaTime*2.0f);

        if(bear.position == pos_bring_rock.position)
        {
            state = State.BringRock;
        }
    }

    void BringRock()
    {      
        rock_anim.gameObject.SetActive(true);
        rock.gameObject.SetActive(false);
        anim_bear.SetBool("bring", true);
        anim_bear.speed = 1.0f;

        timer += Time.deltaTime;
        if(timer > 1.5f)
            bear.position = Vector3.MoveTowards(bear.position, pos__throw_rock.position, Time.deltaTime);

        if(bear.position == pos__throw_rock.position)
        {
            timer = 0;
            state = State.ThrowRock;
        }
    }

    void ThrowRock()
    {
        anim_bear.SetBool("move", false);
        anim_bear.SetTrigger("throw");
        if(rock_anim.position == pos_rock_appear.position)
        {
            rock.gameObject.SetActive(true);
            rock.transform.position = pos_rock_appear.position;
            rock.gameObject.AddComponent<ParabolMove>().target = pos_rock_roll.position;
            rock.gameObject.GetComponent<ParabolMove>().maxSpeed = 2.0f;
            rock.gameObject.GetComponent<ParabolMove>().giaToc = 20.0f;

            rock_anim.gameObject.SetActive(false);
            state = State.RockRoll;

            anim_bear.SetBool("bring", false);
            anim_bear.SetTrigger("idle");
            anim_bear.SetBool("move", true);

            if (bear.transform.position.x > pos_bear_disappear.position.x)
                bear.localScale = new Vector3(-bear.localScale.x, bear.localScale.y, bear.localScale.z);
        }
    }

    void RockRoll()
    {
        anim_bear.speed = 2.0f;

        BearDisappear();

        if (rock.position.y < posA_roll.position.y + 0.5f)
        {
            rock.GetComponent<ParabolMove>().stop = true;

            Rolling();

            if (des != posB_roll.position.x)
                RoteRockLeft();
            else
                RoteRockRight();
        }
        else
        {
            RoteRockRight();
        }
    }

    void Rolling()
    {
        rock.position = new Vector3(Mathf.MoveTowards(rock.position.x, des, Time.deltaTime * SpeedRockRoll), rock.position.y);

        if (rock.position.x == des)
            des = (des == posB_roll.position.x) ? posA_roll.position.x : posB_roll.position.x;


    }

    void RoteRockLeft()
    {
        rock.eulerAngles = new Vector3(rock.eulerAngles.x, rock.eulerAngles.y, rock.eulerAngles.z + Time.deltaTime * 500);
    }

    void RoteRockRight()
    {
        rock.eulerAngles = new Vector3(rock.eulerAngles.x, rock.eulerAngles.y, rock.eulerAngles.z - Time.deltaTime * 500);
    }

    void BearDisappear()
    {
        bear.position = Vector3.MoveTowards(bear.position, pos_bear_disappear.position, Time.deltaTime * 2.5f);
        if (bear.position.x == pos_bear_disappear.position.x)
            bear.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            state = State.RunToRock;
            GetComponent<CircleCollider2D>().enabled = false;
        }

    }
}
