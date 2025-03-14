using UnityEngine;
using System.Collections;

public class ShroomMove : MonoBehaviour {
	public Vector2 veloci;
	bool set = false;

    Rigidbody2D rigid;
    PlayerDamageEnemy getHit;
    BoxCollider2D box;
    Animator animatorShroom;
    GameObject player;

    // Use this for initialization
    void Awake () {
		rigid = GetComponent<Rigidbody2D> ();
        getHit = GetComponent<PlayerDamageEnemy>();
        box = GetComponent<BoxCollider2D>();
        animatorShroom = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (getHit.getHit)
        {
            if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
            rigid.linearVelocity = new Vector2(Mathf.Sign(player.transform.position.x - transform.position.x) * veloci.x, 0);
        }
        if (set)
        {
            if (rigid.linearVelocity.y < 0 && animatorShroom.GetBool("fly"))
            {
                animatorShroom.SetBool("fly", false);
            }
            else
            if (rigid.linearVelocity.y > 0 && !animatorShroom.GetBool("fly"))
            {
                animatorShroom.SetBool("fly", true);
            }
        }

    }

    public void SetShroom()
    {
        rigid.isKinematic = false;
        box.isTrigger = true;
        set = true;
        animatorShroom.SetTrigger("set");
    }
	
	void OnTriggerStay2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			rigid.linearVelocity = new Vector2 (Mathf.Sign(coll.gameObject.transform.position.x - transform.position.x) * veloci.x , 0);
		}
		if (coll.gameObject.tag == "Platform" && set ) {
			rigid.linearVelocity = veloci;
		}
	}
}
