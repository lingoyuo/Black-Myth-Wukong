using UnityEngine;
using System.Collections;

public class CheckRockEnemy : MonoBehaviour {
    public RockEnemy rock;
	// Use this for initialization

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            rock.StartRock();
            gameObject.SetActive(false);
        }
    }
}
