using UnityEngine;
using System.Collections;

public class Turtle : MonoBehaviour {
	public float pointMax;
	public float speed;
	public bool huog;
	
	Vector3 point0;
	Rigidbody2D rigid;
	//DamagePlayer damege;

	void Awake ()
	{
		rigid = GetComponent<Rigidbody2D> ();
		//damege = GetComponent<DamagePlayer> ();
	}
	
	void Start ()
	{
		point0 = transform.position;
		if (huog)
			speed = Mathf.Abs (speed);
		else
			speed = -Mathf.Abs (speed);
	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.position.x - point0.x) > pointMax ) {
			speed = -Mathf.Abs (speed);;

		} else if ((transform.position.x - point0.x) < -pointMax ) {
			speed = Mathf.Abs (speed);

		}

		Flip ();
		MoveX ();

	}
	
	void MoveX ()
	{
		rigid.linearVelocity = new Vector2 (speed, 0);
	}
	void Flip ()
	{
		if (speed > 0)
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
		else transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
	}

    void OnDrawGizmosSelected()
    {
        point0 = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(point0.x - pointMax, point0.y, point0.z), new Vector3(point0.x + pointMax, point0.y, point0.z));
    }

}

