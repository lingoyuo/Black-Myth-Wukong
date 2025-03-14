using UnityEngine;
using System.Collections;

public class FlockFly : MonoBehaviour {

    public float speed = 2.0f;

    private Transform swing_left;
    private Transform swing_right;
    private int direct;
    private float curAngle;

    void Start()
    {
       curAngle = Random.Range(0.0f, 85.0f);

        direct = Random.Range(1, 3);

        swing_right = transform.GetChild(0);
        swing_left = transform.GetChild(1);

        swing_right.eulerAngles = new Vector3(0, 0, -curAngle);
        swing_left.eulerAngles = new Vector3(0, 0, curAngle);
    }

    void Update()
    {
        Fly();
    }

    void Fly()
    {
        if (direct == 1)
        {

            curAngle = Mathf.MoveTowards(curAngle, 90, Time.deltaTime * speed);

            // swing_left.eulerAngles = Vector3.MoveTowards(swing_left.eulerAngles, new Vector3(0, 0, 90), Time.deltaTime * speed);
            //swing_right.eulerAngles = Vector3.MoveTowards(swing_right.eulerAngles, new Vector3(0, 0, -90), Time.deltaTime);         

            if(curAngle > 89)
                direct = 0;           
        }
        else
        {
            //swing_left.eulerAngles = Vector3.MoveTowards(swing_left.eulerAngles, new Vector3(0, 0, 0), Time.deltaTime * speed);
            //swing_right.eulerAngles = Vector3.MoveTowards(swing_right.eulerAngles, new Vector3(0, 0, 0), Time.deltaTime);

            //if (Vector3.SqrMagnitude(swing_left.eulerAngles - new Vector3(0, 0, 0)) < 0.1f)

            curAngle = Mathf.MoveTowards(curAngle, 0, Time.deltaTime * speed);

            if (curAngle < 1)
                direct = 1;
        }

        swing_left.eulerAngles = new Vector3(0, 0, curAngle);
        swing_right.eulerAngles = new Vector3(0, 0, -curAngle);
    }
}
