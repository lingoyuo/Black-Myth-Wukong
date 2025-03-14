using UnityEngine;
using System.Collections;

public class AxeControl : MonoBehaviour {
    public float coldown = 1;
    public Bear2Control bear;
    public ParabolMove axeMove;
    public GameObject player;

    Rigidbody2D rid;
    Animator animationAxe;
    Vector3 beginTranform;
    Vector3 beginAngle;
	// Use this for initialization

    void Awake ()
    {
        rid = GetComponent<Rigidbody2D>();
        animationAxe = GetComponent<Animator>();
        Invoke("GetComponen", 3);
    }

	void Start () {
        beginTranform = transform.position;
        beginAngle = transform.eulerAngles;
	}

    public void AxeAttack()
    {
        transform.SetParent(null);
        ShotAxe();
    }

    public void SetAxe()
    {
        transform.position = beginTranform;
        transform.eulerAngles = beginAngle;
        transform.SetParent(bear.gameObject.transform);
        animationAxe.SetBool("attack", false);
    }

    void OnBecameInvisible()
    {      
        if (rid.linearVelocity.y <= 0)
            ResetAxe();
    }

    void ShotAxe()
    {
        if(player.transform.position.x - bear.transform.position.x < 10)
        {
            axeMove.enabled = true;
            axeMove.target = new Vector2(player.transform.position.x, player.transform.position.y);
            axeMove.InitialPos();
            animationAxe.SetBool("attack", true);
        }
        else
        {
            bear.enabled = false;
        }
    }

    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<InforStrength>().LoseHealth(1);                        // Decrease health player
            ResetAxe();
        }
        if (coll.gameObject.tag == "Platform")
        {
            ResetAxe();
        }
    }

    void ResetAxe()
    {
        axeMove.enabled = false;
        SetAxe();
        Invoke("SetAttack", coldown);
    }

    void SetAttack()
    {
        bear.Bear2Attack();
    }
}
