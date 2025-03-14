using UnityEngine;
using System.Collections;

public class SMB_doorClose : StateMachineBehaviour {
    [HideInInspector]
    public DoorManager doorManager;
    public AudioClip clip;
    public AudioSource source;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //doorManager._stateDoor = DoorManager.StateDoor.closing;
        source.clip = clip;
		AudioManager.Instances.PlayAudioEffect (source);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // doorManager._stateDoor = DoorManager.StateDoor.closed;
    }

}
