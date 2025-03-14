using UnityEngine;
using System.Collections;

public class CheckMoveCamera : MonoBehaviour {
 
    [Space(10)]
    [Header("Overrite dafault offset value")]
    public float OffsetY_Max = 1.5f;
    public float OffsetY_min = -2.0f;

    public float CameraSpeedVertical = 0.5f;

    [Space(10)]
    [Header("Overrite default limit camera")]
    public float maxX = float.MaxValue;
    public float minX = float.MinValue;
    public float maxY = float.MaxValue;
    public float minY = float.MinValue;

    CameraControllerLimited cam;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControllerLimited>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            cam.OffsetY_Max = OffsetY_Max;
            cam.OffsetY_min = OffsetY_min;
            cam.CameraSpeedVertical = CameraSpeedVertical;
            cam.minX = minX;
            cam.maxX = maxX;
            cam.minY = minY;
            cam.maxY = maxY;
        }
    }

}
