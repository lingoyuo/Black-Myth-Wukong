using System;
using System.Collections.Generic;
using UnityEngine;

class BoomSkill : ItemPlayer
{
    // Initial constructor
    public BoomSkill (float countdown,int cost,int limit)
    {
        this.nameItems = LocalAccessValue.boom;
        this.amount = 0;
        this.countdown = countdown;
        this.cost = cost;
        this.typeItem = TypeItem.Attack;
        this.limit_amount = limit;
    }
    // OVerrite active skill
    public override void Active(GameObject targetActive)
    {
       
    }
}