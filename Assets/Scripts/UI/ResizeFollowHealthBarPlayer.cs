using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResizeFollowHealthBarPlayer : MonoBehaviour {

    public RectTransform health_UI;
    public RectTransform follow_health;

    private float offset;

    void Update()
    {
        offset = follow_health.localPosition.x - health_UI.localPosition.x + 10.0f;
        health_UI.sizeDelta = new Vector2(offset, health_UI.rect.size.y);
    }
}
