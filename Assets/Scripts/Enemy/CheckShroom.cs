using UnityEngine;
using System.Collections;

public class CheckShroom : MonoBehaviour {
    public ShroomMove shroom;
	// Use this for initialization
	void Start () {
        transform.SetParent(null);
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            shroom.SetShroom();
            Destroy(gameObject);
        }
    }
}
