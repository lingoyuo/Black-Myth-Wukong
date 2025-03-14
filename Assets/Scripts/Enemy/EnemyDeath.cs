using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour {
    PoolManager effectDeath;

    InforStrength healthEnemy;
    bool des = true;
	// Use this for initialization
	void Awake () {
        healthEnemy = GetComponent<InforStrength>();
        effectDeath = GameObject.FindGameObjectWithTag("EffectEnemyDead").GetComponent<PoolManager>();
    }
	
	// Update is called once per frame
	void Update () {
            
        if (healthEnemy.Get_Health <=0 && des)
        {
            DestroyEnemy();
        }
	}

    void DestroyEnemy()
    {
        effectDeath.GetObjPool(transform.position);
        des = false;
    }
}
