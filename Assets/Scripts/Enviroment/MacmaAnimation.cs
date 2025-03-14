using UnityEngine;
using System.Collections;

public class MacmaAnimation : MonoBehaviour {

    private Transform child_one;
    private Transform child_two;

    private float posEnd;
    private float distance_childs;

    void Start()
    {
        child_one = transform.GetChild(0);
        child_two = transform.GetChild(1);
        distance_childs = Mathf.Abs(child_two.localPosition.x - child_one.localPosition.x);
        posEnd = distance_childs/2;
    }
    
     void Update()
    {
        child_one.localPosition =  Vector2.MoveTowards(child_one.localPosition, new Vector2(posEnd, child_one.localPosition.y), Time.deltaTime);
        child_two.localPosition = Vector2.MoveTowards(child_two.localPosition, new Vector2(posEnd, child_one.localPosition.y), Time.deltaTime);
        Check(child_one);
        Check(child_two);
    }

    void Check(Transform child)
    {
        if(child.localPosition.x == posEnd)
        {
            child.localPosition = new Vector2(child.localPosition.x - distance_childs * 2, child.localPosition.y);
        }
    }
}
