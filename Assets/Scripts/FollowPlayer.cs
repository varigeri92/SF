
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public Transform playerTransform;
	float speed;
	Player player;
	new Camera camera;
	Vector3 prevpos;

	bool playerAlive = true;

	// Use this for initialization
	void Start () {
		player = playerTransform.GetComponent<Player>();
		speed = player.speed;
		camera = GetComponent<Camera>();
		Player.OnPlayerDeath += delegate {

			playerAlive = false;
		};
	}
	
	// Update is called once per frame
	void Update () {
		if (!playerAlive)
			return;


		if (player.jetMode){
			speed = playerTransform.GetComponent<Player>().speed * 2;
		}
		if(!player.jetMode){
			speed = playerTransform.GetComponent<Player>().speed;
		}

		if (Input.GetKey(KeyCode.LeftAlt)) {
			ShakeCamera();
		}
	}

	private void ShakeCamera()
	{
		StartCoroutine(ShakeCam());
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
	private void LateUpdate()
	{
		if (!playerAlive)
			return;


		Vector3 targetPos = Vector2.zero;


		// stransform.position = targetPos;


		Vector3 playerPos = Camera.main.WorldToScreenPoint(playerTransform.position);

		if (playerPos.y >= (Screen.height - Screen.height * 0.2f))
		{
			// MooveCamera(Vector3.up);
			targetPos += Vector3.up;
		}
		else if (playerPos.y <= (Screen.height * 0.2f))
		{
			// MooveCamera(Vector3.down);
			targetPos += Vector3.down;

		}
		if(playerPos.x <= (Screen.width * 0.2f))
		{
			// MooveCamera(Vector3.left);
			targetPos += Vector3.left;

		}
		else if (playerPos.x >= (Screen.width - Screen.width * 0.2f))
		{
			// MooveCamera(Vector3.right);
			targetPos += Vector3.right;
		}

		MooveCamera(targetPos);
	}

	private void MooveCamera(Vector3 dir){
		transform.position = Vector3.Lerp(transform.position, transform.position + dir * speed, Time.deltaTime);
	}
}
