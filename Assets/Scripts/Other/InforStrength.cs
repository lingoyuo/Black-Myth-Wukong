using UnityEngine;
using System.Collections;

public class InforStrength : MonoBehaviour {

    public enum TypeInforStrength
    {
        NormalEnemy,
        Boss,
        SteelHealth,
        Bullet,
        Player,
        OneHit
    }


    [Space(20)]
    [Header("Infor: ")]
    public float MaxHealth;
    public float AttackSpeed;
    public float currentHealth;
    public int Damage;
    public float timerPerHit = 0.5f;
    public TypeInforStrength type = TypeInforStrength.NormalEnemy;

    [Space(20)]
    [Header("Health bar eneymy: ")]
    public GameObject health_bar;

    [Space(20)]
    [Header("Optional player avatar: ")]
    public GameObject avatar_get_hit;
    public AudioSource sound_get_hit;

    public float Get_Health { get { return currentHealth; } }
    public float Set_Health { set { currentHealth = value; } }

    public bool shield_active = false;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = MaxHealth;
    }

    void Update()
    {
        // active health when first hit
        if (type == TypeInforStrength.NormalEnemy || type == TypeInforStrength.SteelHealth)
            if (currentHealth < MaxHealth && currentHealth > 0)
            {
                if (!health_bar.activeSelf)
                {
                    health_bar.gameObject.SetActive(true);
                    health_bar.GetComponent<HealthBarEnemy>().Intial();
                }
            }

        if (currentHealth <= 0)
            if (type == TypeInforStrength.Boss)
                GetComponent<PolygonCollider2D>().enabled = false;
            else if (type == TypeInforStrength.NormalEnemy || type == TypeInforStrength.SteelHealth)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(this);
            }
            else if (type == TypeInforStrength.Player)
            {
                if (!GetComponent<PlayerController>().playerDead)
                    GetComponent<PlayerController>().PlayerDead();          // call to player dead in player controller when player not dead
                avatar_get_hit.SetActive(true);                             // active dead avatar
            }
    }

    public void InitialHealth()
    {
        currentHealth = MaxHealth;
    }

    // Call lose health when any object
    public void LoseHealth(float num)
    {      
        if (!shield_active)
        {
            if(currentHealth > 0)
                currentHealth = currentHealth - num;

            if (currentHealth < 0)
                currentHealth = 0;

            //call anima get hit when lose health
            if (anim)
            {
                anim.SetTrigger("hit");
                if (type == TypeInforStrength.Player)
                {
                    if (currentHealth > 0)
                        if(!sound_get_hit.isPlaying)
                            AudioManager.Instances.PlayAudioEffect(sound_get_hit);
                    StartCoroutine(GetHitAvatar());
                }
            }
        }
    }

    public void AddHealth(int num)
    {
        currentHealth = currentHealth + num;
    }

    IEnumerator GetHitAvatar()
    {
        if (!avatar_get_hit.activeSelf)
        {
            avatar_get_hit.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            avatar_get_hit.SetActive(false);
        }
    }
}
