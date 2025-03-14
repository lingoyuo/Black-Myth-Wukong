using UnityEngine;
using System.Collections;

public class CheckBom : MonoBehaviour {

    public Bom bom;
    // Use this for initialization
    void Start()
    {
        transform.SetParent(null);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            bom.SetBom();
            Destroy(gameObject);
        }
    }
}
