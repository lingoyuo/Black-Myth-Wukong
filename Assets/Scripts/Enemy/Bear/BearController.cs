using UnityEngine;
using System.Collections;

public class BearController : AdvanceFSMState
{
    public Transform[] wayPoints;

    public ParticleSystem particleDead;
    public GameObject boxDeactive;

    public float SpeedChase = 1.0f;

    [HideInInspector]
    public GameObject obj_damage;

    [HideInInspector]
    public float timer;
    public bool get_hit;

    private InforStrength healthBear;
    private Transform player;
    private Animator anim;

    private BearDetectPlayer sensor_detect_player;
    private BearDetectPlayer sensor_detect_attack_player;

    private SensorDetectEnviromentBear sensor_enviroment;

    
    private float direct;
    private Vector2 posPush;

    private float first_step;

    void Awake()
    {
    
    }

    protected override void Initialize()
    {
        anim = GetComponent<Animator>();
        sensor_enviroment = GetComponent<SensorDetectEnviromentBear>();
        healthBear = GetComponent<InforStrength>();

        GetSensor();

        ContructFSM();

    }

    protected override void FSMUpdate()
    {
        timer += Time.deltaTime;

        if (healthBear.Get_Health <= 0)
        {
            anim.SetTrigger("dead");
            return;
        }

        if (anim.GetCurrentAnimatorStateInfo(1).IsName("not_get_hit"))
        {
            get_hit = false;
        }
        if (!get_hit)
        {
            CurrentState.Reason(gameObject.transform);
            CurrentState.Act(gameObject.transform);
        }
        else
            transform.position = Vector2.Lerp(transform.position, posPush, Time.deltaTime * 10);

    }

    protected override void FSMFixedUpdate()
    {
    }

    public void SetTransition(Transition t)
    {
        PerformTransition(t);
    }

    private void ContructFSM()
    {
        // Patrol state
        BearPatrolState patrol = new BearPatrolState(wayPoints,anim,sensor_detect_player,sensor_enviroment);
        patrol.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        patrol.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        patrol.AddTransition(Transition.ReachPatrolPoints, FSMStateID.Idling);
        patrol.AddTransition(Transition.ReachPlayer, FSMStateID.Attacking);

        // Idle State
        BearIdleState idle = new BearIdleState(anim,sensor_detect_player);
        idle.AddTransition(Transition.PatrolContinue, FSMStateID.Patrolling);
        idle.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        idle.AddTransition(Transition.ReachPlayer, FSMStateID.Attacking);
        idle.AddTransition(Transition.NoHealth, FSMStateID.Dead);

        // Chase State
        BearChaseState chase = new BearChaseState(anim, sensor_detect_player,sensor_detect_attack_player,sensor_enviroment,SpeedChase);
        chase.AddTransition(Transition.ReachPlayer, FSMStateID.Attacking);
        chase.AddTransition(Transition.LostPlayer, FSMStateID.Patrolling);
        idle.AddTransition(Transition.NoHealth, FSMStateID.Dead);

        // Attack State
        BearAttackState attack = new BearAttackState(anim, sensor_detect_attack_player);
        attack.AddTransition(Transition.LostPlayer, FSMStateID.Chasing);
        attack.AddTransition(Transition.NoHealth, FSMStateID.Dead);

        AddFSMState(patrol);
        AddFSMState(idle);
        AddFSMState(attack);
        AddFSMState(chase);
    }

    private void GetSensor()
    {
        sensor_detect_player = transform.GetChild(0).GetComponent<BearDetectPlayer>();
        sensor_detect_attack_player = transform.GetChild(1).GetComponent<BearDetectPlayer>();
    }

    public void GetHit()
    {
        if (!obj_damage)
        {
            if (!player)
                player = GameObject.FindGameObjectWithTag("Player").transform;
            direct = Mathf.Sign(player.transform.position.x - transform.position.x);

        }
        else
        {
            direct = -Mathf.Sign(obj_damage.transform.position.x - transform.position.x);
        }

        get_hit = true;

        if (direct < 0)
        {
            if ((transform.localScale.x > 0 && sensor_enviroment.obstacle_front) ||
                (transform.localScale.x < 0 && sensor_enviroment.obstacle_behind))
            {
                posPush = new Vector2(transform.position.x, transform.position.y);
                return;
            }
        }
        else
        {
            if ((transform.localScale.x > 0 && sensor_enviroment.obstacle_behind) ||
               (transform.localScale.x < 0 && sensor_enviroment.obstacle_front))
            {
                posPush = new Vector2(transform.position.x, transform.position.y);
                return;
            }
        }
        posPush = new Vector2(transform.position.x - direct/2, transform.position.y);

        obj_damage = null;
    }

    public void Attack()
    {
        transform.GetComponentInChildren<SwordBear>().GetComponent<BoxCollider2D>().enabled = true;
    }

    public void NotAttack()
    {
        if(transform.GetComponentInChildren<SwordBear>())
            transform.GetComponentInChildren<SwordBear>().GetComponent<BoxCollider2D>().enabled = false;
    }

    // When animation dead finish call it
    // Active particle
    public void DeactiveBear()
    {
        particleDead.gameObject.SetActive(true);
        particleDead.gameObject.transform.SetParent(null);
        particleDead.Play();
        gameObject.SetActive(false);
    }

}
