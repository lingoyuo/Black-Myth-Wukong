using UnityEngine;
using System.Collections;

public class FrameThrowFromVolcano : MonoBehaviour {

    public float TimeSpawn = 3.0f;
    public GameObject Effect;

    private GameObject Frame;
    private Transform posStart;
    private Transform posEnd;
    private GameObject effect;

    private ParabolMove target;

    private float timer;

    void Start()
    {
        Frame = transform.GetChild(0).gameObject;
        posStart = transform.GetChild(1);
        posEnd = transform.GetChild(2);

        target = Frame.GetComponent<ParabolMove>();

        effect = (GameObject)Instantiate(Effect);
        effect.SetActive(false);

        Frame.SetActive(false);
    }

    void Update()
    {
        if (!Frame.activeSelf)
            timer += Time.deltaTime;
        else
            CheckFrame();

        if(timer > TimeSpawn)
        {
            timer = 0;
            SpawnFrame();
        }

        if (effect.activeSelf)
            CheckEffect();
    }

    void SpawnFrame()
    {
        Frame.SetActive(true);
        Frame.transform.position = posStart.position;
        target.target = posEnd.transform.position;
        target.InitialPos();
    }

    void CheckFrame()
    {
        if(Frame.transform.position.y < posEnd.transform.position.y)
        {
            Frame.transform.position = posStart.transform.position;          
            Frame.SetActive(false);

            effect.SetActive(true);
            effect.transform.position = posEnd.position;
            effect.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void CheckEffect()
    {
        if(timer > 1.0f)
        {
            effect.transform.GetChild(0).gameObject.SetActive(false);
            effect.SetActive(false);
        }
    }

}
