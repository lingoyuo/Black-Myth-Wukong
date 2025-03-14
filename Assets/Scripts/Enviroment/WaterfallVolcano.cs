using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterfallVolcano : MonoBehaviour {

    [Space(10)]
    [Header("Object instance")]
    //public GameObject Platform;
    public GameObject ParticleEffect;
    public GameObject Frame;

    [Space(10)]
    [Header("Setting:")]
    //public float speedPlatform = 2.0f;
    //public float timeSpawn = 1.0f;

    public float speedFrame = 5.0f;
    public float timeSpawnFrame = 5.0f;

    //private List<GameObject> listPlatfroms;
    private List<GameObject> listParticles;
    private List<GameObject> listFrames;

    private GameObject[] currentFrames;

    private Vector2 limit_left_local;
    private Vector2 limit_right_local;

    private float limit_bottom;
    //private float sizeBoxPlatform;
    private Vector2 posRandom;

    private float timerPlatform;
    private float timerFrame;
    //private GameObject platformPointers;
    private GameObject framePointers;
    private Transform player;

    //// Random [1..5] mode
    //private int mode;

    void Start()
    {
        listParticles = new List<GameObject>();
        listFrames = new List<GameObject>();

        float max_y = GetComponent<BoxCollider2D>().offset.y + GetComponent<BoxCollider2D>().size.y / 2;
        limit_left_local = new Vector2(-GetComponent<BoxCollider2D>().size.x / 2, max_y);
        limit_right_local = new Vector2(-limit_left_local.x, max_y);
        limit_bottom = max_y - GetComponent<BoxCollider2D>().size.y;

        // Firts spawn frame
        framePointers = AddFrame();
        framePointers.SetActive(true);
        framePointers.transform.localPosition = new Vector2(Random.Range(limit_left_local.x, limit_right_local.x), limit_right_local.y);
        framePointers.transform.GetChild(0).gameObject.SetActive(true);
    }

    void Update()
    {
        //timerPlatform += Time.deltaTime;
        timerFrame += Time.deltaTime;

        if(timerFrame > timeSpawnFrame)
        {
            framePointers = GetFrame();
            if (!framePointers)
                framePointers = AddFrame();

            framePointers.SetActive(true);
            
            if(player)
                framePointers.transform.position = new Vector2(player.position.x, transform.TransformPoint(limit_right_local).y);
            else
                framePointers.transform.localPosition = new Vector2(Random.Range(limit_left_local.x, limit_right_local.x), limit_right_local.y);
            framePointers.transform.GetChild(0).gameObject.SetActive(true);

            timerFrame = 0;
        }
    }

    GameObject GetFrame()
    {
        foreach(GameObject frame in listFrames)
            if (!frame.activeSelf)
                return frame;
        return null;
    }

    GameObject AddFrame()
    {
        var frame = Instantiate(Frame);
        frame.transform.SetParent(transform);
        frame.GetComponent<FrameMoveVertical>().SpeedMove = speedFrame;
        frame.GetComponent<FrameMoveVertical>().PosDeactiveLocal = limit_bottom;
        frame.SetActive(false);
        listFrames.Add(frame);
        return frame;
    }

    public GameObject GetParitcalEffect()
    {
        foreach (GameObject particle in listParticles)
            if (!particle.activeSelf)
                return particle;
        return null;
    }

    public GameObject AddParticleEffect()
    {
        var particle = Instantiate(ParticleEffect);
        particle.transform.SetParent(transform);
        particle.SetActive(false);
        listParticles.Add(particle);
        return particle;
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            player = other.transform;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            player = null;
    }
}
