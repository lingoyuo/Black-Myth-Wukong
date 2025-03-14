using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RockSkill : ItemPlayer {

    // Initial constructor
    public RockSkill(String name, int number, float countdown, int cost, int limit)
    {
        this.nameItems = name;
        this.amount = number;
        this.countdown = countdown;
        this.cost = cost;
        this.typeItem = TypeItem.Attack;
        this.limit_amount = limit;
    }
}
