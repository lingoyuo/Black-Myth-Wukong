using UnityEngine;
using System.Collections;

public class SpringManager : MonoBehaviour {

    public float Force = 500.0f;

    private Animator anim;
    private Rigidbody2D rid;
    private AudioSource audio_source;

    void Start()
    {
        anim = GetComponent<Animator>();
        audio_source = GetComponent<AudioSource>();
    }


	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            rid = other.GetComponent<Rigidbody2D>();
            if (other.transform.position.y > transform.position.y + 0.2f && rid.linearVelocity.y <=0.1f)
            {
                anim.SetTrigger("active");

                // Play audio effect
                if (!audio_source.isPlaying)
                    AudioManager.Instances.PlayAudioEffect(audio_source);

                other.GetComponent<PlayerController>().grounded_spring = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            other.GetComponent<PlayerController>().grounded_spring = false;
    }

    /// <summary>
    /// Animation call
    /// </summary>
    void Active()
    {
        rid.AddForce(Vector3.up * Force);
    }
}
