using UnityEngine;
using System.Collections;
using System;

public class BearPatrolState : FSMState
{
    BearDetectPlayer sensor;

    protected Vector3 destination;
    protected Transform[] waypoints;

    private bool stuck;

    private float timer;
    public BearPatrolState(Transform[] listWayPoints,Animator anim,BearDetectPlayer sensor,SensorDetectEnviromentBear sensor_envi)
    {
        this.anim = anim;
        stateID = FSMStateID.Patrolling;
        waypoints = listWayPoints;
        this.sensor = sensor;
        envi_detect = sensor_envi;
        FindToNextPoint();
    }

    public override void Act(Transform npc)
    {
       // anim.speed = 1.0f;

        if (!stuck)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("bear_idle"))
            {
                npc.transform.position = new Vector2(Mathf.MoveTowards(npc.transform.position.x, destination.x, Time.deltaTime), npc.transform.position.y);
            }

            if (destination.x > npc.transform.position.x+0.2f)
                npc.transform.localScale = new Vector3(1, 1, 1);
            else if(destination.x < npc.transform.position.x - 0.2f)
                npc.transform.localScale = new Vector3(-1, 1, 1);           
        }
        else   
            timer += Time.deltaTime;


        // stuck
        if (!stuck)
            CheckStuck(npc);
        else
            CheckNotStuck();

        // obstacle find check points
        if (envi_detect.CheckDetectObstacles())
        {
            if (!stuck)
                npc.transform.localScale = new Vector3(-npc.transform.localScale.x, npc.transform.localScale.y, npc.transform.localScale.z);
            FindToNextPoint();
        }
    }

    public override void Reason(Transform npc)
    {
        if (Mathf.Abs(npc.transform.position.x - destination.x) < 0.1f)
        {
            FindToNextPoint();
            anim.SetTrigger("idle");
            npc.GetComponent<BearController>().SetTransition(Transition.ReachPatrolPoints);
        }

        if (sensor.Player && sensor.PosDetectPlayer < envi_detect.pos_obstacle_right_player && sensor.PosDetectPlayer > envi_detect.pos_obstacle_left_player && sensor.detected)
        {
            anim.SetTrigger("move");
            npc.GetComponent<BearController>().SetTransition(Transition.SawPlayer);
        }
    }

    public void FindToNextPoint()
    {
        destination = (destination == waypoints[0].position) ? waypoints[1].position : waypoints[0].position;
    }

    // Check stuck
    public void CheckStuck(Transform npc)
    {
        if (envi_detect.obstacle_detect)
        {
            if ((envi_detect.pos_obstacle < waypoints[0].position.x && envi_detect.pos_obstacle < waypoints[1].position.x  && npc.position.x < envi_detect.pos_obstacle) ||
                (envi_detect.pos_obstacle) > waypoints[0].position.x && envi_detect.pos_obstacle > waypoints[1].position.x && npc.position.x > envi_detect.pos_obstacle)
            {
                stuck = true;
                anim.SetTrigger("stick");               
            }
        }                      
    }

    // Check not stuck
    public void CheckNotStuck()
    {
        if (!envi_detect.obstacle_detect && timer > 2.0f)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("stick"))
                anim.SetTrigger("move");
            stuck = false;
            timer = 0;
        }
    }
}
