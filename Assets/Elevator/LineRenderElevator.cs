using UnityEngine;
using System.Collections;

public class LineRenderElevator : MonoBehaviour {

    public Material mat;
    public Transform points01;
    public Transform points02;
    public float lineDrawSpeed = 15;

    public Elevator ele;

    private LineRenderer line;
    private float dist;
    private Vector3 pointsALongLine;
    private float counter;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.material = mat;
        line.SetVertexCount(2);
        line.SetColors(Color.green, Color.green);
        line.SetWidth(0.05f, 0.05f);

        dist = Vector3.Distance(points01.position, points02.position);
        line.SetPosition(0, points01.position);
        line.SetPosition(1, points01.position);
    }

    void LateUpdate()
    {

        if (pointsALongLine.y - points02.position.y > 0.1f)
        {
            if(ele.playerInElevator)
                DrawLine();
        }
        else
        {
            ele.active = true;
            line.SetPosition(1, points02.position);
        }
    }

    void DrawLine()
    {
        counter += 0.1f / lineDrawSpeed;

        float x = Mathf.Lerp(0, dist, counter);

        pointsALongLine = x * Vector3.Normalize(points02.position - points01.position) + points01.position;

        line.SetPosition(1, pointsALongLine);
    }
}
