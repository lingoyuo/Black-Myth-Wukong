using UnityEngine;
using System.Collections;

public class PlatformBossPig : MonoBehaviour {
    public ParticleSystem particle;
    public float delta = 2;
    BoxCollider2D boxPlatform;

    void Awake ()
    {
        boxPlatform = GetComponent<BoxCollider2D>();
    }
	// Use this for initialization

    public void DesTroyPlatform ()
    {
        particle.gameObject.transform.position = transform.position;
        particle.Play();
        Invoke("ActivePlatform", 0.1f);
    }

    void ActivePlatform ()
    {
        gameObject.SetActive(false);
    }

    public void SetPlatform()
    {
        boxPlatform.enabled = false;
        if (transform.parent.position.x > 0)
        {
            transform.position = new Vector3(Random.Range(transform.parent.position.x - delta, delta), transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(Random.Range(transform.parent.position.x + delta, -delta), transform.position.y, transform.position.z);
        }
    }
}
