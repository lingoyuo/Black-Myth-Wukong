using UnityEngine;
using System.Collections;

public class PlatformMoveVolcano : MonoBehaviour {

    [HideInInspector]
    public float PosDeactiveLocal;

    [HideInInspector]
    public float SpeedMove;

    private Animator anim;
    private WaterfallVolcano waterfall;
    private GameObject particle;
    private bool spawnEffect;

    private float timer;

    void Start()
    {
        anim = GetComponent<Animator>();
        waterfall = transform.GetComponentInParent<WaterfallVolcano>();
    }

	// Update is called once per frame
	void Update () {
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(transform.localPosition.x, PosDeactiveLocal), Time.deltaTime * SpeedMove);
        if (transform.localPosition.y < PosDeactiveLocal + 0.1f)
        {
            anim.SetBool("disappear", true);

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
            }

            timer += Time.deltaTime;

            if(timer > 2.0f)
            {
                spawnEffect = false;
                timer = 0;
                particle.transform.GetChild(0).gameObject.SetActive(false);
                anim.SetBool("disappear", false);
                particle.SetActive(false);
                gameObject.SetActive(false);
            }
        }          
	}
}
