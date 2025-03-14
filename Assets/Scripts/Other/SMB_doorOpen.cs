using UnityEngine;
using System.Collections;

public class SMB_doorOpen : StateMachineBehaviour {


    [HideInInspector]
    public DoorManager doorManager;
    public AudioClip clip;
    public AudioSource source;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (doorManager)
        {
            doorManager._stateDoor = DoorManager.StateDoor.opening;          
        }
        // Play audio
        source.clip = clip;
		AudioManager.Instances.PlayAudioEffect (source);

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(doorManager)
            doorManager._stateDoor = DoorManager.StateDoor.opened;
    }
}
