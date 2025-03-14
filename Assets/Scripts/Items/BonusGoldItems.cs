using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class BonusGoldItems:ItemPlayer
{
    public BonusGoldItems(int cost,float timeLive,int limit)
    {
        this.nameItems = LocalAccessValue.bonus_item;
        this.amount = 0;
        this.cost = cost;
        this.timeLive = timeLive;
        this.limit_amount = limit;
    }
}
