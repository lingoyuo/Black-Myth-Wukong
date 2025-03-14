using UnityEngine;
using System.Collections;

public class Bom : MonoBehaviour
{
    public Vector2 veloci;
    public float deltaSet = 12;
    public float deltaDestroy = 20;
    public Sprite sprite;
    public ParticleSystem parti1;
    public ParticleSystem parti2;
    public ParticleSystem parti3;
    public CircleCollider2D circle;

    bool set = false;
    bool bomm = false;

    Rigidbody2D rigid;
    SpriteRenderer spriteRender;
    PlayerDamageEnemy getHit;
    BoxCollider2D box;
    
    

    // Use this for initialization
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    void Start ()
    {
        parti1.Play();
        rigid.isKinematic = true;
    }

    // Update is called once per frame

    public void SetBom()
    {
        set = true;
        rigid.isKinematic = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")     // enemy damage
        {
            rigid.isKinematic = true;
            BomAttack();
            Invoke("BomDamege", 0.1f);
        }
        if (coll.gameObject.tag == "Platform" && set)
        {
            if (bomm)                              // enemy attack
            {
                rigid.isKinematic = true;
            }
            else                                 // enemy Move
            {
                rigid.linearVelocity = veloci;
            }
        }
        if (coll.gameObject.tag == "WeaponPlayer")    // Player kill enemy
        {
            BomAttack();
            Invoke("BomDamege", 0.8f);
        }
    }

    void BomAttack ()
    {
        if (!bomm)
        {
            rigid.linearVelocity = Vector2.zero;
            bomm = true;
            spriteRender.sprite = sprite;

            parti1.gameObject.SetActive(false);
            parti2.Play();
        }
    }

    void BomDamege ()
    {
        circle.enabled = true;
        parti2.gameObject.SetActive(false);

        if(!parti3.GetComponent<AudioSource>().isPlaying)
            AudioManager.Instances.PlayAudioEffect(parti3.GetComponent<AudioSource>());
        parti3.gameObject.SetActive(true);
        parti3.Play();

        Destroy(gameObject, 0.4f);
    }
}
