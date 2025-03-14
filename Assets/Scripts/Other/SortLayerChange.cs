using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SortLayerChange : MonoBehaviour {

    public int Offset = 1000;

    void Awake()
    {
        if (GetComponent<SpriteRenderer>())
            GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + Offset;

        foreach(SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.sortingOrder = sprite.sortingOrder + Offset;
        }
    }
}
