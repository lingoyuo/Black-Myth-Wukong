using UnityEngine;
using System.Collections;

public class MakeWaveWater : MonoBehaviour {

    public float SpeedWave = 2.0f;

    private Water water;
    private Transform Obj;
    private Vector3 startPos;
    private Rigidbody2D obj_rig;

    private float timer;

    void Start()
    {
        Obj = transform.GetChild(0);
        obj_rig = Obj.GetComponent<Rigidbody2D>();
        startPos = Obj.localPosition;
        water = GetComponent<Water>();

    }

    void Update()
    {
        if(Obj.localPosition.y < 0.2f)
        {
            timer += Time.deltaTime;

            if (timer > SpeedWave)
            {
                timer = 0;
                obj_rig.linearVelocity = Vector2.zero;
                Obj.localPosition = new Vector2(Random.Range(0.1f, water.Widlth), startPos.y);
            }
        }
    }
}
