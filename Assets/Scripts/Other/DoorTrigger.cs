using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

    DoorManager doorManager;

    void Start()
    {
        doorManager = GameObject.FindObjectOfType<DoorManager>();
    }

	void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")        
            doorManager._statePlayer = DoorManager.StatePlayer.exitDoor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
            doorManager._statePlayer = DoorManager.StatePlayer.enterDoor;
    }

}
