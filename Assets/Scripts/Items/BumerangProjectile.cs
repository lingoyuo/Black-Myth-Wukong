using UnityEngine;
using System.Collections;

public class BumerangProjectile : MonoBehaviour {
    
    public float speed = 0.2f;
    public int damage = 1;
    public float delta;
    public Transform posSpawn;

    public ParticleSystem particle;

	AudioSource audioGame;

    // detect collider platform
    CircleCollider2D cirCol;
    Collider2D _hit1;                           // obj hit when move forward
    Collider2D _hit2;                           // obj hit when move back

    Vector3 endPosition;
    Vector3 refSmooth = Vector3.zero;

    bool flipBum = false;
    float speefBum;

	void Awake () {
        cirCol = GetComponent<CircleCollider2D>();
		audioGame = gameObject.GetComponent<AudioSource> ();     
    }
	
	void Update () {
        if (!flipBum)
        {
            if (Mathf.Abs(transform.position.x - endPosition.x) < 0.4f)
            {
                flipBum = true;
                endPosition = new Vector3(posSpawn.transform.position.x, posSpawn.transform.position.y + 0.6f, transform.position.z);
            }
        }
        else
        {
            speefBum = Mathf.Clamp(speefBum - Time.deltaTime / 2, 0.0015f, speed);
            endPosition = new Vector3(posSpawn.transform.position.x, posSpawn.transform.position.y + 0.6f, transform.position.z);
        }
        MoveSmoothDamp(endPosition);
    }

    void FixedUpdate()
    {
        RaycastDetectCollider();
    }

    // move bum
    void MoveSmoothDamp (Vector3 endPosition)
    {
        transform.position = Vector3.SmoothDamp(transform.position, endPosition, ref refSmooth, speefBum);

        // when bum nearly end postion, we need deactive it
        if (Vector2.Distance(endPosition, transform.position) < 0.1f)
            DestroyBum();
    }

    void SetEndPosition ( float delta)
    {
        endPosition = new Vector3(posSpawn.transform.position.x + delta * Mathf.Sign(posSpawn.transform.localScale.x), posSpawn.transform.position.y + 1.0f, transform.position.z);
    }

    public void StartBum(Transform posSpawn)
    {
        this.posSpawn = posSpawn;
        flipBum = false;
        gameObject.transform.SetParent(null);
        transform.position = new Vector3(posSpawn.transform.position.x + 0.2f, posSpawn.transform.position.y + 1.0f, transform.position.z);
        SetEndPosition(delta);
        speefBum = speed;

        AudioManager.Instances.PlayAudioEffect(audioGame);
        audioGame.loop = true;
    }

    // Detect hit enemy and player
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (flipBum && coll.gameObject.tag == "Player") {
            AudioManager.Instances.StopAudioEffect(audioGame);
			DestroyBum ();
		}
        else if (coll.gameObject.tag == "Enemy")
        {
            if (coll.gameObject.GetComponent<BearController>())
                coll.gameObject.GetComponent<BearController>().obj_damage = gameObject;

            coll.gameObject.GetComponent<InforStrength>().LoseHealth(damage);

           

            if (!particle.gameObject.activeSelf)
            {
                particle.gameObject.SetActive(true);
                particle.Play();
            }
        }
    }

    // Destroy bum end reset all value
    void DestroyBum()
    {
        audioGame.Stop();
        audioGame.loop = false;

        flipBum = false;
        transform.position = posSpawn.position;
        _hit1 = null;
        _hit2 = null;
        gameObject.SetActive(false);
    }

    // raycast circle to detect hit platform
    public void RaycastDetectCollider()
    {
        Vector2 center = transform.TransformPoint(cirCol.offset);

        if (!flipBum)
            _hit1 = Physics2D.OverlapCircle(center, cirCol.radius + 0.1f, 1 << 13);
        else
            _hit2 = Physics2D.OverlapCircle(center, cirCol.radius , 1 << 13);

        if (_hit1)
        {
            if (flipBum)
            {
                if(_hit2)
                    if(_hit1.gameObject != _hit2.gameObject)
                    DestroyBum();
            }
            endPosition = new Vector3(posSpawn.transform.position.x, posSpawn.transform.position.y + 0.7f, transform.position.z);
            flipBum = true;
        }
    }
}
