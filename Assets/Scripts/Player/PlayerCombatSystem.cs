using UnityEngine;
using System.Collections;

public class PlayerCombatSystem : MonoBehaviour {
    public bool attacking;
    public BumerangProjectile bumerang;
    public AttackSkillManager skillManager;

    public bool Debug = true;

    [Space(20)]
    [Header("Items skill")]
    public GameObject bumerangSlot;
    public GameObject boomSlot;
    public GameObject rockSlot;
    public AudioSource audio_attack;

    private Animator anim;
    private SMB_Attack smb_attack;
   
    private InforStrength strenght;
    
    private PoolManager boomItems;
    private PoolManager rockItems;

    float timer_normal_attack;                                          // Use for bumerang and stick
    float timer_attack_skill;                                           // Use for boom, rock

    bool release;                                                       // Use for UI touch

    bool input_attack;

    public static PlayerCombatSystem Instances { get; private set; }

    void Awake()
    {
        Instances = this;
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        smb_attack = anim.GetBehaviour<SMB_Attack>();

        smb_attack.audio_attack = audio_attack;

        strenght = GetComponent<InforStrength>();
        release = true;

        boomItems = GameObject.FindGameObjectWithTag("PoolManager").transform.GetChild(0).GetComponent<PoolManager>();
        rockItems = GameObject.FindGameObjectWithTag("PoolManager").transform.GetChild(1).GetComponent<PoolManager>();
    }

    void Update()
    {
        if(timer_normal_attack < 10)
            timer_normal_attack += Time.deltaTime;

        // check attack skill
        if (skillManager.Get_currentSkill.Get_TypeItem == ItemPlayer.TypeItem.Attack && skillManager.Get_currentSkill.Get_Name != LocalAccessValue.bumerang)
            timer_attack_skill += Time.deltaTime;
        else
            timer_attack_skill = 0;

        if (Debug)
            GetInputKeyBoard();

        // Get input from use and time can attack
        if (input_attack && skillManager.Get_currentSkill.Get_TypeItem == ItemPlayer.TypeItem.Attack)
        {            
            if (skillManager.Get_currentSkill.Get_TypeItem == ItemPlayer.TypeItem.Attack)
            {
                // if have items, it will have animation and sapwn
                if (skillManager.Get_currentSkill.Get_AmountSkill > 0)
                    if (skillManager.Get_currentSkill.Get_Name != LocalAccessValue.bumerang)
                        anim.SetTrigger("attack_skill");

               
                // If boom item and rock item, spawn item in fiexed time 
                // If bumerang will spawn only bumerang deactive
                if (skillManager.Get_currentSkill.Get_Name != LocalAccessValue.bumerang)
                {
                    if (timer_attack_skill > skillManager.Get_currentSkill.Get_CountDown && skillManager.Get_currentSkill.Get_AmountSkill > 0)
                    {
                        if (skillManager.Get_currentSkill.Get_Name == LocalAccessValue.boom)
                        {
                            var boom = boomItems.GetObjPool(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.6f));
                            if (!boom)
                                boom = boomItems.RequestObjPool(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.6f));
                            boom.GetComponent<BoomProjectile>().BomAttack(gameObject);
                        }
                        else if (skillManager.Get_currentSkill.Get_Name == LocalAccessValue.rock)
                        {
                            var rock = rockItems.GetObjPool(new Vector2(transform.position.x, transform.position.y + 0.6f));
                            if (!rock)
                                rock = rockItems.RequestObjPool(new Vector2(transform.position.x, transform.position.y + 0.6f));
                            rock.GetComponent<RockProjectile>().StartRock(gameObject);
                        }

                        // Decrease number current skill
						AudioManager.Instances.PlayAudioEffect(audio_attack);
                        skillManager.DecreaseNumberCurrentSkill();
                        timer_attack_skill = 0.0f;
                    }
                }
                else if (skillManager.Get_currentSkill.Get_AmountSkill > 0)
                {
                    if (!bumerang.gameObject.activeSelf)
                    {
                        anim.SetTrigger("attack_skill");
                        bumerang.gameObject.SetActive(true);
                        bumerang.StartBum(this.gameObject.transform);

                        // Decrease number current skill
						AudioManager.Instances.PlayAudioEffect(audio_attack);
                        skillManager.DecreaseNumberCurrentSkill();
                    }
                }

                // Check skill in UI to change
                skillManager.CheckSkill();
            }
        }else if(input_attack && skillManager.Get_currentSkill.Get_Name == "STICK")
        {          
            if(timer_normal_attack > strenght.AttackSpeed)
            {
                timer_normal_attack = 0;
                anim.SetTrigger("attack");
            }
        }
    }

    void GetInputKeyBoard()
    {

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.L))
            input_attack = true;
        else
            input_attack = false;
#endif
    }

    void LateUpdate()
    {
        input_attack = false;
    }



    // Change current skill and change item slot in player
    public void ChangeItemsSlot(string name)
    {
        // set active slot
        switch (name)
        {
            case LocalAccessValue.bumerang:
                bumerangSlot.SetActive(true);
                boomSlot.SetActive(false);
                rockSlot.SetActive(false);
                break;
            case LocalAccessValue.rock:
                bumerangSlot.SetActive(false);
                boomSlot.SetActive(false);
                rockSlot.SetActive(true);
                break;
            case LocalAccessValue.boom:
                bumerangSlot.SetActive(false);
                boomSlot.SetActive(true);
                rockSlot.SetActive(false);
                break;
        }
    }

    /*
    ** Moblie mode
    */
    public void Attack(UnityEngine.EventSystems.BaseEventData data)
    {
        //if (timer_normal_attack > strenght.AttackSpeed && release)
        //{
        //    anim.SetTrigger("attack");

        //    timer_normal_attack = 0;

        //    release = false;
        //}

        if (release)
        {
            input_attack = true;
            release = false;
        }
    }

    public void AttackRelease(UnityEngine.EventSystems.BaseEventData data)
    {
        release = true;

        input_attack = false;
    }

    public void ManualAttack()
    {
        if (release)
        {
            input_attack = true;
            release = false;
        }
    }

    public void StopManualAttack()
    {
        release = true;

        input_attack = false;
    }

}
