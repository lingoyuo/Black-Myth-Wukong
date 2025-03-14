using System;
using System.Collections.Generic;
using UnityEngine;
public class BearChaseState : FSMState
{
    private float speed_chase;
    private BearDetectPlayer sensor_detect;
    private BearDetectPlayer sensor_attack;
    private Transform player;

    // value count time when enemy chase player but can not reach
    private float timer;

    public BearChaseState(Animator anim,BearDetectPlayer sensor_detect, BearDetectPlayer sensor_attack,SensorDetectEnviromentBear sensor_envil,float speed_chase)
    {
        stateID = FSMStateID.Chasing;
        this.anim = anim;
        this.sensor_attack = sensor_attack;
        this.sensor_detect = sensor_detect;
        envi_detect = sensor_envil;
        this.speed_chase =speed_chase;
    }

    public override void Act(Transform npc)
    {
        if (!envi_detect.CheckDetectObstacles()) {
            if (sensor_detect.PosDetectPlayer > npc.transform.position.x+0.5f)
                npc.transform.localScale = new Vector3(1, 1, 1);
            else if(sensor_detect.PosDetectPlayer < npc.transform.position.x - 0.5f)
            {       
                npc.transform.localScale = new Vector3(-1, 1, 1);
            }

            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("bear_idle") || !anim.GetCurrentAnimatorStateInfo(0).IsName("bear_attack"))
            {
                if(Mathf.Abs(npc.transform.position.x - sensor_detect.PosDetectPlayer) > 0.3f && sensor_detect.detected)
                    npc.transform.position = new Vector2(Mathf.MoveTowards(npc.transform.position.x, sensor_detect.PosDetectPlayer, Time.deltaTime * 2*speed_chase), npc.transform.position.y);				
            }
            anim.speed = 2;
        }

        if (!sensor_detect.Player)
            timer += Time.deltaTime;
        else
            timer = 0;     
    }

    public override void Reason(Transform npc)
    {

        if (Mathf.Abs(npc.transform.position.x - sensor_detect.PosDetectPlayer) < 0.5f)
            sensor_detect.detected = false;

        if (sensor_attack.Player)
            npc.GetComponent<BearController>().SetTransition(Transition.ReachPlayer);
        else if (Mathf.Abs(npc.transform.position.x - sensor_detect.PosDetectPlayer) < 0.5f && !sensor_detect.Player)
        {
            sensor_detect.detected = false;
            anim.SetTrigger("idle");
            npc.GetComponent<BearController>().SetTransition(Transition.LostPlayer);
        }
        else if (envi_detect.pos_obstacle_left_player > sensor_detect.PosDetectPlayer || envi_detect.pos_obstacle_right_player < sensor_detect.PosDetectPlayer || timer > 2.0f)
        {
            anim.speed = 1.0f;
            anim.SetTrigger("idle");
            npc.GetComponent<BearController>().SetTransition(Transition.LostPlayer);
        }
        else if (envi_detect.obstacle_detect)
        {
            if ((npc.position.x < envi_detect.pos_obstacle && envi_detect.pos_obstacle < sensor_detect.PosDetectPlayer) ||
                (npc.position.x > envi_detect.pos_obstacle && envi_detect.pos_obstacle > sensor_detect.PosDetectPlayer))
            {
                anim.speed = 1.0f;
                anim.SetTrigger("idle");
                npc.GetComponent<BearController>().SetTransition(Transition.LostPlayer);
            }
        }
    }
}

