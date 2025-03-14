using UnityEngine;
using System.Collections;

public class WaitForSecondsExample : MonoBehaviour
{
    public float min;
    public float max;

    public WaitForSecondsExample(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}
