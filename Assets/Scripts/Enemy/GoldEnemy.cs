using UnityEngine;
using System.Collections;

public class GoldEnemy : MonoBehaviour {

    public float delayTime = 0.0f;
    [Header("Random % coin")]
    public int random;
    [Header("Gold 5,10,15,20")]
    public int coin;
    PoolManager goldPool;

    bool dis = false;
    InforStrength healthEnemy;
	// Use this for initialization
	void Awake () {
        healthEnemy = GetComponent<InforStrength>();
        goldPool = GameObject.FindGameObjectWithTag("PoolGold").GetComponent<PoolManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(healthEnemy.Get_Health <= 0 && !dis)
        {
            Invoke("RandomGold", delayTime);
            dis = true;
        }
	}

    void InstantiateGold ()
    {
        GameObject gold = goldPool.GetObjPool(transform.position);

        if (gold == null)
            return;
        gold.GetComponent<Gold>().coin = coin;
    }

    void RandomGold()
    {
        int i = Random.Range(1, 100);
        if (i <= random)
        {
            InstantiateGold();
        }  
    }
}
