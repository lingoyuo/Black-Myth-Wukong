using UnityEngine;
using System.Collections;

public class DebugSpawnPoint : MonoBehaviour {

    public Transform posSpawnPlayer;
    public GameObject doorStart;
    public Transform player;
    public bool isBossMap = false;
    public bool editor = true;

	void Awake()
    {
#if (UNITY_EDITOR)
        if (!player || !doorStart)
        {
            print("error : " + gameObject);
            return;
        }

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().firstState = GameManager.StateGame.Playing;
        player.GetComponent<PlayerCombatSystem>().Debug = true;
        doorStart.SetActive(false);
        player.gameObject.SetActive(true);
        player.GetComponent<PlayerController>().EditorTest = editor;
        var offset = Camera.main.transform.position - player.transform.position;

        player.transform.position = posSpawnPlayer.position;

        if(!isBossMap)
            Camera.main.transform.position = player.transform.position + offset;
#endif
    }

}
