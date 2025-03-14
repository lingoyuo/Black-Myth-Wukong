using UnityEngine;
using System.Collections;

public class ScaleCameraBoss : MonoBehaviour {
    Camera cam;
	// Use this for initialization
	void Awake () {
        cam = GetComponent<Camera>();
	}
     void Start ()
    {
        cam.orthographicSize = 5 * 1.78f / ((float)Screen.width / (float)Screen.height);
    }
}
