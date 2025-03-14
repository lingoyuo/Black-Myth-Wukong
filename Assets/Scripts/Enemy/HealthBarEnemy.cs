using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour {
    public GameObject sliderHealth;
    InforStrength enemy;

    float MaxHealth;
    float curenHealth;

    public void Intial ()
    {
        enemy = transform.GetComponentInParent<InforStrength>();

        MaxHealth = enemy.MaxHealth;
        curenHealth = MaxHealth;
    }

    void Update()
    {
        if (enemy.Get_Health < curenHealth)
        {
            ScaleHealth();
        }

        if (enemy.Get_Health <= 0)
            gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        if (transform.parent.localScale.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (transform.parent.localScale.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void ScaleHealth()
    {
        float scale = Mathf.Clamp(enemy.Get_Health, 0, MaxHealth) / curenHealth;
        sliderHealth.transform.localScale = new Vector3(sliderHealth.transform.localScale.x * scale, sliderHealth.transform.localScale.y, sliderHealth.transform.localScale.z);
        curenHealth = Mathf.Clamp(enemy.Get_Health, 0, MaxHealth);
    }
}
