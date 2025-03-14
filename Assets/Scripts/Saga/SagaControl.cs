using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.InteropServices;

[System.Serializable]
public class TestUnlockLevel
{
	public bool test = false;
	public int levelUnlock = 1;
	public int currentLevel = 1;
}

public class SagaControl : MonoBehaviour
{
	public Camera cam;
	public GameObject player;
	public GameObject levelSaga;
	public float speedLerp = 0.5f;
	public float speedPosition = 0.3f;

	public float minPosition;
	public float maxPosition;
	public float deltaAxis = 1;
	public TestUnlockLevel test;
	Vector3 re = Vector3.zero;
	public LocalAccessValue localSaga;

	float cameraPosition;
	float beginX;
	float beginCam;
	float cameraSize;
	float beginPlayer;
	float camMax;
	float camMin;

	bool mouse = false;

	int prefPlayLevel;
	int levelUnlock;
	Animator aniPlayer;

	[ContextMenu ("SaveData")]
	public void SaveData ()
	{
		localSaga.SetTotalLeLevelUnlock (test.levelUnlock);
		localSaga.SetCurrentLevel (test.currentLevel);
	}

	void Awake ()
	{
		if(test.test)
		{
			SaveData ();
		}
		aniPlayer = player.transform.GetChild (0).gameObject.GetComponent<Animator> ();
        
		if (localSaga.GetCurrentLevel () == -1) {
			localSaga.SetCurrentLevel (1);
			localSaga.SetTotalLeLevelUnlock (1);
		}
	}

	// Use this for initialization
	void Start ()
	{
		prefPlayLevel = localSaga.GetCurrentLevel ();
		levelUnlock = localSaga.GetTotalLevelUnlock ();
		cameraSize = (float)Screen.width / (float)Screen.height * Camera.main.orthographicSize;         // 1/2 chieu rong camera
		camMax = maxPosition - cameraSize - deltaAxis;
		camMin = minPosition + cameraSize + deltaAxis;
		cam.transform.position = new Vector3 (Mathf.Clamp (levelSaga.transform.GetChild (prefPlayLevel - 1).gameObject.transform.position.x, camMin, camMax), cam.transform.position.y, cam.transform.position.z);
		CameraLerpPosition (levelSaga.transform.GetChild (prefPlayLevel - 1).gameObject.transform.position.x);
		player.transform.position = levelSaga.transform.GetChild (prefPlayLevel - 1).gameObject.transform.position;
		if (levelUnlock - prefPlayLevel == 1)
			levelSaga.transform.GetChild (prefPlayLevel - 1).gameObject.GetComponent<Animator> ().SetTrigger ("Win");
	}

	void Update ()
	{
		beginPlayer = player.transform.position.x;
		CameraMoveMouse ();
        
		if (cam.transform.position.x != cameraPosition) {

			if (((cam.transform.position.x > camMax) || (cam.transform.position.x < camMin)) && !Input.GetMouseButton (0)) {
				cam.transform.position = Vector3.SmoothDamp (cam.transform.position, new Vector3 (cameraPosition, cam.transform.position.y, cam.transform.position.z), ref re, speedLerp / 1.5f);
                
			} else {
				if (Input.GetMouseButton (0)) {
					cam.transform.position = Vector3.Lerp (cam.transform.position, new Vector3 (cameraPosition, cam.transform.position.y, cam.transform.position.z), speedLerp);
				} else {
					cam.transform.position = Vector3.Lerp (cam.transform.position, new Vector3 (cameraPosition, cam.transform.position.y, cam.transform.position.z), speedLerp / 5);
				}
			}
                
		}
        
	}

	void LateUpdate ()
	{
		SetMouseDown ();
		if (player.transform.position.x - beginPlayer > 0) {
			TurnPlayer (1);

		} else if (player.transform.position.x - beginPlayer < 0) {
			TurnPlayer (-1);

		}
	}

	void CameraLerp ()                                  // lerp camera theo touch
	{
		if (Input.touchCount > 0 && (Input.GetTouch (0).phase == TouchPhase.Moved)) {
			cameraPosition = Mathf.Clamp (cam.transform.position.x - Input.GetTouch (0).deltaPosition.x * speedPosition, camMin, camMax);
		}
	}

	public void CameraLerpPosition (float posiX)        // lerp camera den 1 vi tri
	{
		cameraPosition = Mathf.Clamp (posiX, camMin, camMax);
		//print(camMax);
	}

	void CamraMoveToch ()                                 // Move camera theo touch
	{
		if (Input.touchCount > 0 && (Input.GetTouch (0).phase == TouchPhase.Moved))
			cameraPosition = Mathf.Clamp (cameraPosition - Input.GetTouch (0).deltaPosition.x / ((float)Screen.width) * cameraSize * 2, camMin - deltaAxis, camMax + deltaAxis);
		else if (Input.GetTouch (0).phase == TouchPhase.Ended) {
			if (cameraPosition > camMax)
				cameraPosition = camMax;
			else if (cameraPosition < camMin)
				cameraPosition = camMin;
			else
				cameraPosition = Mathf.Clamp (cameraPosition - Input.GetTouch (0).deltaPosition.x * Time.deltaTime, camMin, camMax);
		}
	}

	public void CameraMoveMouse ()                      // Movecamera theo mouse
	{
		if (LevelSaga.click) {
			if (Input.GetMouseButtonDown (0)) {
				if (!mouse) {
					mouse = true;
				}
				beginX = Input.mousePosition.x;
				beginCam = cam.transform.position.x;
			} else if (Input.GetMouseButton (0)) {

				cameraPosition = Mathf.Clamp (beginCam + (beginX - Input.mousePosition.x) / ((float)Screen.width) * cameraSize * 2, camMin - deltaAxis, camMax + deltaAxis);
			} else if (Input.GetMouseButtonUp (0) && !Input.GetMouseButton (0) && mouse) {
				mouse = false;
				if (cameraPosition > camMax)
					cameraPosition = camMax;
				else if (cameraPosition < camMin)
					cameraPosition = camMin;
				else if (Input.touchCount > 0)
					cameraPosition = Mathf.Clamp (cameraPosition - Input.GetTouch (0).deltaPosition.x * speedPosition, camMin, camMax);
			}
		}
	}

	void SetMouseDown ()
	{
		beginX = Input.mousePosition.x;
		beginCam = cameraPosition;
	}

   
	public void MovePlayerNextLevel (string level)                  // Player di chuyen den level tiep theo
	{
		iTween.MoveTo (player, iTween.Hash ("path", iTweenPath.GetPath ("New Path " + level), "time", 1.5, "easetype", iTween.EaseType.linear));
		aniPlayer.SetBool ("walk", true);
		Invoke ("SetAnimationHi", 1.5f);
		Invoke ("SetAnimationIdle", 2f);
	}

	public void MoveLevel (GameObject target)                    // Player di chuyen den level ngay nhien
	{
		iTween.MoveTo (player, iTween.Hash ("position", target.transform.position, "time", 1.5, "easetype", iTween.EaseType.easeOutCubic));
		aniPlayer.SetBool ("fly", true);
		Invoke ("SetAnimationHi", 1.5f);
		Invoke ("SetAnimationIdle", 2f);
	}

	public void PlayLevelIdle ()                                   // Player click vao level hien tai
	{
		Invoke ("SetAnimationHi", 1f);
		Invoke ("SetAnimationIdle", 1.5f);
	}

	void SetAnimationHi ()
	{
		aniPlayer.SetTrigger ("hi");
	}

	void SetAnimationIdle ()
	{
		aniPlayer.SetBool ("walk", false);
		aniPlayer.SetBool ("fly", false);
	}

	void TurnPlayer (int i)                                                  // xoay Player
	{
		player.transform.eulerAngles = new Vector3 (0, 90 - i * 90, 0);
	}
}
