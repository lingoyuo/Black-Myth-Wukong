using UnityEngine;
using System.Collections;

public class SubEmitterParticle : MonoBehaviour {

    public ParticleSystem sub_emitter;
    public float time_active = 0.5f;

    private ParticleSystem parent_particle;
    private bool active;
    private float timer;

    void Awake()
    {
        parent_particle = GetComponent<ParticleSystem>();

        timer = 0;
    }

    void Update()
    {
        if (parent_particle.isPlaying)
        {
            timer += Time.deltaTime;
        }
            if(timer > time_active)
            {
                sub_emitter.gameObject.SetActive(true);
                if (!sub_emitter.isPlaying)
                    sub_emitter.Play();             
            }

        if(!parent_particle.isPlaying)
        {
            timer = 0;
            gameObject.SetActive(false);
        }
    }
}
