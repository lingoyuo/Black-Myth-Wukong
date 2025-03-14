using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhysicVolume : MonoBehaviour {

    public ControlPhysics overritePhysics;

    GameObject UIPlayerHealth;
    GameObject UICooldown;

    SpriteRenderer sprite_player;

    void Start()
    {
        UIPlayerHealth = GameObject.FindGameObjectWithTag("PlayerHealthUI");

        if (UIPlayerHealth)
        {
            Slider[] arr = UIPlayerHealth.GetComponentsInChildren<Slider>(true);
            foreach (Slider i in arr)
            {
                if (i.gameObject.activeSelf == false)
                {
                    UICooldown = i.gameObject;
                }
            }
        }
    }

    // Get component sprite render player when player enter water ( tip: help avoid grabage collection)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().AudioWaterHitPlay();
            sprite_player = other.GetComponent<SpriteRenderer>();
            sprite_player.sortingLayerName = "Default";
            other.gameObject.layer = 0;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && UICooldown)
        {
            UICooldown.SetActive(true);        
        } 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            sprite_player = other.GetComponent<SpriteRenderer>();
            sprite_player.sortingLayerName = "Player";
            other.gameObject.layer = 9;
        }
    }
}
