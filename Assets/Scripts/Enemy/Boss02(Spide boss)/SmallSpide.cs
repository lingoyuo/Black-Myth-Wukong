using UnityEngine;
using System.Collections;

public class SmallSpide : MonoBehaviour {

    public enum State
    {
        None,
        Droping,
        Fluctuating,
        Dead
    }

    public float speedDropSilk = 5.0f;
    public State setState { set { curState = value; } }

    [Space(10)]
    [Header("Postion line render")]
    public Transform silkPos01;
    public Transform silkPos02;

    //[HideInInspector]
    public State curState;
    private Transform smallSpide;
    private SpringJoint2D spring_spide;

    private ControlLeafSpawnParticle leafSpawn;

    private float distanceSpawn;

    private Animator anim;
    private float des;

    private LineRenderer silkLine;

	private CircleCollider2D box_damage_player;

    /*
    const float limit_max_y = 1.87f;
    const float limit_min_y = -1.32f;
    */

    private float limit_max_distance;
    private float limit_min_distance;


    void Awake()
    {
        smallSpide = transform.GetChild(0);
        spring_spide = smallSpide.GetComponent<SpringJoint2D>();
        leafSpawn = GetComponentInChildren<ControlLeafSpawnParticle>();
        anim = GetComponentInChildren<Animator>();
		box_damage_player = smallSpide.GetComponent<CircleCollider2D> ();

        silkLine = silkPos02.GetComponent<LineRenderer>();


        // Intial
        spring_spide.distance = 0.0f;
        spring_spide.connectedAnchor = transform.position;
        smallSpide.localPosition = Vector2.zero;
        curState = State.None;
        silkLine.enabled = false;

        DropLeaf();
       // CallDrop(7.29f, 5.05f);
    }

    void Update()
    {
        switch (curState)
        {
			case State.None:
				box_damage_player.enabled = false;
				break;
            case State.Droping:
				box_damage_player.enabled = true;
                silkLine.enabled = true;
                DropSilk();
                break;
            case State.Fluctuating:
                Fluctation();
                break;
            case State.Dead:
                silkLine.enabled = false;
                Dead();
                break;
        }

        Silk();

    }

    // drop in random range
    public void CallDrop(float limit_max,float limit_min)
    {
        curState = State.Droping;

        limit_max_distance = limit_max;
        limit_min_distance = limit_min;

        distanceSpawn = Random.Range(limit_max_distance, limit_min_distance);
    }

    void DropSilk()
    {
        spring_spide.distance += Time.deltaTime * speedDropSilk;

        smallSpide.transform.Translate(-Vector2.up * Time.deltaTime);                              // fix bug: not active spide move

        if(spring_spide.distance > distanceSpawn)
        {
            des = distanceSpawn;
            curState = State.Fluctuating;
        }
    }

    void Fluctation()
    {
        spring_spide.distance = Mathf.MoveTowards(spring_spide.distance, des, Time.deltaTime);
      
        if (Mathf.Abs(spring_spide.distance - des) < 0.1f)
            FindNextPoint();    
    }

    void FindNextPoint()
    {
        
        des = (des == distanceSpawn) ? limit_min_distance : distanceSpawn;

    }

    public void Deactive()
    {
        GetComponentInChildren<InforStrength>().MaxHealth = 1;
        GetComponentInChildren<InforStrength>().InitialHealth();
        curState = State.None;
        silkLine.enabled = false;
        spring_spide.distance = 0.0f;
        spring_spide.connectedAnchor = transform.position;
        smallSpide.localPosition = Vector2.zero;
        anim.SetBool("die", false);
       // gameObject.SetActive(false);        
    }

    public void Dead()
    {
        silkLine.enabled = false;
        spring_spide.distance += Time.deltaTime * 5.0f;
    }

    public void DropLeaf()
    {
        leafSpawn.SpawnLeaf();
    }

    void Silk()
    {
        silkLine.SetPosition(0, silkPos02.position);
        silkLine.SetPosition(1, silkPos01.position);
    }
}
