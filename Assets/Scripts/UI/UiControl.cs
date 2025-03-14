using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiControl : MonoBehaviour
{
	public GameObject left1;
	public GameObject left2;
	public GameObject right1;
	public GameObject right2;
	public GameObject jump1;
	public GameObject jump2;
	public GameObject attack1;
	public GameObject attack2;

	public void Left1 ()
	{
		left1.SetActive (false);
		left2.SetActive (true);
	}

	public void Left2 ()
	{
		left2.SetActive (false);
		left1.SetActive (true);
	}

	public void Right1 ()
	{
		right1.SetActive (false);
		right2.SetActive (true);
	}

	public void Right2 ()
	{
		right2.SetActive (false);
		right1.SetActive (true);
	}

	public void Jump1 ()
	{
		jump1.SetActive (false);
		jump2.SetActive (true);
	}

	public void Jump2 ()
	{
		jump2.SetActive (false);
		jump1.SetActive (true);
	}

	public void Attack1 ()
	{
		attack1.SetActive (false);
		attack2.SetActive (true);
	}

	public void Attack2 ()
	{
		attack2.SetActive (false);
		attack1.SetActive (true);
	}
}
