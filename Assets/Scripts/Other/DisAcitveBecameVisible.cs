using UnityEngine;
using System.Collections;

public class DisAcitveBecameVisible : MonoBehaviour {
    public GameObject objectDisActive;
    // Use this for initialization
    void OnBecameInvisible()
    {
        if(gameObject.activeSelf)
        objectDisActive.SetActive(false);
    }
}
