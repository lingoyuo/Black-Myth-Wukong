using UnityEngine;
using System.Collections;

public class SilkSpideBoss : MonoBehaviour {

    public float speedSlow = 1.0f;
    public float timeAdd = 3.0f;

    private float timeDisappear = 6.5f;

    float orinalSpeed;

    bool active;                        // Only change speed player if active = true
    bool slowPlayer;                    // Change speed player when slowPlayer = true

    float timer_bind;
    float timer_live;

    PlayerController speedPlayer;

    void Update()
    {
        timer_live += Time.deltaTime;

        if(slowPlayer)
        { 
            speedPlayer.DefaultPhysics.maxSpeed = speedSlow;
        }

        if(timer_live > timeDisappear)
        {
            gameObject.transform.SetParent(null);
            if (slowPlayer)
            {
                speedPlayer.DefaultPhysics.maxSpeed = 8;
            }
            timer_live = 0;
            timer_bind = 0;      
            active = false;
            slowPlayer = false;
            timeDisappear = 6.5f;
            gameObject.SetActive(false);
        }
    }

	void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            speedPlayer = other.gameObject.GetComponent<PlayerController>();
            orinalSpeed = speedPlayer.DefaultPhysics.maxSpeed;
            speedPlayer.DefaultPhysics.maxSpeed = speedSlow;
            gameObject.transform.SetParent(other.transform,false);

            slowPlayer = true;

            // bind silk to player in local position (0,0.5f)
            gameObject.transform.localPosition = new Vector2(0, 0.5f);
            active = true;

            timeDisappear = timeDisappear + timeAdd;
        }
    }
}
