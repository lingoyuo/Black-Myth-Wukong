using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class DefenseItems:ItemPlayer
{
    public DefenseItems(int cost,float timeLive,int limit)
    {
        this.nameItems = LocalAccessValue.defense_item;
        this.amount = 0;
        this.cost = cost;
        this.timeLive = timeLive;
        this.limit_amount = limit;
    }

    public override void Active(GameObject targetActive)
    {
        targetActive.GetComponent<InforStrength>().shield_active = true;
    }

    public override void Deactive(GameObject target)
    {
        target.GetComponent<InforStrength>().shield_active = false;
    }
}

