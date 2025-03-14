using UnityEngine;
using System.Collections;

public class ControlLeafSpawnParticle : MonoBehaviour {

    ParticleSystem particle;

    float timer;

    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void SpawnLeaf()
    {
        if (particle)
        {
            if (!particle.isPlaying)
                particle.Play();
        }
    }

}
