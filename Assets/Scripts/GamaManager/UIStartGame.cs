using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIStartGame : MonoBehaviour {

    public GameObject DoorManager;
    public float TimeLoading = 1.5f;

    private Animator anim_UI;
    private float timer;

    void Awake()
    {
        DoorManager.SetActive(false);

        anim_UI = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > TimeLoading)
        {
            anim_UI.SetTrigger("appear");
            if (timer > TimeLoading + 1.0f)
            {
                DoorManager.SetActive(true);
            }
        }
    }
}
