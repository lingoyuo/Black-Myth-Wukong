using UnityEngine;
using System.Collections;

public class PlayerDeadFallInVolCano : MonoBehaviour {

    public GameObject EffectPlayerDead;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!other.GetComponent<PlayerController>().playerDead)
            {
                other.GetComponent<InforStrength>().Set_Health = 0;
                other.GetComponent<PlayerController>().playerDead = true;
                other.gameObject.SetActive(false);
                var effect = (GameObject)Instantiate(EffectPlayerDead, other.transform.position, Quaternion.identity);
                effect.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
