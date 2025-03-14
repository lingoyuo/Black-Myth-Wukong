using UnityEngine;
using System.Collections;

public class Bear2Control : MonoBehaviour {
    public AxeControl axe;
    public GameObject player;

    Animator animatorBear2;
	// Use this for initialization
	void Awake () {
        animatorBear2 = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.transform.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            if (player.transform.position.x - transform.position.x > 20)
                Destroy(gameObject);
        }    
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if (player.transform.position.x > transform.position.x + 5.0f)
            gameObject.SetActive(false);
	
	}

    public void Bear2Attack()
    {
        animatorBear2.SetTrigger("attack");
        Invoke("ShotAxe", 0.1f);
    }

    void ShotAxe()
    {
        axe.AxeAttack();
    }
}
