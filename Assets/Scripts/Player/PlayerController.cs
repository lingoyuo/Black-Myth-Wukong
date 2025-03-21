using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	public enum Enviroment
	{
		Normal,
		Water
	}

	public bool EditorTest = true;

	public float MAX_ForcePushBack = 20;

	public float timePush = 1f;

	public Enviroment enviroment = Enviroment.Normal;

	public float MaxSpeedPlayer = 7.0f;

    public ParticleSystem effect_run_player;

	public GameObject moblie_controller;
	public GameManager gameManager;
	public AudioSource audio_water_hit;

	[Space (10)]
	[Header ("Ground Check(Platform and Spring)")]
	public bool grounded = true;
	public bool grounded_spring = false;
	public bool ground_plaform_move = false;
	public bool playerDead;

	public ControlPhysics DefaultPhysics;

	public ControlPhysics Parameters { get { return overritePhysics ?? DefaultPhysics; } }

	public int lepJump = 2;
	// number leap

	[Space (10)]
	[Header ("Force in Water")]
	public float forcePushWater = 200.0f;
	public float forcePushWhenFall = 100.0f;

	Rigidbody2D rid;
	Animator anim;
	ControlPhysics overritePhysics;
	PlayerCombatSystem attackState;

	HandlePlatform handle;

	int currentLep;

	float limitLeft;
	float limitRight;

	float horizontal;
	float currentHorizontal;
	float bottomWater;

	// Check finish animation
	bool finishAnimation;

	/*
    ** Process hit from enemy
    */

	float timer;
	float MaxPush;
	Vector2 posHit;
    
	//------------------------

	Vector3 currentPosPlayer;

	bool moveLetf;

	bool playerGetHit;
	// stop control input to push player to back
    
	//bool jump;
	bool dive;

	void Start ()
	{
		rid = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		currentLep = 0;

		grounded = false;
		playerDead = false;

		MaxPush = MAX_ForcePushBack;

		attackState = GetComponent<PlayerCombatSystem> ();
		handle = GetComponent<HandlePlatform> ();
	}

	void OnBecameInvisible ()
	{
		if (playerDead) { 
			gameManager.ShowUIEndGame ();
		}
	}

	void Update ()
	{

//#if UNITY_EDITOR
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.Space))
            Jump_in_editor();
        else
            Jump_release_in_editor();
//#endif

        if (Mathf.Abs (rid.linearVelocity.x) > 5 && grounded) {
			if (!effect_run_player.isPlaying)
				effect_run_player.Play ();
		} else if (!grounded) {
			effect_run_player.Stop ();
			effect_run_player.Clear ();
		}

		// Process animation walk and jump       
		if (horizontal != 0) {
			anim.SetBool ("walk", true);
		} else
			anim.SetBool ("walk", false);

		if (!grounded && !grounded_spring)
			anim.SetBool ("jump", true);
		else
			anim.SetBool ("jump", false);


		// Only get input when player no dead
		if (!playerDead)
			ControlUserInput ();

		if (finishAnimation)
			RotatePlayerDead ();

		LimitMoveInScreen ();

		if (playerDead)
			OnBecameInvisible ();
	}

	void LimitMoveInScreen ()
	{

		float realWilthScreen = Camera.main.orthographicSize * Screen.width / Screen.height;

		float boxSize = GetComponent<BoxCollider2D> ().size.x / 2;

		limitLeft = Camera.main.transform.position.x - realWilthScreen + boxSize + 0.3f;

		limitRight = Camera.main.transform.position.x + realWilthScreen - boxSize - 0.3f;

		//// Fix bug: Enemy push player to limit left screen
		//if(transform.position.x - limitLeft < 0.1f && transform.localScale.x < 0)
		//    transform.position = new Vector2(limitLeft, transform.position.y);

		transform.position = new Vector2 (Mathf.Clamp (transform.position.x, limitLeft, float.MaxValue), transform.position.y);
        

	}

	void ControlUserInput ()
	{

		if (!playerGetHit) {
			// If player attack, stop player
			if (!attackState.attacking && !ground_plaform_move)
				MoveHorizontal ();
			else
				rid.linearVelocity = new Vector2 (0, rid.linearVelocity.y);

			if (ground_plaform_move)
				handle.horizontal = horizontal;
		} else {
			PushPlayer ();
		}

		if (timer > timePush) {
			timer = 0;
			playerGetHit = false;
			MaxPush = MAX_ForcePushBack;
		}

		//MoveHorizontal();

		JumpInAir ();

		if (enviroment == Enviroment.Water)
			DiveControl ();
	}

	// Control dive in water
	void DiveControl ()
	{
		if (dive) {
			if ((transform.position.y - currentPosPlayer.y) < Parameters.maxSpeedInHeight / 2) {
				rid.linearVelocity = Vector2.Lerp (rid.linearVelocity, new Vector2 (rid.linearVelocity.x, 7), Time.deltaTime * Parameters.SpeedDiveUp);
			} else {
				rid.linearVelocity = Vector2.Lerp (rid.linearVelocity, new Vector2 (rid.linearVelocity.x, 7), Time.deltaTime * Parameters.SpeedDiveUp / 2);
			}

			if (transform.position.y - currentPosPlayer.y > Parameters.MaxHeightDive) {
				dive = false;
			}
		}

		/*
        ** --- Check animation dive
        */

		/*
        // Raycast detect
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, -transform.up, box.size.y / 2 + 0.1f, 1 << 9);

        if (rayHit.transform != null)
            anim.SetBool("dive", false);
        else
            anim.SetBool("dive", true);*/
	}

	void MoveHorizontal ()
	{
		if (horizontal < 0 && transform.position.x - limitLeft < 0.1f) {
			transform.position = new Vector2 (limitLeft, transform.position.y);
			rid.linearVelocity = new Vector2 (0, rid.linearVelocity.y);
		} else if (horizontal > 0 && transform.position.x - limitRight > -0.1f) {
			transform.position = new Vector2 (limitRight, transform.position.y);
			if (rid.linearVelocity.x > 0)
				rid.linearVelocity = new Vector2 (0, rid.linearVelocity.y);
		} else
			rid.linearVelocity = Vector2.Lerp (rid.linearVelocity, new Vector2 (horizontal * MaxSpeedPlayer, rid.linearVelocity.y), Time.deltaTime * Parameters.accelSpeed);

		if (horizontal == 0)
			rid.linearVelocity = Vector2.Lerp (rid.linearVelocity, new Vector2 (horizontal * MaxSpeedPlayer, rid.linearVelocity.y), Time.deltaTime * Parameters.bakeSpeed);

		if (Mathf.Abs (rid.linearVelocity.x) > Parameters.maxSpeed) {
			rid.linearVelocity = new Vector2 (Parameters.maxSpeed * horizontal, GetComponent<Rigidbody2D> ().linearVelocity.y);
		}

		Flip (horizontal);

	}

	void JumpInAir ()
	{     
		if (!grounded && Mathf.Abs (rid.linearVelocity.x) > Parameters.maxSpeedInHeight)
			rid.linearVelocity = new Vector2 (Parameters.maxSpeedInHeight * horizontal, rid.linearVelocity.y);


		if (rid.linearVelocity.y >= 0)
			rid.gravityScale = Parameters.gravityUp;
		else if (rid.linearVelocity.y < -0.1)
			rid.gravityScale = Parameters.gravity;
	}

	void Flip (float horizontal)
	{
		if (horizontal != 0)
			transform.localScale = new Vector2 (Mathf.Sign (horizontal) * Mathf.Abs (transform.localScale.x), transform.localScale.y);
	}

 
	public void Jump (UnityEngine.EventSystems.BaseEventData data)
	{

		if (enviroment != Enviroment.Water) {
			if (grounded) {
				rid.linearVelocity = new Vector2 (rid.linearVelocity.x, Parameters.jumpHeight);
				// currentLep++;
			} else if (currentLep != 0) {
				currentLep++;
				rid.linearVelocity = new Vector2 (rid.linearVelocity.x, Parameters.jumpHeight);
				if (currentLep >= lepJump)
					currentLep = 0;
			}

			// Animation dive
		} else {
			dive = true;
			currentPosPlayer = transform.position;

			// Animation not dive
		}   
	}

	public void PushPlayer ()
	{

		if (posHit.x > transform.position.x) {

			if (horizontal >= 0) {
				MaxPush = Mathf.Lerp (MaxPush, 0, Time.deltaTime * 10.0f);
				rid.linearVelocity = new Vector2 (-MaxPush, rid.linearVelocity.y);

				playerGetHit = true;
				timer += Time.deltaTime;
			} else
				playerGetHit = false;
		} else {
			if (horizontal <= 0) {
				MaxPush = Mathf.Lerp (MaxPush, 0, Time.deltaTime * 10.0f);
				rid.linearVelocity = new Vector2 (MaxPush, rid.linearVelocity.y);

				playerGetHit = true;
				timer += Time.deltaTime;
			} else
				playerGetHit = false;

		}
	}

	public void PlayerGetHitControlPhysics (Vector2 pos)
	{
		posHit = pos;
		playerGetHit = true;
		MaxPush = MAX_ForcePushBack;
		timer = 0;
	}

	public void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Water") {
			enviroment = Enviroment.Normal;
			overritePhysics = null;
			rid.gravityScale = 1.0f;
			rid.AddForce (new Vector2 (0, forcePushWater));
		}
	}

	public void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Water") {
			enviroment = Enviroment.Water;
			overritePhysics = other.gameObject.GetComponent<PhysicVolume> ().overritePhysics;
			rid.AddForce (new Vector2 (0, forcePushWhenFall * Mathf.Abs (rid.linearVelocity.y)));
		}
	}

	/*
    ** Editor test mode
    */

	//#if UNITY_EDITOR
	void Jump_in_editor ()
	{
		if (!playerDead) {
			if (enviroment != Enviroment.Water) {
				if (grounded) {
					rid.linearVelocity = new Vector2 (rid.linearVelocity.x, Parameters.jumpHeight);
					// currentLep++;
				} else if (currentLep != 0) {
					currentLep++;
					rid.linearVelocity = new Vector2 (rid.linearVelocity.x, Parameters.jumpHeight);
					if (currentLep >= lepJump)
						currentLep = 0;
				}

				// Animation dive
			} else {
				dive = true;
				currentPosPlayer = transform.position;

				// Animation not dive
			}
		}
	}

	void Jump_release_in_editor ()
	{
		dive = false;
		currentPosPlayer = transform.position;
	}
	//#endif

	/* 
** Moblie mode
*/

	#if UNITY_IOS || UNITY_ANDROID
	public void JumpRelease (UnityEngine.EventSystems.BaseEventData data)
	{
		dive = false;
		currentPosPlayer = transform.position;
	}

	public void MoveLeft (UnityEngine.EventSystems.BaseEventData data)
	{
		horizontal = -1;
		anim.SetBool ("walk", true);
	}

	public void MoveRight (UnityEngine.EventSystems.BaseEventData data)
	{
		horizontal = 1;
		anim.SetBool ("walk", true);
	}

	public void Release (UnityEngine.EventSystems.BaseEventData data)
	{
		horizontal = 0;
		anim.SetBool ("walk", false);
	}
	# endif

	// player dead behaviours function
	public void PlayerDead ()
	{
		playerDead = true;                                                                      // set player dead
		rid.isKinematic = true;                                                                 // set kinematic to disable force player
		anim.SetTrigger ("dead");                                                                // animation dead appeear
		GetComponent<FallInAirDamagePlayer> ().enabled = false;                                  // Disable particle effect
		moblie_controller.SetActive (false);                                                     // Deactive input
	}

	// animation script player dead
	public void RotatePlayerDead ()
	{
		rid.constraints = RigidbodyConstraints2D.None;
		anim.enabled = false;
		transform.parent.eulerAngles = new Vector3 (transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, transform.parent.eulerAngles.z + Time.deltaTime * 700);
		transform.parent.position = new Vector2 (transform.parent.position.x, transform.parent.position.y - Time.deltaTime * 4.0f);
       
	}

	public void FinishAnimation ()
	{
		GameObject parent = new GameObject ();
		Vector3 center = GetComponent<SpriteRenderer> ().sprite.bounds.center;
		parent.transform.position = new Vector2 (transform.position.x + center.x, transform.position.y + center.y);
		transform.SetParent (null);
		transform.SetParent (parent.transform);
		Camera.main.GetComponent<CameraControllerLimited> ().enabled = false;                    // Stop camera follow player
		finishAnimation = true;       
	}

	public void AudioWaterHitPlay ()
	{
		AudioManager.Instances.PlayAudioEffect (audio_water_hit);
	}
}
