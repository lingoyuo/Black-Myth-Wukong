using UnityEngine;
using System.Collections;

public class DoorStartProcess : MonoBehaviour {


    public GameObject player;
    public GameObject MobileController;

    public AudioClip audio_door_open;
    public AudioClip audio_door_close;

    private SMB_doorClose smb_door_close;
    private SMB_doorOpen smb_door_open;
    private AudioSource audio_source;

    Animator anim;
	bool opened;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        audio_source = GetComponent<AudioSource>();

        smb_door_open = anim.GetBehaviour<SMB_doorOpen>();
        smb_door_open.clip = audio_door_open;
        smb_door_open.source = audio_source;

        smb_door_close = anim.GetBehaviour<SMB_doorClose>();
        smb_door_close.clip = audio_door_close;
        smb_door_close.source = audio_source;

    }

    void Start()
    {               
        anim.SetTrigger("open");     

        if (!player || !MobileController)
        {
            Debug.Log("Door start error null empty obj!!!");
            Application.Quit();
        }
    }

    void Update()
    {
        StartDoor();
    }

    void StartDoor()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("doorOpened") && !opened)
        {
            player.SetActive(true);
            MobileController.SetActive(true);
			opened = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            anim.SetTrigger("close");
        }
    }

}
