using UnityEngine;
using System.Collections;

public class RockDetectPlayer : MonoBehaviour {
    public GameObject rock;
    public GameObject path;

    void Awake()
    {
        rock.SetActive(false);
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            transform.GetChild(0).gameObject.SetActive(true);
    }

    void OnDrawGizmos()
    {
        Transform[] poin = path.GetComponentsInChildren<Transform>();
        Gizmos.color = Color.red;
        for(int i = 1; i < poin.Length; i++)
        {
            Gizmos.DrawLine(poin[i - 1].position, poin[i].position);
        }
    }
}
