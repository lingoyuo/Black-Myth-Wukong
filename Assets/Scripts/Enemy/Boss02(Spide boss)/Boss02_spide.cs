using UnityEngine;
using System.Collections;

public class Boss02_spide : MonoBehaviour {

    /// <summary>
    /// State boss:
    ///     idle: first state boss not see player, boss idling
    ///     spawnSilk: when boss saw player, we need spawn silk to tail boss
    ///     pullSilk: boss pull silk then boss disappear tree
    ///     caculateSpawn: logic boss active, boss nee decide use skill or normal attack . In current state, need use random to spawn other small spide and spawn to suitable position
    ///     dropSilk: after caculate logic boss in state caculateSpawn, boss drop to postion with skill or other small spide
    ///     attack: after drop suitable postion, boss will have action to harm player
    ///     dead: boss lose all health ==> level finish
    /// </summary>
    public enum State
    {
        idle,
        spawnSilk,
        pullSilk,
        caculateSpawn,
        dropSilk,     
        attack,
        dead
    }

    /// <summary>
    /// State skil boss:
    ///     None: No skill use, this only normal attack
    ///     Normal: Boss use area skill, shoot all bullet to random postion
    ///     Special skil: Boss use skill spawn bullet with static delay time, bullet find player to reach when start spawn.
    /// </summary>
    public enum StateSkill
    {
        None,
        Normal,
        Special
    }

    [Space(10)]
    [Header("Player setting for spide boss")]
    public float distanceDetectPlayer = 9.0f;
    public Transform player;

    [Space(10)]
    [Header("Setting sensor and pos move spide boss")]
    public Transform silkPos01;
    public Transform silkPos02;
    public Transform rayFromGround;

    public ControlLeafSpawnParticle leafSpawnSignal;
    public ControlLeafSpawnParticle leafSpawnHitGrass;

    public float speed_drop_silk = 5.0f;

    [Space(10)]
    [Header("Object pool")]
    public PoolManager bullets;
    public PoolManager effects;
    public PoolManager cobweb;

    [Space(10)]
    [Header("Pos spawn bullet skill")]
    public Transform posSpawnBullet;
    public Transform[] posSpawnBulletSkills;
    public GameObject effectSkill;


   [Space(10)]
   [Header("Spide boss")]
    public GameObject small_spide;

    public State setState { set { cur_state_boss = value; } }
    public float Time_wait_attack = 2.0f;

    public StateSkill SetStateSkill { set { stateSkill = value; } }

    public PolygonCollider2D[] boxDamagePlayers;
    public State GetStateBoss { get { return cur_state_boss; } }

    private SpringJoint2D spring;
    private State cur_state_boss;
    private Transform spideBoss;
    private Animator anim_boss;
    private LineRenderer silkLine;
    private float timer;
    private RaycastHit2D rayHit;



    private int numberSpawnSPideEachTurn;
    private GameObject[] list_spide;

    [HideInInspector]
    public StateSkill stateSkill;

    private ParticleSystem lightEffect;
    private ParticleSystem circleEffect;
    private ParticleSystem starEffect;

    private bool spawnSkill;
    private bool deactiveSkill;

    // Constant value setting for boss drop suitable postion
    private const float LIMIT_RIGHT = -9.0f;                        // limit right screen
    private const float LIMIT_LEFT = -20.0f;                        // limit left screen
    private const float POS_DROP_BOSS = 6.0f;                       // limit y axis when boss drop in none skill
    private const float POS_DROP_BOSS_SPAWN_SKILL = 5.5f;           // limit y axis when boss drop in use skill

    // Constant value setting for small spide drop suitable postion
    private const float LIMIT_RIGHT_SMALL_SPIDE = -7.0f;                        // limit right screen
    private const float LIMIT_LEFT_SMALL_SPIDE = 7.0f;                        // limit left screen
    private const float POS_DROP__SMALL_SPIDE = 6.8f;                       // limit y axis when small spide drop


    void Start()
    {
        spideBoss = transform.GetChild(0);
        anim_boss = spideBoss.GetComponent<Animator>();
        spring = spideBoss.GetComponent<SpringJoint2D>();
        cur_state_boss = State.idle;


        // Check bug
        if (!player)
            Debug.LogError("Please add player to boss!!");

        silkLine = silkPos02.GetComponent<LineRenderer>();

        spring.connectedAnchor = transform.position;
        list_spide = new GameObject[5];

        // Instate spide and cache in array list_spide
        for (int i = 0; i < 5; i++)
            list_spide[i] = (GameObject)Instantiate(small_spide, new Vector2(Random.Range(LIMIT_LEFT_SMALL_SPIDE, LIMIT_RIGHT_SMALL_SPIDE), POS_DROP__SMALL_SPIDE), Quaternion.identity);

        // Set first state skill poss
        stateSkill = StateSkill.None;

        // Settup effect skill
        lightEffect = effectSkill.GetComponent<ParticleSystem>();
        circleEffect = effectSkill.transform.GetChild(0).GetComponent<ParticleSystem>();
        starEffect = effectSkill.transform.GetChild(1).GetComponent<ParticleSystem>();

        lightEffect.startSize = 0.0f;
        circleEffect.startSize = 0.0f;
        starEffect.maxParticles = 0;
    }


    void Update()
    {
        CheckDead();

        switch (cur_state_boss)
        {
            case State.idle:
                Idle();
                break;
            case State.spawnSilk:
                SpawnSpilk();
                break;
            case State.pullSilk:
                Silk();
                PullSilk();
                RaycastDetect();
                break;
            case State.caculateSpawn:
                Silk();
                CaculateSpawn();
                break;
            case State.dropSilk:
                Silk();
                DropSilk();
                break;
            case State.attack:
                Silk();
                Attack();
                break;
            case State.dead:
                Dead();
                break;               
        }
    }

    // Idle state: boss find player postion to decide active
    void Idle()
    {
        if(Mathf.Abs(spideBoss.position.x - player.position.x) < distanceDetectPlayer)
        {
            anim_boss.SetTrigger("move");
            timer += Time.deltaTime;
            if(timer > anim_boss.GetCurrentAnimatorStateInfo(0).length)
            {
                timer = 0;
                cur_state_boss = State.spawnSilk;
            }
        }
    }


    // State: Caculate postion sapwn boss, postion spawn spide boss
    void CaculateSpawn()
    {
        if(timer == 0)
        {
            float rand = Random.Range(LIMIT_LEFT, LIMIT_RIGHT);
            transform.localPosition = new Vector2(rand, transform.localPosition.y);
            spring.connectedAnchor = transform.position;
            numberSpawnSPideEachTurn = Random.Range(0, 5);

            for (int i = 0; i < 5; i++)
            {
                list_spide[i].transform.position = new Vector2(Random.Range(LIMIT_LEFT_SMALL_SPIDE, LIMIT_RIGHT_SMALL_SPIDE), POS_DROP__SMALL_SPIDE);
                list_spide[i].GetComponent<SmallSpide>().Deactive();
            }

        }

        timer += Time.deltaTime;
        anim_boss.SetTrigger("free");


        // If no skill use for this attack, we will spawn small spide

        // Spawn Leaf
        if (timer > 2.0f )
        {
            leafSpawnSignal.SpawnLeaf();

            // Check
            if (stateSkill == StateSkill.None)
                for (int i = 0; i < numberSpawnSPideEachTurn; i++)
                    list_spide[i].GetComponent<SmallSpide>().DropLeaf();
        }

        // Spawn small spide
        if (timer > 3.0f)
        {
            // Check
            if (stateSkill == StateSkill.None)
                for (int i = 0; i < numberSpawnSPideEachTurn; i++)
                    list_spide[i].GetComponent<SmallSpide>().CallDrop(7.29f, 5.05f);

            // Change state
            cur_state_boss = State.dropSilk;

            timer = 0;
        }
        
    }

    // State: Enemy use skill or spawn enemy
    void Attack()
    {
        if (stateSkill == StateSkill.None)
        {
            timer += Time.deltaTime;

            if (timer > 2.0f)
            {
                var bullet = (GameObject)bullets.GetObjPool(posSpawnBullet.position);

                if (bullet)
                    SettupBullet(bullet, new Vector2(player.transform.position.x, -1.0f));
                else
                {
                    bullet = (GameObject)bullets.RequestObjPool(posSpawnBullet.position);
                    bullet.GetComponent<ParabolMove>().target = player.transform.position;
                }

                bullet.transform.GetChild(0).gameObject.SetActive(true);

                // Set bullet object with pool effect and pool cobweb
                bullet.GetComponent<DeactiveSelfBullet>().poolEffect = effects;
                bullet.GetComponent<DeactiveSelfBullet>().poolSpawnHarming = cobweb;

                timer = 0;
                cur_state_boss = State.pullSilk;
            }
        }
        else if (stateSkill == StateSkill.Normal)
        {
            lightEffect.startSize = Mathf.MoveTowards(lightEffect.startSize, 0.6f, Time.deltaTime);
            circleEffect.startSize = Mathf.MoveTowards(circleEffect.startSize, 4.88f, Time.deltaTime*2);
            starEffect.maxParticles = 55;

            if (circleEffect.startSize > 4.7f && !deactiveSkill)
            {
                if(!spawnSkill)
                    NormalSkill();

                deactiveSkill = true;
            }  
            
            if(deactiveSkill)
            {
                timer += Time.deltaTime;

                lightEffect.startSize = Mathf.MoveTowards(lightEffect.startSize, 0.0f, Time.deltaTime*3);
                circleEffect.startSize = Mathf.MoveTowards(circleEffect.startSize, 0.0f, Time.deltaTime * 4);
                starEffect.maxParticles = 10;

                if (timer > 1.5f)
                    ResetSkill();
            }        
        }
        else if (stateSkill == StateSkill.Special)
        {

            lightEffect.startSize = Mathf.MoveTowards(lightEffect.startSize, 0.6f, Time.deltaTime);
            circleEffect.startSize = Mathf.MoveTowards(circleEffect.startSize, 4.88f, Time.deltaTime * 2);
            starEffect.maxParticles = 55;


            if (circleEffect.startSize > 4.7f && !deactiveSkill)
            {
                if (!spawnSkill)
                    SpecialSkill();

                deactiveSkill = true;

                timer += Time.deltaTime;
            }

            if(deactiveSkill)
            {
                timer += Time.deltaTime;

                lightEffect.startSize = Mathf.MoveTowards(lightEffect.startSize, 0.0f, Time.deltaTime*3);
                circleEffect.startSize = Mathf.MoveTowards(circleEffect.startSize, 0.0f, Time.deltaTime * 4);
                starEffect.maxParticles = 10;

                if (timer > 3.0f)
                    ResetSkill();
            }
        }
    }

    // Use normal skill boss
    void NormalSkill()
    {
        spawnSkill = true;

        int direct = (player.position.x > transform.position.x) ? 1 : -1;

        for(int i=0;i<8;i++)
        {
            var bullet = (GameObject)bullets.GetObjPool(posSpawnBulletSkills[i].position);
            if (bullet)
                SettupBullet(bullet, new Vector2(posSpawnBulletSkills[i].position.x + i * direct * Random.Range(0.5f, 2.0f), 0.0f));
            else
            {
                bullet = (GameObject)bullets.RequestObjPool(posSpawnBulletSkills[i].position);
                bullet.GetComponent<ParabolMove>().target = new Vector2(posSpawnBulletSkills[i].position.x + i * direct * Random.Range(0.5f, 2.0f), 0.0f);
            }

            bullet.transform.GetChild(0).gameObject.SetActive(true);

            // Set bullet object with pool effect and pool cobweb
            bullet.GetComponent<DeactiveSelfBullet>().poolEffect = effects;
            bullet.GetComponent<DeactiveSelfBullet>().poolSpawnHarming = cobweb;

        }     
    }

    // Use special skill
    void SpecialSkill()
    {
        spawnSkill = true;
        StartCoroutine(SpawnSkillByTime());
    }
    IEnumerator SpawnSkillByTime()
    {

        int direct = (player.position.x > transform.position.x) ? 1 : -1;

        for (int i = 0; i < 8; i++)
        {
            var bullet = (GameObject)bullets.GetObjPool(posSpawnBulletSkills[i].position);
            if (bullet)
                SettupBullet(bullet, new Vector2(player.transform.position.x + direct * 1.5f, -1.0f));
            else
            {
                bullet = (GameObject)bullets.RequestObjPool(posSpawnBulletSkills[i].position);
                bullet.GetComponent<ParabolMove>().target = player.transform.position;
            }

            bullet.transform.GetChild(0).gameObject.SetActive(true);

            // Set bullet object with pool effect and pool cobweb
            bullet.GetComponent<DeactiveSelfBullet>().poolEffect = effects;
            bullet.GetComponent<DeactiveSelfBullet>().poolSpawnHarming = cobweb;

            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    // Dead state: Disable bos harm player and set animation to die
    void Dead()
    {
        anim_boss.SetTrigger("die");

        foreach (PolygonCollider2D box in boxDamagePlayers)
            box.enabled = false;
    }

    void SpawnSpilk()
    {
        timer += Time.deltaTime * 5;
        silkLine.SetPosition(0, silkPos02.position);

        if (silkPos02.position.y + timer < silkPos01.position.y)
            silkLine.SetPosition(1, new Vector2(silkPos02.position.x, silkPos02.position.y + timer));
        else
        {
            silkLine.SetPosition(1, silkPos01.position);
            timer = 0;
            cur_state_boss = State.pullSilk;
        }
    }

    void Silk()
    {
        silkLine.SetPosition(0, silkPos02.position);
        silkLine.SetPosition(1, silkPos01.position);
    }

    void PullSilk()
    {
        spring.distance -= Time.deltaTime * speed_drop_silk;

        for (int i = 0; i < numberSpawnSPideEachTurn; i++)
        {
            if (list_spide[i].GetComponent<SmallSpide>().curState != SmallSpide.State.None)
            {
                list_spide[i].GetComponentInChildren<Animator>().SetBool("die", true);
                list_spide[i].GetComponent<SmallSpide>().setState = SmallSpide.State.Dead;
            }
        }
        
        // Pull silk not smaller 1 to fix bug change direct spring unity( offset = 1)
        if (spring.distance < 1f)
            cur_state_boss = State.caculateSpawn;
    }

    void DropSilk()
    {
        spring.distance += Time.deltaTime * speed_drop_silk;

        // if use skill need change destination spawn, may be higher postion none skill that how to play can not attack
        float _destination =( stateSkill == StateSkill.None) ? POS_DROP_BOSS : POS_DROP_BOSS_SPAWN_SKILL;

        if(spring.distance > _destination)
            cur_state_boss = State.attack;
    }

    // Detect pos spawn leaf
    void RaycastDetect()
    {
        // Raycast only layer enemy layer = 11
        rayHit = Physics2D.Raycast(rayFromGround.position, Vector2.up, 0.75f, 1 << 11);

        if (rayHit.collider)
            if (rayHit.collider.gameObject.tag == "Platform")
                leafSpawnHitGrass.SpawnLeaf();

    }

    // Check when boss lose all health
    void CheckDead()
    {
        if (spideBoss.GetComponent<InforStrength>().Get_Health <= 0)
            cur_state_boss = State.dead;
    }

    // Settup bullet and target
    void SettupBullet(GameObject bullet, Vector3 target)
    {
        bullet.GetComponent<ParabolMove>().target = target;
        bullet.GetComponent<ParabolMove>().InitialPos();
        bullet.GetComponent<DeactiveSelfBullet>().ResetTimer();
      //  bullet.GetComponent<RotateDirection>().CheckDirection();
    }

    // Deactive skill
    void ResetSkill()
    {
        timer = 0;
        deactiveSkill = false;
        lightEffect.startSize = 0.0f;
        circleEffect.startSize = 0.0f;
        starEffect.maxParticles = 0;
        spawnSkill = false;
        cur_state_boss = State.pullSilk;
        stateSkill = StateSkill.None;
    }
}
