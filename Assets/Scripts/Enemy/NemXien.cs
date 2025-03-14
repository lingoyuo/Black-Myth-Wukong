using UnityEngine;
using System.Collections;

public class NemXien : MonoBehaviour {
	public GameObject player;
	public float maxSpeed = 5f;
	public float gocDo = 45;
	public float giaToc =1f ;

	Rigidbody2D rig;
	float speed;
	float gocRad;
	float tan;
	float cos;
	float sin;
	float deltaX;
	float deltaY;
	float time;

	void Awake ()
	{
		rig = GetComponent<Rigidbody2D> ();
	}
	// Use this for initialization
	void Start () {
		time = 0;
		deltaX = player.transform.position.x - transform.position.x;
		deltaY = player.transform.position.y - transform.position.y;
		if (deltaX < 0)
			gocDo = 180 - gocDo;
		gocRad = gocDo * Mathf.PI / 180;
		tan = Mathf.Tan (gocRad);
		cos = Mathf.Cos (gocRad);
		sin = Mathf.Sin (gocRad);

		Set ();
	}
	
	// Update is called once per frame
	void Update () {
		if (speed < maxSpeed)
			Move ();
	
	}
	void Move ()
	{
		rig.linearVelocity = new Vector2 (speed * cos, speed * sin - giaToc * time);
		time += Time.deltaTime;
	}

	void Set()
	{
		speed = Mathf.Sqrt (deltaX * deltaX * giaToc / 2 / (deltaX * tan - deltaY)) / Mathf.Abs (cos);
	}
}
