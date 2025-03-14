using UnityEngine;
using System.Collections;

public class SetIntLevel : MonoBehaviour {
    public SpriteRenderer sprite1;
    public SpriteRenderer sprite2;
    public SpriteRenderer sprite3;

    [Header("Mang So'")]
    public Sprite[] SpriteLevel;
    // Use this for initialization
    public void SetInt (int level)
    {
        if (level <= 0)
        {
            sprite1.gameObject.SetActive(false);
            sprite2.gameObject.SetActive(false);
            sprite3.gameObject.SetActive(false);
        }
        else if (level < 10)
        {
            sprite1.gameObject.SetActive(true);
            sprite2.gameObject.SetActive(false);
            sprite3.gameObject.SetActive(false);
            sprite1.sprite = SpriteLevel[level];
        }
        else
        {
            sprite1.gameObject.SetActive(false);
            sprite2.gameObject.SetActive(true);
            sprite3.gameObject.SetActive(true);
            sprite2.sprite = SpriteLevel[level / 10];
            sprite3.sprite = SpriteLevel[level % 10];
        }
    }
}
