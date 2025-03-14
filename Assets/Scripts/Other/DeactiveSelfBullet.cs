using UnityEngine;
using System.Collections;

public class DeactiveSelfBullet : MonoBehaviour {

    public float timeDeactive = 3.0f;   
    public PoolManager poolEffect { set { _poolEffect = value; } }
    public PoolManager poolSpawnHarming { set { _poolSpawnHarming = value; } }

    PoolManager _poolEffect;
    PoolManager _poolSpawnHarming;

    CircleCollider2D cirCol;
    RaycastHit2D[] listHit;

    float timer;


    void Start()
    {
        cirCol = GetComponent<CircleCollider2D>();

        listHit = new RaycastHit2D[6];

    }
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > timeDeactive)
        {
            timer = 0;

            if (transform.GetComponentInChildren<ParticleSystem>())
            {
                transform.GetComponentInChildren<ParticleSystem>().Clear();
                transform.GetChild(0).gameObject.SetActive(false);
            }
            transform.GetChild(0).gameObject.SetActive(false);

            if (GetComponent<ParabolMove>())
                GetComponent<ParabolMove>().enabled = false;

            gameObject.SetActive(false);
        }

        RaycastDetectCollider();
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            var player = other.gameObject.GetComponent<InforStrength>();

            // Default damage by bullet = 1;
            player.LoseHealth(1);

            if(transform.GetComponentInChildren<ParticleSystem>())
                transform.GetComponentInChildren<ParticleSystem>().Clear();                                                     // deactive particle effect


            gameObject.SetActive(false);
        }
    }

    public void ResetTimer()
    {
        timer = 0;
    }

    public void RaycastDetectCollider()
    {
        Vector2 center = transform.TransformPoint(cirCol.offset);    

        int j = 0;
        for (int i=0;i<6;i++)
        {
            Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, j) * Vector2.right);
            listHit[0] = Physics2D.Raycast(center, dir.normalized, cirCol.radius*transform.localScale.x, 1 << 13);
            j += 45;
        }

        foreach(RaycastHit2D hit in listHit)
        {
            if(hit)
            {
                // Check if bullet have explotion effect, we need active when bullet hit something like platform
                if (_poolEffect)
                {
                    var effect = _poolEffect.GetObjPool(hit.point);
                    effect.transform.GetChild(0).gameObject.SetActive(true);                                                // active particle effect in effect explosion
                }

                // Check if bullet have something spawn when hit, we need active
                if(_poolSpawnHarming)
                    _poolSpawnHarming.GetObjPool(hit.point);

                // Check if bullet have trail paritcle, we need deactive particle to use in pool
                if (transform.GetChild(0) && transform.GetComponentInChildren<ParticleSystem>())
                {
                    transform.GetComponentInChildren<ParticleSystem>().Clear();
                    transform.GetChild(0).gameObject.SetActive(false);                                                      // deactive particle effect in effect trail frame
                }

                gameObject.SetActive(false);
            }
        }      
    }

    //void OnDrawGizmos()
    //{
    //    cirCol = GetComponent<CircleCollider2D>();
    //    Vector2 center = transform.TransformPoint(cirCol.offset);

    //    for(int i=0;i<=360;i+= 45)
    //    {
    //        Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, i) * Vector2.right);
           
    //        Debug.DrawRay(center, dir.normalized*(cirCol.radius*transform.localScale.x) , Color.red);
    //    }
    //}

}
