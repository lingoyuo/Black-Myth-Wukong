using UnityEngine;
using System.Collections;

public class ParabolMove : MonoBehaviour {

   	public Vector2 target;
    [Header("Stop on target")]
    public bool stop = false;
	public float maxSpeed = 1f;
	public float angleThrow = 45.0f ;
	public float giaToc = 5.0f ;

    Vector2 startPos;

    float speed ;
	float angle;
	float tan;
	float cos;
	float sin;
    float time;
    float timeEnd;

	void Start () {

        InitialPos();
    }

	void Update () {

        time += Time.deltaTime;
        if (!stop || (time <= timeEnd))
        {
            if (startPos != target)
                Move();
        }
	}
	void Move ()
	{
            transform.position = new Vector2(startPos.x + time * speed * cos,
                                       startPos.y + time * speed * sin - giaToc * time * time / 2);
    }

	public void InitialPos()
	{
        if (transform.position.x < target.x)
            angle = angleThrow * Mathf.PI / 180;
        else
            angle = Mathf.PI - angleThrow * Mathf.PI / 180;

        tan = Mathf.Tan(angle);
        cos = Mathf.Cos(angle);
        sin = Mathf.Sin(angle);
        startPos = transform.position;

        speed = 0;

        time = 0;

        float deltaX = (target.x - startPos.x);
        speed = Mathf.Sqrt(deltaX * deltaX * giaToc / 2 / (deltaX * tan + startPos.y - target.y)) / Mathf.Abs( cos);
        timeEnd = Mathf.Abs(deltaX / (speed * cos));
    }
}
