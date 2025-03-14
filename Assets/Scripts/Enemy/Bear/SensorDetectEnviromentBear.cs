using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SensorDetectEnviromentBear : MonoBehaviour {

    public bool obstacle_detect;
    public float pos_obstacle_left_player = float.MinValue;
    public float pos_obstacle_right_player = float.MaxValue;
    public float pos_obstacle;

    // caching pos detect and reset in time
    public float time_reset = 3.0f;

    // detect obstacle
    public bool obstacle_behind;
    public bool obstacle_front;
    public bool stuck;

    BoxCollider2D box;

    RaycastHit2D[] rayDetectObstaclesFront;                                     // detect front
    RaycastHit2D[] rayDetectObstaclesBehind;                                    // deteck behind
    RaycastHit2D[] rayDetectGround;                                             // detect ground
    BearDetectPlayer detectPlayer;                                              // deactive when fall in air and get position when detect player
    Animator anim;                                                              // jump animation

    Vector2 centerBox;

    public float offset = 0.05f;
    float height_box;
    float wilth_box;

    Vector2 firts_ray_right;
    Vector2 firts_ray_left;
    Vector2 firts_ray_bottom;

    bool isRid;

    float timer;

    void Awake()
    {
        IntializeValue();
        detectPlayer = transform.GetChild(0).GetComponent<BearDetectPlayer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        RayDetect();
        CheckObstacleBehind();
        CheckObstacleFront();

        if (obstacle_behind && obstacle_front)
            stuck = true;

        if (timer > time_reset)
        {
            pos_obstacle_left_player = float.MinValue;
            pos_obstacle_right_player = float.MaxValue;
            timer = 0;
        }

        if (pos_obstacle_left_player != float.MinValue || pos_obstacle_right_player != float.MaxValue) 
            timer += Time.deltaTime;
    }

    // Raydetect
    void RayDetect()
    {
        // update value
        firts_ray_right = transform.TransformPoint(new Vector2(centerBox.x + wilth_box / 2 + offset, centerBox.y - height_box / 2 + offset));
        firts_ray_bottom = transform.TransformPoint(new Vector2(centerBox.x - wilth_box / 2 + offset, centerBox.y - height_box / 2 - offset));
        firts_ray_left = transform.TransformPoint(new Vector2(centerBox.x - wilth_box / 2 - offset, centerBox.y - height_box / 2 + offset));

        int bit_mask = 1 << 13 | 1 << 15;

        // raycast in layer 13 platform
        for (int i = 0; i < 5; i++)
        {
            if (transform.localScale.x == 1)
            {
                rayDetectGround[i] = Physics2D.Raycast(new Vector2(firts_ray_bottom.x + i * (wilth_box - offset * 2) / 4, firts_ray_bottom.y), transform.TransformDirection(Vector2.up), offset, bit_mask);
                rayDetectObstaclesFront[i] = Physics2D.Raycast(new Vector2(firts_ray_right.x, firts_ray_right.y + i * (height_box - offset * 2) / 4), transform.TransformDirection(Vector2.left), offset, bit_mask);
                rayDetectObstaclesBehind[i] = Physics2D.Raycast(new Vector2(firts_ray_left.x, firts_ray_left.y + i * (height_box - offset * 2) / 4), transform.TransformDirection(Vector2.right), offset, bit_mask);
            }
            else
            {
                rayDetectGround[i] = Physics2D.Raycast(new Vector2(firts_ray_bottom.x - i * (wilth_box - offset * 2) / 4, firts_ray_bottom.y), transform.TransformDirection(Vector2.up), offset, bit_mask);
                rayDetectObstaclesFront[i] = Physics2D.Raycast(new Vector2(firts_ray_right.x, firts_ray_right.y + i * (height_box - offset * 2) / 4), transform.TransformDirection(Vector2.right), offset, bit_mask);
                rayDetectObstaclesBehind[i] = Physics2D.Raycast(new Vector2(firts_ray_left.x, firts_ray_left.y + i * (height_box - offset * 2) / 4), transform.TransformDirection(Vector2.left), offset, bit_mask);
            }
        }


        // animation jump and add rigidbody to fall in air
        if (!rayDetectGround[0].collider)
        {
            if (!GetComponent<Rigidbody2D>())
            {
                var rid = gameObject.AddComponent<Rigidbody2D>();
                rid.constraints = RigidbodyConstraints2D.FreezeRotation;
                isRid = true;
            }
            detectPlayer.gameObject.SetActive(false);
            anim.SetBool("jump", true);           
        }
        else
        {
            if (isRid)
            {
                Destroy(GetComponent<Rigidbody2D>());
                isRid = false;
            }
            detectPlayer.gameObject.SetActive(true);
            anim.SetBool("jump", false);
        }
    }


    /*
    ** Debug draw gismos
    */

    /*
    void OnDrawGizmos()
    {
        IntializeValue();

        for (int i = 0; i < 5; i++)
        {

            if (transform.localScale.x == 1)
            {
                Debug.DrawRay(new Vector2(firts_ray_bottom.x + i * (wilth_box - offset * 2) / 4, firts_ray_bottom.y), transform.TransformDirection(Vector2.up) * offset, Color.red);
                Debug.DrawRay(new Vector2(firts_ray_right.x, firts_ray_right.y + i * (height_box - offset * 2) / 4), transform.TransformDirection(Vector2.left) * offset, Color.red);
                Debug.DrawRay(new Vector2(firts_ray_left.x, firts_ray_left.y + i * (height_box - offset * 2) / 4), transform.TransformDirection(Vector2.right) * offset, Color.red);
            }
            else
            {
                Debug.DrawRay(new Vector2(firts_ray_bottom.x - i * (wilth_box - offset * 2) / 4, firts_ray_bottom.y), transform.TransformDirection(Vector2.up) * offset, Color.red);
                Debug.DrawRay(new Vector2(firts_ray_right.x, firts_ray_right.y + i * (height_box - offset * 2) / 4), transform.TransformDirection(Vector2.right) * offset, Color.red);
                Debug.DrawRay(new Vector2(firts_ray_left.x, firts_ray_left.y + i * (height_box - offset * 2) / 4), transform.TransformDirection(Vector2.left) * offset, Color.red);
            }
        }
    } */


    // Intial value
    void IntializeValue()
    {
        rayDetectObstaclesFront = new RaycastHit2D[5];
        rayDetectGround = new RaycastHit2D[5];
        rayDetectObstaclesBehind = new RaycastHit2D[5];

        box = GetComponent<BoxCollider2D>();
        centerBox = box.offset;

        height_box = box.size.y;
        wilth_box = box.size.x;

        firts_ray_right = transform.TransformPoint( new Vector2(centerBox.x + wilth_box / 2 + offset, centerBox.y - height_box / 2 + offset));
        firts_ray_left = transform.TransformPoint(new Vector2(centerBox.x - wilth_box / 2 - offset, centerBox.y - height_box / 2 + offset));
        firts_ray_bottom = transform.TransformPoint( new Vector2(centerBox.x - wilth_box / 2 + offset, centerBox.y - height_box / 2 - offset));
    }

    // Detect have any obstacle
    public bool CheckDetectObstacles()
    {
        for (int i = 0; i < 5; i++)
        {
            if (rayDetectObstaclesFront[i])
            {
                obstacle_detect = true;

                pos_obstacle = rayDetectObstaclesFront[i].point.x;

                if (detectPlayer.Player)
                {
                    if (rayDetectObstaclesFront[i].point.x > transform.position.x)
                    {
                        pos_obstacle_right_player = rayDetectObstaclesFront[i].point.x;
                    }
                    else
                    {
                        pos_obstacle_left_player = rayDetectObstaclesFront[i].point.x;
                    }
                }
                return true;
            }
        }
        obstacle_detect = false;

        return false;
    }


    // Check obstacle behind
    void CheckObstacleFront()
    {
        for(int i = 0; i < 5; i++)
        {
            if(rayDetectObstaclesFront[i].collider)
            {
                obstacle_front = true;
                return;
            }
        }
        obstacle_front = false;
    }
    // Check obstacle front
    void CheckObstacleBehind()
    {
        for (int i = 0; i < 5; i++)
        {
            if (rayDetectObstaclesBehind[i].collider)
            {
                obstacle_behind = true;
                return;
            }
        }
        obstacle_behind = false;
    }
}
