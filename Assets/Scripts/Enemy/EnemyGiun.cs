using UnityEngine;
using System.Collections;

public class EnemyGiun : MonoBehaviour {
    public float speedUp = 2;
    public float speedDown = 1;
    public float timeDeplayUp = 2;
    public float timeDeplayDown = 1;
    public Transform point1;
    public Transform point2;
    public GameObject targetMove;
    public PlayerDamageEnemy playerDamage;
    public GameObject gold;
    [Header("sprite Health")]
    public GameObject healthEnemy;
    public GameObject healthEnemybg;
    
    bool up = true;
    bool down = false;

    BoxCollider2D boxGiun;
    Animator animaGiun;
    InforStrength healthGiun;

    void Awake()
    {
        animaGiun = GetComponent<Animator>();
        boxGiun = playerDamage.gameObject.GetComponent<BoxCollider2D>();
        healthGiun = playerDamage.gameObject.GetComponent<InforStrength>();
    }
	// Use this for initialization
	void Start () {
        targetMove.transform.position = (Vector2) point2.transform.position - new Vector2(0,Random.Range(0,4.0f));
	}
	
	// Update is called once per frame
	void Update () {
        GiunMove();
        if(healthGiun.currentHealth <= 0)
        {
            EnemyDeal();
        }

    }

    void MoveUp()
    {
        targetMove.transform.position = Vector3.MoveTowards(targetMove.transform.position, point1.position, speedUp * Time.deltaTime);
        
    }

    void MoverDown()
    {
        targetMove.transform.position = Vector3.MoveTowards(targetMove.transform.position, point2.position, speedDown * Time.deltaTime);  
    }

    void GiunMove()
    {
        if (up)
        {
            MoveUp();
            if (targetMove.transform.position == point1.position && up)
            {
                up = false;
                animaGiun.SetBool("up", true);
                StartCoroutine(SetMove(timeDeplayUp, false));
            }
        }
        else if (down)
        {
            MoverDown();
            if (targetMove.transform.position == point2.position && down)
            {
                down = false;
                StartCoroutine(SetMove(timeDeplayDown, true));
            }
        }
        if (playerDamage.getHit)
        {
            if(boxGiun.enabled)
            {
                up = false;
                down = true;
                StopAllCoroutines();
                boxGiun.enabled = false;
                animaGiun.SetBool("up", false);
                Invoke("DeActiveHealth", 0.1f);
            }
            
        }
        
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(point1.position, point2.position);
    }

    IEnumerator SetMove(float waitTime , bool setUp)
    {
        yield return new WaitForSeconds(waitTime);
        if (setUp)
        {
            up = true;
            ActiveHealth();
        }
        else
        {
            down = true;
            animaGiun.SetBool("up", false);
            Invoke("DeActiveHealth", 0.2f);
        }
    }

    void EnemyDeal()
    {
        targetMove.SetActive(false);
        gold.SetActive(true);
        this.enabled = false;
    }

    void ActiveHealth()
    {
        healthEnemy.SetActive(true);
        healthEnemybg.SetActive(true);
        boxGiun.enabled = true;
    }

    void DeActiveHealth()
    {
        healthEnemy.SetActive(false);
        healthEnemybg.SetActive(false);
        boxGiun.enabled = false;
    }
}
