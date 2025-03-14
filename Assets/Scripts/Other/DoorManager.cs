using UnityEngine;
using System.Collections;

public class DoorManager : MonoBehaviour {

    const float TIME_DELAY_DOOR_OPEN_END_GAME = 7.0f;

    public enum StateDoor
    {
        none,
        opening,
        opened,
        closing,
        closed
    }

    public enum StatePlayer
    {
        none,
        enterDoor,
        stayDoor,
        exitDoor
    }

    public Vector2 posDoorOpen = new Vector2(-6.24f, -3.27f);
    public Vector2 posDoorClose = new Vector2(0, -3.27f);

    public GameObject player;
    public GameObject Door;
    public GameObject boss;
    public GameObject MobileController;
   
    InforStrength healthBoss;
    GameObject door;
    SMB_doorClose smb_doorClose;
    SMB_doorOpen smb_doorOpen;
    public StateDoor _stateDoor { set { stateDoor = value; } }
    public StatePlayer _statePlayer { set { statePlayer = value; } }

    StateDoor stateDoor;
    StatePlayer statePlayer;
    bool playerCome;                                                       // Spawn one time player
    float timer;                                                           // Time count open end game
    bool doorSpawnEnd;                                                     // Check spawn door only one time


    void Start()
    {
        stateDoor = StateDoor.none;

        healthBoss = boss.GetComponent<InforStrength>();
    
        DoorSpawn(posDoorOpen);     

        if(!player || !door||!boss||!MobileController)
        {
            print("error nulll exception !!");
            return;
        }
    }

    void Update()
    {

        if (healthBoss.Get_Health <= 0)
        {
            timer += Time.deltaTime;
            if (timer > TIME_DELAY_DOOR_OPEN_END_GAME)
                DoorOpenFinishLevel();
        }
        else
        {
            DoorOpenStartLevel();
        }

    }

    void DoorSpawn(Vector3 pos)
    {
        door = (GameObject) Instantiate(Door, pos, Quaternion.identity);

        SettupDoor(door);
    }

    void SettupDoor(GameObject _door)
    {
        Animator animator_door = _door.GetComponent<Animator>();

        smb_doorOpen = animator_door.GetBehaviour<SMB_doorOpen>();
        smb_doorOpen.doorManager = this;

        smb_doorClose = animator_door.GetBehaviour<SMB_doorClose>();
        smb_doorClose.doorManager = this;

        animator_door.SetTrigger("open");
    }

    /*
    ** Player and moblie controll active when door opened
    */
    void PlayerComeOut(Vector3 pos)
    {
        if (stateDoor == StateDoor.opened && !playerCome)
        {
            player.SetActive(true);

            MobileController.SetActive(true);

            GameObject.FindGameObjectWithTag("PlayerHealthUI").GetComponent<UIPlayerHealth>().enabled = true;           // enable UI health script for player

            playerCome = true;                                                                                          // reset purpose to player come one time

            door.GetComponent<Animator>().SetTrigger("close");         
        }
    }

    void DoorOpenStartLevel()
    {
        PlayerComeOut(posDoorOpen);

        if (statePlayer == StatePlayer.exitDoor)
        {
            statePlayer = StatePlayer.none;                                                                             // Reset door 

            if (door)
                door.GetComponent<Animator>().SetTrigger("disappear");

            Destroy(door, 3.0f);
        }
        
    }

    public void DoorOpenFinishLevel()
    {
        if (!doorSpawnEnd)
        {
            doorSpawnEnd = true;
            DoorSpawn(posDoorClose);
        }
        if (statePlayer == StatePlayer.enterDoor)
        {       
            if (Vector3.Distance(player.transform.position,door.transform.position) < 0.5f)
            {
                statePlayer = StatePlayer.none;
                player.GetComponent<Animator>().SetTrigger("enter_door");
                player.GetComponent<PlayerController>().enabled = false;
                player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                door.GetComponent<Animator>().SetTrigger("close");
                Destroy(player, 3.0f);
            }
        }

        if(!GameObject.FindGameObjectWithTag("Player"))
        {
            print("Finish Level");
        }
    }

}
