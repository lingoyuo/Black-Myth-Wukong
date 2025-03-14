using UnityEngine;
using System.Collections;

public class FallInAirDamagePlayer : MonoBehaviour {

    public ParticleSystem effect_jump_hit_ground;
    float SafeHeight = 8.0f;
    float MaxHeight = 16.0f;

    PlayerController player;
    AudioSource audio_source;
    AudioManager audio_manager;

    bool jump = false;
    bool hitGround = false;
    bool airing = false;

    float posJumpHeightest;
    float posHitGround;
    float MaxHealth;
    float fallHeight;

    void Start()
    {
        audio_source = GetComponent<AudioSource>();
        player = GetComponent<PlayerController>();
        MaxHealth = GetComponent<InforStrength>().Get_Health;
    }

    void FixedUpdate()
    {
        // player hit jump from keyboard
        if (!player.grounded && !jump)
        {
            jump = true;                                                // check jump hit
            hitGround = false;                                          // not in ground
            posJumpHeightest = transform.position.y;                    // get postion
            airing = true;                                              // check in air
        }

        // player gain heightest
        if(!player.grounded && jump)
        {
            if (posJumpHeightest < transform.position.y)
                posJumpHeightest = transform.position.y;                // update hieghest postion player when jump
        }

        // player hit ground
        if (player.grounded && !hitGround && jump)
        {
            posHitGround = transform.position.y;                        // get postion hit ground
            jump = false;                                               // reset check jump keyboard false
            hitGround = true;                                           // check hit ground
            airing = false;                                             // airing is false     

            // if audio not play, play it
            if (!audio_source.isPlaying && player.enviroment != PlayerController.Enviroment.Water)
            {

				AudioManager.Instances.PlayAudioEffect(audio_source);
            }

            if (!effect_jump_hit_ground.isPlaying) 
                effect_jump_hit_ground.Play();
        }

        fallHeight = posJumpHeightest - posHitGround;                   

        if (player.enviroment == PlayerController.Enviroment.Water)
        {
            posJumpHeightest = 0;
            posHitGround = 0;
            jump = false;
        }
       
        if (fallHeight > SafeHeight && !airing)
        {
            posJumpHeightest = 0;
            posHitGround = 0;

            if (player.enviroment != PlayerController.Enviroment.Water)
                GetComponent<InforStrength>().LoseHealth(DamagePlayer());

           // anim.SetTrigger("hit");
        }
    }

    int DamagePlayer()
    {
        float proportion = (fallHeight - SafeHeight) / (MaxHeight - SafeHeight);

        float healthLost = MaxHealth * proportion;

        return (int)healthLost;
    }

    /*
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - SafeHeight));
        Debug.DrawLine(new Vector2( transform.position.x,transform.position.y - SafeHeight),new Vector2(transform.position.x, transform.position.y - MaxHeight), Color.red);
    }*/
}
