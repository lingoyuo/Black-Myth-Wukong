using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class ControlPhysics  {

    // Normal physic control
    public float maxSpeedInHeight = 1.5f;
    public float maxSpeed = 4.0f;
    public float accelSpeed = 5f;
    public float bakeSpeed;
    public float gravity = 4.0f;
    public float gravityUp = 3f;
    public float jumpHeight = 10.0f;

    // Water enviroment
    public float MaxHeightDive;
    public float SpeedDive;
    public float SpeedDiveUp;

}
