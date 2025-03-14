using UnityEngine;
using System.Collections;

public class RockEnemy : MonoBehaviour {
    public Transform point1;
    public ParticleSystem effect;

    Rigidbody2D rock;
    // Use this for initialization
    void Awake () {
        rock = GetComponent<Rigidbody2D>();
        rock.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if (transform.position.y <= point1.position.y)
        {
            DisAcitve();
        }

	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, point1.position);
    }

    public void StartRock ()
    {
        rock.isKinematic = false;
    }

    void DisAcitve()
    {
        effect.transform.position = transform.position;
        effect.Play();

        AudioManager.Instances.PlayAudioEffect(effect.GetComponent<AudioSource>());

        gameObject.SetActive(false);
    }
}
