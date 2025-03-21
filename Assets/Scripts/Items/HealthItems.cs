using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class HealthItems:ItemPlayer
{
    public HealthItems(int cost,float timeLive,int limit)
    {
        this.nameItems = LocalAccessValue.health_item;
        this.amount = 0;
        this.cost = cost;
        this.timeLive = timeLive;
        this.limit_amount = limit;
    }

    public override void Active(GameObject targetActive)
    {
        if (targetActive.GetComponent<InforStrength>().Get_Health == 8)
            return;
        else if (targetActive.GetComponent<InforStrength>().Get_Health + 2 >= 8)
            targetActive.GetComponent<InforStrength>().Set_Health = 8;
        else
            targetActive.GetComponent<InforStrength>().Set_Health = 2 + targetActive.GetComponent<InforStrength>().Get_Health;
    }
}
