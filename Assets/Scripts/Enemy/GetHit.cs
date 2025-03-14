using UnityEngine;
using System.Collections;

public class GetHit : MonoBehaviour {
	public enum TypeEnemy
	{
		Normal,
		Bee,
	}
    
    public TypeEnemy type = TypeEnemy.Normal;
	InforStrength enemyInfor;
	// Use this for initialization
	void Awake () {
		enemyInfor = GetComponent<InforStrength> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (enemyInfor.Get_Health <= 0)
            if (type == TypeEnemy.Normal)
                Invoke("Dead", 0.02f);
            else
                BeeDead();
	}
	void Dead ()
	{
        Destroy(gameObject);
	}
	void BeeDead ()
	{
        transform.GetChild(0).gameObject.SetActive(false);
		gameObject.GetComponent<Animator> ().SetTrigger("dead");
		gameObject.GetComponent<BeeMove> ().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 2;
		Destroy (gameObject, 3);
	}
}
