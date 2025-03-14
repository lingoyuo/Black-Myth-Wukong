using UnityEngine;
using System.Collections;

public class DebugBossTest : MonoBehaviour {

    public GameObject player;

    void Start()
    {
        player.GetComponent<PlayerController>().EditorTest = true;
    }
}
