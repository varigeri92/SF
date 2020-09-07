
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public Transform playerTransform;
	float speed;
	Player player;
	Camera cam;
	Vector3 prevpos;
	Vector3 dDirection;
	bool playerAlive = true;

	Vector3 velocity = Vector3.zero;

	public float startingTime = 0f;
	public float dampSpeed;
	public float dampingDistance;
	float time = 0f;

	bool allowDamp = false;

    [SerializeField]
    float distance;
    float _distance;
    [SerializeField]
    float distanceFactor;
    float _distanceFactor;

    // Use this for initialization
    void Start () {
        _distance = distance;
        _distanceFactor = 1;
		prevpos = transform.position;
        Player.OnPlayerLoaded += InitCam;
        Player.OnPlayerShooting += PlayerShooting;
        Player.OnPlayerStopShooting +=PlayerStopShooting;
	}

    private void OnDestroy()
    {
        Player.OnPlayerLoaded -= InitCam;
        Player.OnPlayerDeath -= OnPlayerDead;
        Player.OnPlayerShooting -= PlayerShooting;
        Player.OnPlayerStopShooting -= PlayerStopShooting;
    }

    void PlayerShooting() {
        _distanceFactor = distanceFactor;
    }

    void PlayerStopShooting()
    {
        _distanceFactor = 1;
    }

    void OnPlayerDead()
    {
        playerAlive = false;
    }

    public void InitCam(Player player){
        this.player = player;
        playerTransform = player.transform;
		speed = player.speed;
		cam = GetComponentInChildren<Camera>();
        Player.OnPlayerDeath += OnPlayerDead;

		// direction = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (player == null){
			return;
		}

		if (!playerAlive)
			return;

        SmothCameraMovement();
    }

	private void MooveCamera(Vector3 dir){
		prevpos = transform.position;
		transform.position = Vector3.Lerp(transform.position, transform.position + dir * speed, Time.deltaTime);
		dDirection = transform.position - prevpos;
		dDirection = dDirection * dampingDistance;

		transform.position = Vector3.SmoothDamp(transform.position, transform.position + dDirection, ref velocity, dampSpeed);

	}


    void CameraMovement()
    {
        if (!playerAlive)
            return;

		allowDamp = true;
        // stransform.position = targetPos;
		Vector3 direction = Vector3.zero;

        Vector3 playerPos = Camera.main.WorldToScreenPoint(playerTransform.position);

        if (playerPos.y >= (Screen.height - Screen.height * 0.2f))
        {
            // MooveCamera(Vector3.up);
            direction += Vector3.up;
			time = startingTime;
			allowDamp = false;
        }
        else if (playerPos.y <= (Screen.height * 0.2f))
        {
            // MooveCamera(Vector3.down);
            direction += Vector3.down;
			time = startingTime;
			allowDamp = false;

        }
        if (playerPos.x <= (Screen.width * 0.2f))
        {
            // MooveCamera(Vector3.left);
            direction += Vector3.left;
			time = startingTime;
			allowDamp = false;

        }
        else if (playerPos.x >= (Screen.width - Screen.width * 0.2f))
        {
            // MooveCamera(Vector3.right);
            direction += Vector3.right;
			time = startingTime;
			allowDamp = false;
        }

        MooveCamera(direction);

    }

	void SmothCameraMovement(){
		Vector3 calculatedPosition = Vector3.SmoothDamp(transform.position, playerTransform.position + playerTransform.up * distance * _distanceFactor, ref velocity, dampSpeed);
		transform.position = calculatedPosition;
	}
}
