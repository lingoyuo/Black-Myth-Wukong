using UnityEngine;
using System.Collections;

public class PlayerDamageEnemy : MonoBehaviour {

    // this script help player attack enemy and player can not get damage when attack

    const float TIME_OFFSET_CHECK = 0.0f;
    const float TIME_APPEAR_HEALTH_BAR = 0.1f;

    public float TimeRecover = 1.0f;

    public float timeProtect = 0.01f;

    public bool getHit;

    InforStrength Strength;

    float timer;
    float timeForOffset;

    bool canAttack;

    void Start()
    {
        Strength = GetComponent<InforStrength>();
        timeProtect = TIME_APPEAR_HEALTH_BAR;
        canAttack = true;
    }

    void Update()
    {
        if( timeProtect < 10)
              timeProtect += Time.deltaTime;

        if(timer < 10)
              timer += Time.deltaTime;
        
        
        if (timeProtect < 0.05)
            getHit = true;
        else
            getHit = false;
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && timeProtect > TimeRecover && Strength.type == InforStrength.TypeInforStrength.NormalEnemy)
        {
            var player = other.gameObject.GetComponent<InforStrength>();
            var playerAttack = other.gameObject.GetComponent<PlayerCombatSystem>();

            if (!playerAttack.attacking)
            {
                player.LoseHealth(1);
            }

            if (!player.GetComponent<InforStrength>().shield_active)
                other.gameObject.GetComponent<PlayerController>().PlayerGetHitControlPhysics(gameObject.transform.position);
        }

    }

    // Apply with type enemy steel health and boss
    void OnTriggerStay2D(Collider2D other)
    {
        timeForOffset += Time.deltaTime;

        if (other.tag == "Player" && timeProtect > TimeRecover)
        {
            // Check for steel health enemy
            if ( Strength.type == InforStrength.TypeInforStrength.SteelHealth)
            {
                var player = other.gameObject.GetComponent<InforStrength>();
                var playerAttack = other.gameObject.GetComponent<PlayerCombatSystem>();

                if (!playerAttack.attacking && timer > Strength.timerPerHit)
                {
                    player.LoseHealth(Strength.Damage);
                    timer = 0;
                }
            }

            // Check for boss
            else if ( Strength.type == InforStrength.TypeInforStrength.Boss && canAttack && timeForOffset > TIME_OFFSET_CHECK)
            {
                var player = other.gameObject.GetComponent<InforStrength>();
                var playerAttack = other.gameObject.GetComponent<PlayerCombatSystem>();

                if (!playerAttack.attacking)
                {
                    player.LoseHealth(Strength.Damage);
                    timer = 0;
                }

                canAttack = false;

                timeForOffset = 0;
            }
        }
    }
   
    void OnTriggerExit2D(Collider2D other)
    {
        canAttack = true;

        timeForOffset = 0;
    }
}
