using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ShoeSpeedItems:ItemPlayer
{
    public ShoeSpeedItems(int cost,float timeAlive,int limit)
    {
        this.nameItems = LocalAccessValue.shoe_item;
        this.cost = cost;
        this.amount = 0;
        this.timeLive = timeAlive;
        this.limit_amount = limit;
    }

    public override void Active(GameObject targetActive)
    {
        var speed_player = targetActive.GetComponent<PlayerController>();
        speed_player.MaxSpeedPlayer = 9.0f;
        speed_player.DefaultPhysics.maxSpeedInHeight = 7.0f;
        speed_player.DefaultPhysics.maxSpeed = 10;
        speed_player.DefaultPhysics.accelSpeed = 11;
        speed_player.DefaultPhysics.jumpHeight = 13;      
    }

    public override void Deactive(GameObject target)
    {
        var speed_player = target.GetComponent<PlayerController>();
        speed_player.MaxSpeedPlayer = 7.0f;
        speed_player.DefaultPhysics.maxSpeedInHeight = 5.0f;
        speed_player.DefaultPhysics.maxSpeed = 8.0f;
        speed_player.DefaultPhysics.accelSpeed = 9.0f;
        speed_player.DefaultPhysics.jumpHeight = 12.0f;
    }
}

