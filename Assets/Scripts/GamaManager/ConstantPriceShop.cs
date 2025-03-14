using UnityEngine;
using System.Collections;

// Sprice items in shop
public class ConstantPriceShop : MonoBehaviour {

    [Space(10)]
    [Header("Bumerang")]
    public int bumerang_cost = 30;
    public int bumerang_limit = 30;

    [Space(10)]
    [Header("Rock")]
    public int rock_cost = 10;
    public int rock_limit = 50;
    public float rock_countdown = 0.1f;

    [Space(10)]
    [Header("Boom")]
    public int boom_cost = 25;
    public int boom_limit = 35;
    public float boom_countdown = 0.1f;

    [Space(10)]
    [Header("Shoe Speed Items")]
    public int shoe_cost = 100;
    public float shoe_time_live = 15.0f;
    public int shoe_limit = 1;

    [Space(10)]
    [Header("Defense Items")]
    public int defense_cost = 300;
    public float defense_time_live = 12.0f;
    public int defense_limit = 1;

    [Space(10)]
    [Header("Health Items")]
    public int health_cost = 250;
    public float health_time_live = 10.0f;
    public int health_limit = 5;

    [Space(10)]
    [Header("Bonus gold x2")]
    public int bonus_gold_cost = 200;
    public float bonus_gold_time_live = 10.0f;
    public int bonus_gold_limit = 1;

    [Space(10)]
    [Header("Bonus time")]
    public int bonus_time_cost = 500;
    public float bonus_time_items_time_live = 10.0f;
    public int bonus_time_limit = 1;

}
