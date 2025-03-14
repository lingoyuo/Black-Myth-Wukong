using UnityEngine;
using System.Collections;

public class DetectPlayerSpideEnemy : MonoBehaviour {

    public Vector2 postionDrop = new Vector2(7.29f, 5.05f);

    private SmallSpide spide;

    void Awake()
    {
        spide = GetComponent<SmallSpide>();       
    }

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            spide.DropLeaf();
            spide.CallDrop(postionDrop.x, postionDrop.y);
            Destroy(this);
        }
    }
}
