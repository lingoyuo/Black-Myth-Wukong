using UnityEngine;
using System.Collections;

public class BoomProjectile : MonoBehaviour {
    public float speed = 1;
    public float timeDeplay = 0.5f;
    public GameObject player;
    public float angle = 45;
    public ParticleSystem particleDamage;
    public CircleCollider2D circleCheck;
    public GameObject particleChay;

    AudioSource audio_source;
    SpriteRenderer spriteBom;
    Rigidbody2D rigidBom;
    float cosAngle;
    float sinAngle;
    float time = 0;

	// Use this for initialization
	void Awake () {
        rigidBom = GetComponent<Rigidbody2D>();
        spriteBom = GetComponent<SpriteRenderer>();
        cosAngle = Mathf.Cos(angle / 180 * Mathf.PI);
        sinAngle = Mathf.Sin(angle / 180 * Mathf.PI);
        audio_source = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 5)
            DestroyBom();
    }
	
	// Update is called once per frame

    public void BomAttack(GameObject player)
    {
        this.player = player;
        if (rigidBom)
        {
            rigidBom.isKinematic = false;
            rigidBom.linearVelocity = new Vector2(Mathf.Sign(player.transform.localScale.x) * speed * cosAngle, speed * sinAngle);
            time = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Platform" || coll.gameObject.tag == "Enemy")
        {
            AudioManager.Instances.PlayAudioEffect(audio_source);
            GetHit();
        }
    }

    void GetHit()
    {
        circleCheck.enabled = true;
        particleDamage.gameObject.SetActive(true);
        particleDamage.Play();
        rigidBom.linearVelocity = Vector2.zero;
        rigidBom.isKinematic = true;
        spriteBom.enabled = false;
        particleChay.SetActive(false);
        Invoke("DestroyBom", timeDeplay);
    }

    void DestroyBom ()
    {
        particleDamage.Stop();
        circleCheck.enabled = false;
        time = 0.0f;
        particleDamage.gameObject.SetActive(false);
        spriteBom.enabled = true;
        particleChay.SetActive(true);
        gameObject.SetActive(false);
    }
}
