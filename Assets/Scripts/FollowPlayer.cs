
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public Transform playerTransform;
	float speed;
	Player player;
	new Camera camera;
	Vector3 prevpos;
	Vector3 dDirection;
	bool playerAlive = true;

	Vector3 velocity = Vector3.zero;

	public float startingTime = 0f;
	public float dampSpeed;
	public float dampingDistance;
	float time = 0f;

	bool allowDamp = false;

	// Use this for initialization
	void Start () {
		prevpos = transform.position;
	}

	public void InitCam(){
		
		player = playerTransform.GetComponent<Player>();
		speed = player.speed;
		camera = GetComponent<Camera>();
		Player.OnPlayerDeath += delegate {

			playerAlive = false;
		};

		// direction = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (player == null){
			return;
		}

		if (!playerAlive)
			return;


		if (player.jetMode){
			speed = playerTransform.GetComponent<Player>().speed * 2;
		}
		if(!player.jetMode){
			speed = playerTransform.GetComponent<Player>().speed;
		}

		// CameraMovement();

		SmothCameraMovement();
    }



	private void ShakeCamera()
	{
		// StartCoroutine(ShakeCam());
	}

	IEnumerator ShakeCam(){
		for (int i = 0; i < 5; i++){
			float rnd = Random.Range(9.5f, 9.8f);
			float time = 0.01f;
			float progr = 0;
			while(progr < 1) {
				progr += time;
				camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, rnd, progr);
				yield return null;
			}
			yield return null;
		}
		yield break;
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
		Vector3 calculatedPosition = Vector3.SmoothDamp(transform.position, playerTransform.position, ref velocity, dampSpeed);
		calculatedPosition.z = -20f;
		transform.position = calculatedPosition;
	}
}
