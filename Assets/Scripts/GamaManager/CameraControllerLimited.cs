using UnityEngine;
using System.Collections;

public class CameraControllerLimited : MonoBehaviour {

    public enum Type
    {
        MoveX,
        MoveXX,
        MoveXY,
        MoveY
    }

    public Type TypeCamera = Type.MoveXY;
    public Transform _target;

    [Space(10)]
    [Header("Distance")]

    public float HorizontalLookDistance = 1;
    public float LookAheadTrigger = 0.1f;

    [Space(10)]
    [Header("Limit offset Y")]
    public float OffsetY_Max = 1.5f;
    public float OffsetY_min = -2.0f;

    [Space(10)]
    [Header("Movement speed")]
    public float ResetSpeed = 0.5f;
    public float CameraSpeedHorrizontal = 0.5f;
    public float CameraSpeedVertical = 1.0f;

    [Space(10)]
    [Header("LimitCamera")]
    public float maxX = float.MaxValue;
    public float minX = float.MinValue;
    public float maxY = float.MaxValue;
    public float minY = float.MinValue;

    private Vector3 lastTargetPosition;
    private Vector3 lookAheadPos;
   
    private Vector3 aheadTargetPosDown;
    private Vector3 newCameraPosDown;
    private Vector3 currenVelocityDown;

    private Vector3 aheadTargetPosUp;
    private Vector3 newCameraPosUp;
    private Vector3 currentVelocityUp;

    private Vector3 aheadTargetPosRight;
    private Vector3 newCameraPosRight;
    private Vector3 currentVelocityRight;

    void Start()
    {
        Application.targetFrameRate = 60;

        lastTargetPosition = _target.position;
    }

    void LateUpdate()
    {
        // Tracking pos will move camera when player

        if (!_target)
            return;

        LookTarget();

        switch(TypeCamera)
        {
            case Type.MoveXY:               
                MoveXY();
                break;
        }
       
        lastTargetPosition = _target.position;
    }

    void LookTarget()
    {
        float xMoveDelta = (_target.position - lastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > LookAheadTrigger;

        if (updateLookAheadTarget)
            lookAheadPos = HorizontalLookDistance * Vector3.right * Mathf.Sign(xMoveDelta);
        else
            lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * ResetSpeed);

        // Check camera when player move to the right
        aheadTargetPosRight = _target.position + lookAheadPos;
        newCameraPosRight = Vector3.SmoothDamp(transform.position, aheadTargetPosRight, ref currentVelocityRight, CameraSpeedHorrizontal);
        if (aheadTargetPosRight.x > transform.position.x)
            transform.position = new Vector3(Mathf.Clamp(newCameraPosRight.x, minX, maxX), transform.position.y, transform.position.z);

    }

    void MoveXY()
    {
        // Check camera when player move upward
        aheadTargetPosDown = _target.position + lookAheadPos + Vector3.up*OffsetY_Max;
        newCameraPosDown = Vector3.SmoothDamp(transform.position, aheadTargetPosDown, ref currenVelocityDown, CameraSpeedVertical);
        if (aheadTargetPosDown.y < transform.position.y)
        {
            if (aheadTargetPosDown.y - transform.position.y < -1.0f)
            {
               // print(aheadTargetPosDown.y - transform.position.y);
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(newCameraPosDown.y, minY, maxY), transform.position.z);
            }
        }

        // Check camera when player move downward
        aheadTargetPosUp = _target.position + lookAheadPos + Vector3.up * OffsetY_min;
        newCameraPosUp = Vector3.SmoothDamp(transform.position, aheadTargetPosUp, ref currentVelocityUp, CameraSpeedHorrizontal);
        if (aheadTargetPosUp.y > transform.position.y )
            if(aheadTargetPosUp.y > transform.position.y + 1.0f)
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(newCameraPosUp.y, minY, maxY), transform.position.z);
    }

}
