 using UnityEngine;
using System.Collections;

public class BossPigControl : MonoBehaviour {
    public float speed;
    public Transform checkL;
    public Transform checkR;
    public bool flip = false;
    public PlatformBossPig platform1;
    public PlatformBossPig platform2;
    public ParticleSystem stunParticle;
    public ParticleSystem bossParticle;
    public SpriteRenderer matSprite;
    public GameObject da1;
    public GameObject da2;
    public GameObject da3;
    InforStrength healthBoss;
    public GameObject player;
    int randomT = 3;
    Animator animatorBoss;
    bool check = true;
	// Use this for initialization
	void Awake() {
        healthBoss = GetComponent<InforStrength>();
        animatorBoss = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (check)
        {
            if (player.transform.position.x > -2)
            {
                Invoke("BossAttack", 1);
                check = false;
            }
        }
        else
        {
            if (transform.position.x > checkR.position.x)
            {
                CheckDange();
            }
            else if (transform.position.x < checkL.position.x)
            {
                CheckDange();
            }
            if (animatorBoss.GetBool("run"))
            {
                BossRun();
            }
            if (flip)
            {
                Flip();
            }
        }
    }

    void BossRun ()
    {   
        if (!animatorBoss.GetBool("run"))
        {
            animatorBoss.SetBool("attack", false);
            animatorBoss.SetBool("dange", false);
            animatorBoss.SetBool("run", true);
            bossParticle.Stop();
        }
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    void Flip ()
    {
        randomT -= 1;
        speed = -speed;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
        flip = false;
        platform1.gameObject.SetActive(true);
        platform1.SetPlatform();
        platform2.gameObject.SetActive(true);
        platform2.SetPlatform();
    }

    void BossDange ()
    {
        if (!animatorBoss.GetBool("dange"))
        {
            healthBoss.LoseHealth(1);
            SetDa(da1.transform.GetChild(0).gameObject,player.transform.position.x);
            if (healthBoss.Get_Health <= 2)
            {
                if (healthBoss.Get_Health == 2)
                {
                    matSprite.color = Color.red;
                    speed += Mathf.Sign(speed) * 1f;
                }
                Invoke("SetDa2", 0.6f);
                
            }
            stunParticle.Play();
            animatorBoss.SetBool("attack", false);
            animatorBoss.SetBool("dange", true);
            animatorBoss.SetBool("run", false);
            if (healthBoss.Get_Health > 0)
                Invoke("BossAttack", 1);
        }
    }

    void CheckDange()
    {
        if (randomT > 0)
        {
            if (!animatorBoss.GetBool("attack"))
            { 
                
                if (transform.position.x > checkR.position.x && transform.localScale.x > 0)
                {
                    transform.position = new Vector3(checkR.position.x, transform.position.y, transform.position.z);
                    flip = true;
                }
                else if (transform.position.x < checkL.position.x && transform.localScale.x < 0)
                {
                    transform.position = new Vector3(checkL.position.x, transform.position.y, transform.position.z);
                    flip = true;
                }
            }
        }
        else
        {
            if (transform.position.x < checkR.position.x + 0.4f && transform.position.x > checkL.position.x - 0.4f)
            {
                if (healthBoss.Get_Health > 0)
                    BossRun();
                else
                    BossDie();
            }
            else
            {
                if (healthBoss.Get_Health > 0)
                {
                    BossDange();
                }
                else
                {
                    BossDie();
                }
            }
        }   
    }

    void BossAttack()
    {
        if (!animatorBoss.GetBool("attack"))
        {
            
            bossParticle.Play();
            Flip();
            animatorBoss.SetBool("attack", true);
            animatorBoss.SetBool("dange", false);
            animatorBoss.SetBool("run", false);
            if (transform.position.x > 0)
            {
                bossParticle.gameObject.transform.localEulerAngles = new Vector3(180, 90, 0);
                transform.position = transform.position = new Vector3(checkR.position.x, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < 0)
            {
                bossParticle.gameObject.transform.localEulerAngles = new Vector3(0, 90, 0);
                transform.position = transform.position = new Vector3(checkL.position.x, transform.position.y, transform.position.z);
            }
            
            Invoke("BossRun", 2);
            randomT = Random.Range(2, 6);
        }
    }

    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Platform")
            coll.gameObject.GetComponent<PlatformBossPig>().DesTroyPlatform();

    }

    void SetDa (GameObject da, float x)
    {
        da.SetActive(true);
        da.transform.position = new Vector3(x, 7, da.transform.position.z);
    }

    void SetDa2()
    {
        SetDa(da2.transform.GetChild(0).gameObject, Random.Range(checkL.transform.position.x, checkR.transform.position.x));
        if (healthBoss.Get_Health == 0)
            Invoke("SetDa3", 0.8f);
    }
    void SetDa3()
    {
        SetDa(da3.transform.GetChild(0).gameObject, Random.Range(checkL.transform.position.x, checkR.transform.position.x));
        Invoke("SetDa2", 1f);
    }

    void BossDie()
    {
        
        if (!animatorBoss.GetBool("die"))
        {
            animatorBoss.SetBool("attack", false);
            animatorBoss.SetBool("dange", false);
            animatorBoss.SetBool("run", false);
            animatorBoss.SetBool("die", true);
            
        }
    }
}
