using UnityEngine;
using System.Collections;

public class SpawnRandom : MonoBehaviour {

    public GameObject[] items;

    public float[] percentages;

	void Start()
    {

        for (int i = 0; i < items.Length; i++)
            SpawnItem(items[i], percentages[i]);
    }

    void SpawnItem(GameObject item, float percent)
    {
        float rand = Random.Range(0, 100.0f);

        if(rand < percent)
                item.SetActive(true);
        else
            item.SetActive(false);
    }
}
