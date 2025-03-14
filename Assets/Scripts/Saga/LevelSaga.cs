using UnityEngine;
using System.Collections;

public class LevelSaga : MonoBehaviour {
    
    public static bool click = true;
    public int level;
    public SpriteRenderer unLook;
    public SpriteRenderer starRenderer;
    public SetIntLevel levelSprite;
    public Sprite[] SpriteUnLook;
    public Sprite[] SpriteStar;
    public bool unLock = false;

    int prefStarLevel;
    public int prefSagaLevel;
    int prefPlayLevel;
    SagaControl saga;
    Animator animatorLevel;
    BoxCollider2D boxLevel;
    LocalAccessValue localSaga;

    void Awake ()
    {
        localSaga = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LocalAccessValue>();
        saga = GetComponentInParent<SagaControl>();
        animatorLevel = GetComponent<Animator>();
        boxLevel = GetComponent<BoxCollider2D>();
    }
    // Use this for initialization
    void Start () {
        prefSagaLevel = localSaga.GetTotalLevelUnlock();
        prefPlayLevel = localSaga.GetCurrentLevel();
        if (level >= prefSagaLevel)
            localSaga.SetStarLevel(level, 0);
        prefStarLevel = localSaga.GetStar(level);
        SetLevel();
    }

    public void SetLevel()
    {
        if (prefSagaLevel >= level)
            UnLookLevel();
        else LookLevel();
        if ( prefSagaLevel == level )
        {
            animatorLevel.SetTrigger("Unlook");
        }
        
    }

    public void UnLookLevel ()
    {
        unLock = true;
        unLook.sprite = SpriteUnLook[0];
        levelSprite.SetInt(level);
        Star(prefStarLevel);
        boxLevel.enabled = true;
    }

    public void LookLevel ()
    {
        unLock = false;
        unLook.sprite = SpriteUnLook[1];
        levelSprite.SetInt(0);
        Star(0);
        boxLevel.enabled = false;
    }

    public void Star (int starLevel)
    {
        if (starLevel == 0)
            starRenderer.enabled = false;
        else
        {
            starRenderer.enabled = true;
            starRenderer.sprite = SpriteStar[starLevel - 1];
        }
    }

    void OnMouseUpAsButton()
    {
        if (unLook.sprite == SpriteUnLook[0] && click)
        {
            animatorLevel.SetTrigger("Click");
            if (level == prefPlayLevel)
            {
                saga.PlayLevelIdle();
            }
            else if (level - prefPlayLevel == 1)
            {
                saga.MovePlayerNextLevel(prefPlayLevel.ToString());
            }
            else
            {
                saga.MoveLevel(gameObject);
            }
            localSaga.SetCurrentLevel(level);
            saga.CameraLerpPosition(transform.position.x);
            saga.speedLerp *= 0.2f;
            click = false;
            Invoke("SetClick", 2.5f);
        }
    }

    void SetClick()
    {
        click = true;

        if (level < 10)
            Application.LoadLevel("Level0" + level);
        else
            Application.LoadLevel("Level" + level);
    }

   
}
