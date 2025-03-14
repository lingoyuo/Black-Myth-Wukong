using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaterEnviromentEffectPlayer : MonoBehaviour {

    const float TIME_DISSAPPEAR = 5.0f;

    public float speedOxy =20f;

    public float DamagePerSec = 2.0f;


    PlayerController player;
    Slider slider;
    float timer;

    void Start()
    {
        slider = GetComponent<Slider>();

        if (GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }


    void Update()
    {

        // Disappear water cooldown
        if(slider.value == 100)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        if(timer > TIME_DISSAPPEAR)
        {
            gameObject.SetActive(false);
        }

        Check();
        
    }

    void Check()
    {
        var boxPlayer = player.GetComponent<BoxCollider2D>().size.y ;

        var posStartRay = new Vector2(player.transform.position.x, player.transform.position.y + boxPlayer);

        RaycastHit2D hit = Physics2D.Raycast(posStartRay, -player.transform.up, boxPlayer/2, 1 << 4);

        if(hit)
            slider.value = Mathf.Clamp(slider.value - Time.deltaTime * speedOxy, 0, 100);
        else
            slider.value = Mathf.Clamp(slider.value + Time.deltaTime * speedOxy, 0, 100);

        if (slider.value == 0)
            DamagePlayer();

    }

    void DamagePlayer()
    {
        player.GetComponent<InforStrength>().LoseHealth(Time.deltaTime * DamagePerSec);
    }

}
