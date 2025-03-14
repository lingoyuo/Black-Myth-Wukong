using UnityEngine;
using System.Collections;

public class DeativeByTime : MonoBehaviour {

    public float time_wait = 1.0f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > time_wait)
        {
            timer = 0;         

            if (transform.childCount > 0)
                transform.GetChild(0).gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
