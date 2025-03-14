using UnityEngine;
using System.Collections;

public class DaBossPig : MonoBehaviour {
    public ParticleSystem particleDa;
    public BossPigControl bossControl;
    PlatformBossPig platform;
    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Platform")
        {
            if (coll.gameObject.GetComponent<PlatformBossPig>() == null)
            {
                particleDa.gameObject.transform.position = transform.position;
                gameObject.SetActive(false);
                particleDa.Play();
            }
            else
            {
                platform = coll.gameObject.GetComponent<PlatformBossPig>();
                platform.DesTroyPlatform();
                Invoke("SetPlatform", 0.7f);
            }
        }
    }

    void SetPlatform()
    {
        platform.gameObject.SetActive(true);
        platform.SetPlatform();
    }

}
