using UnityEngine;
using System.Collections;

public class FrameTrapVolcano : MonoBehaviour {

    public float TimeSpawnFire = 2.0f;

    private BoxCollider2D box;
    private ParticleSystem particleEffect;

    private float timer;
    private float timer_fire;
    private float MaxStartLifeTime;
    private bool fireOff;

    void Start()
    {
        box = transform.GetChild(0).GetComponent<BoxCollider2D>();
        particleEffect = GetComponentInChildren<ParticleSystem>();

        // Intital
        timer = Random.Range(0.0f, 2.0f);
        timer_fire = 0;
        box.enabled = false;
        MaxStartLifeTime = particleEffect.startLifetime;
        particleEffect.startLifetime = 0.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > TimeSpawnFire )
        {
            if (!fireOff)
            {
                particleEffect.startLifetime = Mathf.MoveTowards(particleEffect.startLifetime, MaxStartLifeTime, Time.deltaTime*4);
                box.enabled = true;

                // Increase box size to damage player
                box.size = new Vector2(box.size.x,Mathf.Clamp( box.size.y + Time.deltaTime * 1.5f,0.0f,2.3f));
                box.offset = new Vector2(box.offset.x, box.size.y / 2);
            }
            else
            {
                
                particleEffect.startLifetime = Mathf.MoveTowards(particleEffect.startLifetime, 0.0f, Time.deltaTime / 2);
                box.size = new Vector2(box.size.x, box.size.y - Time.deltaTime * 1.2f);
                box.offset = new Vector2(box.offset.x, box.size.y / 2);
                if (particleEffect.startLifetime == 0.0f)
                {
                    box.size = new Vector2(box.size.x, 0.0f);
                    box.offset = new Vector2(box.offset.x, box.size.y / 2);
                    box.enabled = false;
                    fireOff = false;
                    timer = 0.0f;
                }
            }
            if (particleEffect.startLifetime == MaxStartLifeTime)
            {
                timer_fire += Time.deltaTime;
                if (timer_fire > TimeSpawnFire)
                {
                    //box.enabled = false;
                    timer_fire = 0.0f;
                    fireOff = true;
                }
            }          
        }
    }
}
