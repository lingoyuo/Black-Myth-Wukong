using UnityEngine;
using System.Collections;

public class EnemyScore : MonoBehaviour {
    public int score = 10;

    PoolManager effectScoreText;

    InforStrength healthEnemy;
    bool des = true;
	GameManager gameManager;
	InfomationGame info;
    // Use this for initialization
    void Awake()
    {
		info = FindObjectOfType<InfomationGame> ();
		gameManager = FindObjectOfType<GameManager> ();
        healthEnemy = GetComponent<InforStrength>();
        effectScoreText = GameObject.FindGameObjectWithTag("EffectScoreText").GetComponent<PoolManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthEnemy.Get_Health <= 0 && des)
        {
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        GameObject effectScore = effectScoreText.GetObjPool(transform.position);
        effectScore.GetComponent<ScoreText>().SetScoreText(score);
        des = false;
		gameManager.SET_SCORE = gameManager.GET_SCORE + score;
		info.CheckItem ();
    }
}
