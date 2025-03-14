using UnityEngine;
using System.Collections;

public class RockProjectile : MonoBehaviour
{
    public float speed = 2;
    public GameObject rock;
    public GameObject player;
    public ParticleSystem particleRock;
    public ParticleSystem particleGetHit;

    float time = 0;
    bool startRock = false;


    CircleCollider2D cirCol;
    Collider2D _hit;

    void Awake()
    {
        cirCol = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startRock)
        {
            ShotRock();
        }
        time += Time.deltaTime;
        if (time >= 5)
            DestroyRock();

        RaycastDetectCollider();
    }
    void FixedUpdate()
    {
        RaycastDetectCollider();
    }

    public void StartRock(GameObject player)
    {
        this.player = player;
        rock.SetActive(true);
        speed = Mathf.Sign(player.transform.localScale.x) * Mathf.Abs(speed);
        startRock = true;

        if(!cirCol)
            cirCol = GetComponent<CircleCollider2D>();
        cirCol.enabled = true;
        time = 0;
    }

    void ShotRock()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<InforStrength>().LoseHealth(1);

            if (!particleGetHit.gameObject.activeSelf)
            {
                particleGetHit.gameObject.SetActive(true);
                particleGetHit.Play();

                // play audio get hit
                AudioManager.Instances.PlayAudioEffect(particleGetHit.GetComponent<AudioSource>());
            }

            DestroySpriteRock();
        }
    }

    void DestroySpriteRock()
    {
        startRock = false;
        rock.SetActive(false);
        cirCol.enabled = false;
        Invoke("DestroyRock", 1.0f);
    }

    void DestroyRock()
    {
        gameObject.SetActive(false);
    }

    void SetParticle()
    {
        particleRock.gameObject.transform.position = new Vector3(transform.position.x + speed / 100, transform.position.y, transform.position.z);
        particleRock.Play();
    }

    // raycast circle to detect collider
    public void RaycastDetectCollider()
    {
        Vector2 center = transform.TransformPoint(cirCol.offset);

        _hit = Physics2D.OverlapCircle(center, cirCol.radius + 0.1f, 1 << 13);

        if (_hit & cirCol.enabled)
        {
            SetParticle();
            DestroySpriteRock();
        }
    }
}
