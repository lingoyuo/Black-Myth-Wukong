using UnityEngine;
using System.Collections;

public class DoorEndProcess : MonoBehaviour {

    public GameObject MoblieController;
    public float distance = 1.5f;
    public GameObject player;

    public AudioClip audio_door_open;
    public AudioClip audio_door_close;

    private SMB_doorClose smb_door_close;
    private SMB_doorOpen smb_door_open;
    private AudioSource audio_source;

    Animator anim;
    bool FinishLevel;
    float timer;

    // Component player
    Animator player_anim;
    Rigidbody2D player_rid;
    PlayerController player_contr;
    GameManager gameManager;

    bool door_open;

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
        if (!MoblieController)
            Debug.Log("Door end error null obj !!");

        if (!player)
            Debug.LogError("Player not found in end door obj !!");

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        anim.speed = 10f;

        // Caching data
        player_anim = player.GetComponent<Animator>();
        player_rid = player.GetComponent<Rigidbody2D>();
        player_contr = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if(!door_open)
            EndDoor();       
    }

    void EndDoor()
    {      
        if (Vector3.Distance(player.transform.position, transform.position) < distance)
        {
            door_open = true;
            anim.SetTrigger("open");
        }      
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && !FinishLevel && gameManager.state != GameManager.StateGame.Losing)
        {
            // player enter door end, we need active door end and disable interact input from use

            player.tag = "Untagged";                                                // Fix bug enemy chase player when enter door

            player_anim.SetTrigger("enter_door");
            player_contr.enabled = false;
            player_rid.linearVelocity = Vector2.zero;
            player_rid.isKinematic = true;         

            player.transform.SetParent(null);
            anim.speed = 1.0f;
            anim.SetTrigger("close");
            FinishLevel = true;
            MoblieController.SetActive(false);

            // Unlock level , setStar and change state GameManager
            gameManager.GetComponent<GameManager>().state = GameManager.StateGame.Ending;
        }

        if (FinishLevel)
        {
            timer += Time.deltaTime;
            Destroy(player, 3.0f);
        }
    }
}
