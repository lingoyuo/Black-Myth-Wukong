using UnityEngine;
using System.Collections;

public class PlatformCheck : MonoBehaviour {
    BoxCollider2D boxPlatform;

	void Awake ()
    {
        boxPlatform = GetComponent<BoxCollider2D>();
    }

	void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && coll.transform.position.y - transform.position.y > -0.1f)
        {
            // fix bug when player dead
            // check health player when player enter platform

            if (coll.GetComponent<InforStrength>().Get_Health > 0)
            {
                boxPlatform.enabled = true;

                coll.transform.SetParent(transform.parent);
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (coll.GetComponent<InforStrength>().Get_Health > 0)
            {

                boxPlatform.enabled = false;

                coll.transform.SetParent(null);
            }
        }
    }

}
