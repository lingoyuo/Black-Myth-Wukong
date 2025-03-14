using UnityEngine;
using System.Collections;

public class RotateDirection : MonoBehaviour {

    enum Direction
    {
        Left,
        Right
    }

    Vector3 target;

    Vector2 currentPos;

    Direction direct;

    void Start()
    {
        CheckDirection();
    }

	void Update()
    {
        var directMove = (Vector2)transform.position - currentPos;
        Quaternion rotation;

        if (direct == Direction.Left)
        {
            rotation = Quaternion.LookRotation(new Vector2(Mathf.Abs(directMove.x), directMove.y), transform.TransformDirection(Vector3.up));

            transform.eulerAngles = new Vector3(0, 0, rotation.eulerAngles.x + 180);
        }
        else
        {
            rotation = Quaternion.LookRotation(-directMove, transform.TransformDirection(Vector3.up));
            transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }

        currentPos = transform.position; 
    }

    public void CheckDirection()
    {
        currentPos = transform.position;

        target = GetComponent<ParabolMove>().target;

        if (transform.position.x - target.x > 0)
        {
            direct = Direction.Left;
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else
        {
            direct = Direction.Right;
            transform.eulerAngles = new Vector3(0, 0, 90);

        }
    }
}
