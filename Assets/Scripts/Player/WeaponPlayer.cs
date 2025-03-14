using UnityEngine;
using System.Collections;

public class WeaponPlayer : MonoBehaviour {

    public ParticleSystem particle;
    public AudioSource audio_source;

    AudioManager audio_manager;
    InforStrength Strength;

    void Start()
    {
        Strength = GetComponentInParent<InforStrength>();
        audio_manager = FindObjectOfType<AudioManager>();
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Enemy" || other.tag =="Boss")
        {
            audio_manager.PlayAudioEffect(audio_source);

            if (other.gameObject.GetComponent<PlayerDamageEnemy>())
            {               
                // if enemy have body harm player, we need reset time to make enemy can not harm player
                if (other.gameObject.GetComponent<PlayerDamageEnemy>())
                    other.gameObject.GetComponent<PlayerDamageEnemy>().timeProtect = 0;               
            }

            GiveDamage(other.gameObject, Strength.Damage);

            if (other.gameObject.GetComponent<Animator>() != null)
                other.gameObject.GetComponent<Animator>().SetTrigger("hit");

            particle.gameObject.SetActive(true);
            particle.Play();
        }
    }

    void GiveDamage(GameObject target, float damage)
    {
        if (target.GetComponent<InforStrength>())
            target.GetComponent<InforStrength>().LoseHealth(damage);
    }
}
