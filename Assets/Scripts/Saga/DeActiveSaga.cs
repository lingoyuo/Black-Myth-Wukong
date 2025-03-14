using UnityEngine;
using System.Collections;

public class DeActiveSaga : MonoBehaviour
{
    public GameObject levelSaga;
    public SagaControl sagaControl;
    LevelSaga[] saga = new LevelSaga[20];

    void Start()
    {
        saga = levelSaga.GetComponentsInChildren<LevelSaga>();
    }
    public void DeActive()
    {
        sagaControl.enabled = false;
        foreach(LevelSaga level in saga)
        {
            CheckLevel(level, false);
        }
    }
   
    public void Active()
    {
        foreach (LevelSaga level in saga)
        {
            CheckLevel(level, true);
        }
        sagaControl.enabled = true;
    }

    void CheckLevel(LevelSaga level, bool unLock)
    {
        if (level.unLock)
        {
            level.gameObject.GetComponent<BoxCollider2D>().enabled = unLock;
        }
    }
}
