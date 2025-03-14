using UnityEngine;
using System.Collections;

public class WaterPush : MonoBehaviour {

    public float DeactiveTime = 1.5f;

    private ParticleSystem partical;
    private Transform effect;
    private AreaEffector2D effectPhysics;

    private float timer;

    void Start()
    {
        partical = transform.GetComponentsInChildren<ParticleSystem>(true)[0];
        effectPhysics = transform.GetComponentsInChildren<AreaEffector2D>(true)[0];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            partical.gameObject.SetActive(true);
            effectPhysics.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if(partical.gameObject.activeSelf)
            timer += Time.deltaTime;

        if(timer > DeactiveTime)
        {
            partical.gameObject.SetActive(false);
            effectPhysics.gameObject.SetActive(false);
            timer = 0;
        }
    }
}
