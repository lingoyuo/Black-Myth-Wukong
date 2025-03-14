using UnityEngine;
using System.Collections;

public class SpeedParticleEffect : MonoBehaviour {

    public Transform player;

    private ParticleSystemRenderer particle;

    void Awake()
    {
        particle = GetComponent<ParticleSystemRenderer>();
    }

    void Update()
    {
        particle.material.mainTextureScale = new Vector2(player.transform.localScale.x, 1);
    }
}
