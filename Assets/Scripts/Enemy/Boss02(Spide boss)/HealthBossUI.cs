using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthBossUI : MonoBehaviour {

    private int current_health;
    public GameObject piece_health;

    private RectTransform bg_l;
    private RectTransform bg_r;

    private float width;

    private GameObject[] list_piece_healths;

    private RectTransform parent;
    private Transform parent_parent;


    private const float const_widlth = 0.46f;

    void Start()
    {
        parent = transform.parent.GetComponentInParent<RectTransform>();
        parent_parent = transform.parent.parent;

        current_health = (int) transform.GetComponentInParent<InforStrength>().Get_Health;

        bg_l = transform.GetChild(0).GetComponent<RectTransform>();
        bg_r = transform.GetChild(1).GetComponent<RectTransform>();
 
        //       width = bg_l.rect.width;
        width = const_widlth;
        
        if (current_health < 2)
            print("error! number health can't not less than two");

        list_piece_healths = new GameObject[current_health];

        list_piece_healths[0] = bg_l.gameObject;
        list_piece_healths[current_health - 1] = bg_r.gameObject;

        for (int i = 1; i < current_health - 1; i++)
        {
            list_piece_healths[i] = (GameObject)Instantiate(piece_health);
            list_piece_healths[i].transform.SetParent(gameObject.transform);
            list_piece_healths[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }

        if (current_health >= 2)
        {

            if (current_health % 2 == 0)
            {
                for (int i = 0; i < current_health / 2; i++)
                    list_piece_healths[i].GetComponent<RectTransform>().localPosition = new Vector2(-(current_health - 1 - 2 * i) * width / 2, 0);
                for (int i = current_health / 2; i < current_health; i++)
                    list_piece_healths[i].GetComponent<RectTransform>().localPosition = new Vector2(-(current_health - 1 - 2 * i) * width / 2, 0);
            }
            else
            {
                for (int i = 0; i < (current_health - 1) / 2; i++)
                {
                    list_piece_healths[i].GetComponent<RectTransform>().localPosition = new Vector2(-(current_health - 1 - 2 * i) * width / 2, 0);
                }
                for (int i = (int)(current_health / 2) + 1; i < current_health; i++)
                {
                    list_piece_healths[i].GetComponent<RectTransform>().localPosition = new Vector2(-(current_health - 1 - 2 * i) * width / 2, 0);
                }
                list_piece_healths[(int)(current_health / 2)].GetComponent<RectTransform>().localPosition = Vector2.zero;
            }
        }
    }

    void Update()
    {
        parent.localScale = new Vector3(parent.localScale.x * Mathf.Sign(parent_parent.localScale.x), parent.localScale.y * Mathf.Sign(parent_parent.localScale.y), parent.localScale.z * Mathf.Sign(parent_parent.localScale.z));

    }

    public void LostHealth(int number_health_lost)
    {
        for(int i = current_health - 1; i > current_health - 1 - number_health_lost; i--)
        {
            list_piece_healths[i].GetComponentInChildren<Animator>().SetTrigger("disappear");
        }

        current_health = current_health - number_health_lost;
    }
}
