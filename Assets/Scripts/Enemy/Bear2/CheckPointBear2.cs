using UnityEngine;
using System.Collections;

public class CheckPointBear2 : MonoBehaviour {
    public Bear2Control bear2;
	
    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            bear2.Bear2Attack();
            gameObject.SetActive(false);
        }
    }
	
}
