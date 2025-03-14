using UnityEngine;
using System.Collections;

public class FrameMoveVertical : MonoBehaviour {

    [HideInInspector]
    public float PosDeactiveLocal;

    [HideInInspector]
    public float SpeedMove;

    private WaterfallVolcano waterfall;
    private GameObject particle;
    private bool spawnEffect;

    private float timer;

    void Start()
    {
        waterfall = transform.GetComponentInParent<WaterfallVolcano>();
    }

    void Update()
    {
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(transform.localPosition.x, PosDeactiveLocal), Time.deltaTime * SpeedMove);

        if (transform.localPosition.y < PosDeactiveLocal + 0.1f)
        {
            if (!spawnEffect)
            {
                // Take particle effect
                particle = waterfall.GetParitcalEffect();
                if (!particle)
                    particle = waterfall.AddParticleEffect();
                particle.SetActive(true);
                particle.transform.localPosition = transform.localPosition;
                particle.transform.GetChild(0).gameObject.SetActive(true);
                spawnEffect = true;
                transform.GetChild(0).gameObject.SetActive(false);
            }

            timer += Time.deltaTime;

            if (timer > 1.0f)
            {
                spawnEffect = false;
                timer = 0;
                particle.transform.GetChild(0).gameObject.SetActive(false);
                particle.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        {
            other.GetComponent<Animator>().SetTrigger("hit");
        }
    }
}
