using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour {
    public SpriteRenderer[] Sprite;
    [Header("Mang So'")]
    public Sprite[] Score;

    Animation anima;
    void Awake () {
        anima = GetComponent<Animation>();
	}

    public void SetScoreText (int score)
    {
        SetScode(score);
        anima.Play();

        StartCoroutine(Diactive());
    }

    IEnumerator Diactive()
    {
        yield return new WaitForSeconds(1.1f);
        gameObject.SetActive(false);
    }

    void SetScode (int score)
    {
        if (score <=0)
        {
            foreach(SpriteRenderer sprite in Sprite)
            {
                sprite.gameObject.SetActive(false);
            }
        }
        else if (score < 10)
        {
            Sprite[0].gameObject.SetActive(true);
            Sprite[1].gameObject.SetActive(false);
            Sprite[2].gameObject.SetActive(false);
            Sprite[3].gameObject.SetActive(false);
            Sprite[4].gameObject.SetActive(false);
            Sprite[5].gameObject.SetActive(false);

            Sprite[0].sprite = Score[score];
        }
        else if (score < 100)
        {
            Sprite[0].gameObject.SetActive(false);
            Sprite[1].gameObject.SetActive(true);
            Sprite[2].gameObject.SetActive(true);
            Sprite[3].gameObject.SetActive(false);
            Sprite[4].gameObject.SetActive(false);
            Sprite[5].gameObject.SetActive(false);

            Sprite[1].sprite = Score[score / 10];
            Sprite[2].sprite = Score[score % 10];
        }
        else if (score < 1000)
        {
            Sprite[0].gameObject.SetActive(false);
            Sprite[1].gameObject.SetActive(false);
            Sprite[2].gameObject.SetActive(false);
            Sprite[3].gameObject.SetActive(true);
            Sprite[4].gameObject.SetActive(true);
            Sprite[5].gameObject.SetActive(true);

            Sprite[3].sprite = Score[score / 100];
            Sprite[4].sprite = Score[(score % 100) / 10];
            Sprite[5].sprite = Score[(score % 100) % 10];
        }
    }

    void OnEnable()
    {
        SetScoreText(134);
    }
}
