using System;
using System.Collections.Generic;
using UnityEngine;


class BumerangSkill : ItemPlayer
{
    // Initial constructor
    public BumerangSkill(String name ,int number, int cost,int limit)
    {
        this.nameItems = name;
        this.amount = number;
        this.cost = cost;
        this.typeItem = TypeItem.Attack;
        this.limit_amount = limit;
    }
}

