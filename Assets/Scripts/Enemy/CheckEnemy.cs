using UnityEngine;
using System.Collections;

public class CheckEnemy : MonoBehaviour {

    public enum TypeCheck
    {
        Fiexed,
        Move,
    }
    public bool ghost = false;
	
    public bool DetectPlayer { get { return detectPlayer; } }

    public TypeCheck typeCheck = TypeCheck.Fiexed;

    BeeMove check;
    Vector3 delta;
    bool detectPlayer;
    Animator enemyAnima;

    // Use this for initialization
    void Awake () {
	    check =	transform.GetComponentInParent<BeeMove> ();
        enemyAnima = transform.GetComponentInParent<Animator>();
        detectPlayer = false;
	}
	void Start ()
	{
        transform.SetParent (null);
        delta = transform.position - check.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (typeCheck == TypeCheck.Move)
        {
            transform.position = check.gameObject.transform.position + delta;
        }
    }

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") {
            if(ghost)
            {
                enemyAnima.SetTrigger("fly");
            }
            detectPlayer = true;
                check.check = true;
                check.player = coll.gameObject;
			Destroy (gameObject);
		}
	}
}
