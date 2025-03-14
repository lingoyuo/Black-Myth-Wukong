using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SnapGround : MonoBehaviour {
	
	public float cell_sizeX = 1f; // = larghezza/altezza delle celle
	public float cell_sizeY = 1f;
	private float x, y, z;
	
	void Start() {
		x = 0f;
		y = 0f;
		z = 0f;
		
	}
	
	void Update () {
		x = Mathf.Round(transform.position.x / cell_sizeX) * cell_sizeX;
		y = Mathf.Round(transform.position.y / cell_sizeY) * cell_sizeY;
		z = transform.position.z;
		transform.position = new Vector3(x, y, z);
	}
	
}
