using UnityEngine;
using System.Collections;

public class ResizeBackground : MonoBehaviour {

    public Sprite BackgroundSprite;
    public GameObject BackGrounds;

    private BoxCollider2D boxCamera;

    void Start()
    {
        boxCamera = Camera.main.gameObject.GetComponent<BoxCollider2D>();
    }

	void Update()
    {
        float realWilthScreen = Camera.main.orthographicSize * Screen.width / Screen.height;
        float wilthSprite = BackgroundSprite.bounds.size.x / 2;

        BackGrounds.transform.localScale = new Vector3(realWilthScreen / wilthSprite, 1, 1);

        boxCamera.size = new Vector2(realWilthScreen*2, Camera.main.orthographicSize*2);

    }
}
