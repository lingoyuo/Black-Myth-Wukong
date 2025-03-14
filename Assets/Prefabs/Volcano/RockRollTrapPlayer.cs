using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockRollTrapPlayer : MonoBehaviour {

    public List<Transform> listPoints;
    public Transform pointsThrough;
    public float speed = 3.0f;

    public float offset = 0.5f;              // Value distance change to next points

    private int index;
    private Vector2 des;

    private bool stop;
    private bool fall;

    void Start()
    {
        transform.position = listPoints[0].position;
        des = listPoints[1].position;
        index = 0;
    }

    void Update()
    {
        if(!stop)
            Move(des,ref index);
        else if(pointsThrough != null && !fall)
        {
            fall = true;
            gameObject.AddComponent<ParabolMove>().target = pointsThrough.position;
            gameObject.GetComponent<ParabolMove>().giaToc = 20;
            des = pointsThrough.position;
        }

        if (transform.position.x > des.x)
            RotateToLeft();
        else
            RotateToRight();

        if (pointsThrough)
            if (transform.position.y < pointsThrough.position.y)
                gameObject.SetActive(false);

    }

    void NextPoint(ref int _index)
    {
        _index++;
        if (_index < listPoints.Count)           
            des = listPoints[_index].position;   
        else
            stop = true;
    }

    void Move(Vector2 des,ref int _index)
    {
        transform.position = Vector2.MoveTowards(transform.position, des, Time.deltaTime*speed);

        if (Vector2.Distance(transform.position,des) < offset)
        {
            NextPoint(ref _index);
        }
    }



    void RotateToRight()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - Time.deltaTime * 500);
    }

    void RotateToLeft()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + Time.deltaTime * 500);
    }


}
