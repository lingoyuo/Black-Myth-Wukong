using UnityEngine;
using System.Collections;

public class RespawnParallax : MonoBehaviour {
    public bool tree;
    public float valueSpawn = 10.0f;

    private SpriteRenderer sprite;

    private float distanceRespawn;
    private float currentPosCamera;
    private float distance;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        distanceRespawn = sprite.bounds.size.x;

        currentPosCamera = Camera.main.transform.position.x;

    }

    void Update()
    {
        float xMoveDelta = Camera.main.transform.position.x - currentPosCamera;
        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > 0.2f;

        if (updateLookAheadTarget)
            distance = Camera.main.transform.position.x - transform.position.x + 0.5f;
        else
            distance = Camera.main.transform.position.x - transform.position.x;

        if (distance >= distanceRespawn && !tree)
            Spawn();

        currentPosCamera = Camera.main.transform.position.x;
        
    }

    void OnBecameInvisible()
    {
        if (tree)
            Spawn();
    }

    void Spawn()
    {
        if(!tree)
            transform.position = new Vector3(transform.position.x + sprite.bounds.size.x * 2 - 0.2f, transform.position.y,transform.position.z);
        else
            transform.position = new Vector3(transform.position.x + valueSpawn * 2 - 0.3f, transform.position.y, transform.position.z);
    }
}
