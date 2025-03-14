using UnityEngine;
using System.Collections;
// [ExecuteInEditMode]
public class BeeMove : MonoBehaviour {

	public float speed = 4f;      // toc do khi bee lao vao player
	public float speedMove2 = 5f; // toc do bee bat ra khoi player
	public float delta = 0.25f;      // thay doi do cong khi bee tan cong
	public float playerDelta = 1f;// khoang cach tu anchor cua player den dau`
	public float limit1 = 6;         // khoang cach toi da khi bee bj bat ra roi quay lai tan cong
	public float limit2 = 2;
	public bool check = false;
    [Header("Set huong xoay enemy")]
    public bool setAngle = true;
	public GameObject player;
	Rigidbody2D rigBee;
	PlayerDamageEnemy damage;


	float rad = -90;  // huong ban dau
	float xSpeed;     // toc do theo truc x
	float ySpeed;     // toc do theo truc y
	float deltaX;     // khoang cach giua bee va player theo truc x
	float deltaY;     // khoang cach giua bee va player theo truc y

	bool move = true;     // set trang thai tan cong
	bool move2 = false;   // set trang thai bj bat ra khoi player
	Vector3 offSet;

	void Awake ()
	{
		rigBee = GetComponent<Rigidbody2D> ();
		damage = GetComponent<PlayerDamageEnemy> ();
		//check = transform.GetChild (0).gameObject.GetComponent<CheckEnemy> ().check;
	}

	// Use this for initialization
	void Start () {
		rad = Mathf.PI * rad / 180;
        rigBee.gravityScale = 0;
        rigBee.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
			if (check && player != null) {
				deltaX = player.transform.position.x - transform.position.x;
				deltaY = player.transform.position.y - transform.position.y;
            if (setAngle)
            {
                transform.localScale = new Vector3(Mathf.Sign(deltaX) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-Mathf.Sign(deltaX) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
				
				if (deltaY < -playerDelta) {
					if (move)
						Move ();

				} else
					rigBee.linearVelocity = new Vector2 (rigBee.linearVelocity.x, speed / 1.5f);
				if (damage.getHit) {
					move2 = false;
					Move2 (limit1);
				}
				if (move2) {
					if (deltaX * deltaX + deltaY * deltaY > limit2) {
						move = true;
					}
				} else if (deltaX * deltaX + deltaY * deltaY > limit1) {
					move = true;
				}
			} 

	}

	// Bee chuyen dong cong lao xuong player
	void Move ()
	{
		float rad2 = Mathf.Atan ((player.transform.position.y + playerDelta - transform.position.y) / deltaX);
		if (deltaX < 0)
			rad2 = rad2 - Mathf.PI;
		rad = Mathf.Lerp (rad, rad2, delta);
		xSpeed = speed * Mathf.Cos (rad);
		ySpeed = speed * Mathf.Sin (rad);
		rigBee.linearVelocity = new Vector2 (xSpeed, ySpeed);
	}

	// khi bee cham player thi cho bee la doi tuong con cua player
	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject == player) {
			if (!damage.getHit){
			move = false;
			move2 = true;
			Move2 (20);    
            }
            else {
				move = false;
				move2 = false;
				Move2 (limit1);
            }
		}

        if(coll.tag == "WeaponPlayer")
        {
            move = false;
            move2 = false;
            Move2(limit1);
        }
    }

	// khi bj Player dap thi Bee bi ban' laj.
	void Move2(float limit )
	{
		ySpeed = speedMove2;
		if (deltaX > 0)
			xSpeed = -speedMove2;
		else
			xSpeed = speedMove2;
		rigBee.linearVelocity = new Vector2 (xSpeed, ySpeed);


		rad = -Mathf.PI / 2;
	}

}
