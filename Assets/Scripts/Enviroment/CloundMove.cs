using UnityEngine;
using System.Collections;

public class CloundMove : MonoBehaviour {

    public float Speed = 1.0f;

    void Update()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }
}
