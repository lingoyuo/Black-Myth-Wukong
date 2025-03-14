using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class BonusTimeItems:ItemPlayer
{
	public float Get_time_bonus{get{return timeBonus;}}
	private float timeBonus;

    public BonusTimeItems(int cost,float timeLive,int limit)
    {
        this.nameItems = LocalAccessValue.time_item;
        this.amount = 0;
        this.timeLive = timeLive;
        this.cost = cost;
        this.limit_amount = limit;
		timeBonus = 30;
    }
}

