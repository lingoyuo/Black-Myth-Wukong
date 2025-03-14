using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class AddPlayerToEventTriggger : MonoBehaviour
{

	PlayerController player_control;
	PlayerCombatSystem player_combat;

	EventTrigger[] eventList;

	void Start ()
	{
		player_control = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		player_combat = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerCombatSystem> ();

		eventList = new EventTrigger[4];

		eventList [0] = transform.GetChild (0).GetComponent<EventTrigger> ();
		//CallEvent (eventList [0], new UnityEngine.Events.UnityAction<BaseEventData> (player_control.MoveLeft), EventTriggerType.PointerEnter);
		//CallEvent (eventList [0], new UnityEngine.Events.UnityAction<BaseEventData> (player_control.Release), EventTriggerType.PointerExit);
		//CallEvent (eventList [0], new UnityEngine.Events.UnityAction<BaseEventData> (player_control.Release), EventTriggerType.PointerUp);

		//eventList [1] = transform.GetChild (1).GetComponent<EventTrigger> ();
		//CallEvent (eventList [1], new UnityEngine.Events.UnityAction<BaseEventData> (player_control.MoveRight), EventTriggerType.PointerEnter);
		//CallEvent (eventList [1], new UnityEngine.Events.UnityAction<BaseEventData> (player_control.Release), EventTriggerType.PointerExit);
		//CallEvent (eventList [1], new UnityEngine.Events.UnityAction<BaseEventData> (player_control.Release), EventTriggerType.PointerUp);

		//eventList [2] = transform.GetChild (2).GetComponent<EventTrigger> ();
		//CallEvent (eventList [2], new UnityEngine.Events.UnityAction<BaseEventData> (player_control.Jump), EventTriggerType.PointerEnter);
		//CallEvent (eventList [2], new UnityEngine.Events.UnityAction<BaseEventData> (player_control.JumpRelease), EventTriggerType.PointerExit);
		//CallEvent (eventList [2], new UnityEngine.Events.UnityAction<BaseEventData> (player_control.JumpRelease), EventTriggerType.PointerUp);

		//eventList [3] = transform.GetChild (3).GetComponent<EventTrigger> ();
		//CallEvent (eventList [3], new UnityEngine.Events.UnityAction<BaseEventData> (player_combat.Attack), EventTriggerType.PointerEnter);
		//CallEvent (eventList [3], new UnityEngine.Events.UnityAction<BaseEventData> (player_combat.AttackRelease), EventTriggerType.PointerExit);
		//CallEvent (eventList [3], new UnityEngine.Events.UnityAction<BaseEventData> (player_combat.AttackRelease), EventTriggerType.PointerUp);

	}

	

    void CallEvent (EventTrigger event_call, UnityEngine.Events.UnityAction<BaseEventData> func_call, UnityEngine.EventSystems.EventTriggerType type)
	{
		EventTrigger.Entry entry = new EventTrigger.Entry ();
		entry.eventID = type;
		UnityEngine.Events.UnityAction<BaseEventData> _call = func_call;
		entry.callback.AddListener (_call);
		event_call.triggers.Add (entry);
	}

}
