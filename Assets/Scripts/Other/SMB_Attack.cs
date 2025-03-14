using UnityEngine;
using System.Collections;

public class SMB_Attack : StateMachineBehaviour {

    [HideInInspector] public AudioSource audio_attack;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		animator.transform.GetChild (1).gameObject.GetComponent<PolygonCollider2D> ().enabled = true;

		AudioManager.Instances.PlayAudioEffect (audio_attack);

        animator.gameObject.GetComponent<PlayerCombatSystem>().attacking = true;

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		animator.transform.GetChild (1).gameObject.GetComponent<PolygonCollider2D> ().enabled = false;
        animator.gameObject.GetComponent<PlayerCombatSystem>().attacking = false;

    }

}
