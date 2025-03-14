using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// This script for health player
// Algorithm : Use follow_health move according to proportion current health and max health player
public class UIPlayerHealth : MonoBehaviour
{
    public InforStrength player_health;
    public float SpeedDecreaseHealth = 7.0f;

    public RectTransform health_UI;
    public RectTransform follow_health;

    [HideInInspector]
    bool playerDead;

    float Max_size_health_bar;                              // max distance
    float current_size_health_bar;                          // current distance

    float MAX_health;                                       // max_health
    float current_health;                                   // current health

    float offset;                                           // distance from follow_health and health_ui

    void Start()
    {
        if(player_health == null)
        {
            Debug.LogError("Can not allow player null!");
            return;
        }

        // health bar
        Max_size_health_bar = follow_health.localPosition.x - health_UI.localPosition.x;
        current_size_health_bar = Max_size_health_bar;
        
        // player health
        MAX_health = player_health.MaxHealth;
        current_health = MAX_health;

    }

    void Update()
    {
        current_health = player_health.Get_Health;                                                  // get current health
        current_size_health_bar = Max_size_health_bar * current_health / MAX_health;            // caculate distance offset
        offset = current_size_health_bar;                                                       // this distance will move to

        // move follow health if distance offset not equal real distance 
        if(current_size_health_bar <= Max_size_health_bar)
        {
            if (follow_health.localPosition.x != health_UI.localPosition.x + offset + 10 && current_health >=0)
                follow_health.localPosition = new Vector2(Mathf.MoveTowards( follow_health.localPosition.x, health_UI.localPosition.x + offset + 50, Time.deltaTime * 500),follow_health.localPosition.y);
        }

        ResizeHealthBar();
    }

    // Resize health according to offset
    void ResizeHealthBar()
    {
        offset = follow_health.localPosition.x - health_UI.localPosition.x+20;
        health_UI.sizeDelta = new Vector2(offset, health_UI.rect.size.y);
    }
}
