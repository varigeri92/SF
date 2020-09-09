using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGroup : MonoBehaviour {

	Transform target;
	Rigidbody2D rb;
	bool playeralive =true;
	public EnemyObject enemyObject;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		target = GameObject.FindGameObjectWithTag("Player").transform;
		Player.OnPlayerDeath += onPlayerdead;
	}
	
	// Update is called once per frame
	void Update () {
		if(!playeralive)
			return;

		Vector2 direction = (Vector2)target.position - rb.position;
		direction.Normalize();
		float rotateamount = Vector3.Cross(direction, transform.up).z;
		rb.angularVelocity = -rotateamount * enemyObject.rotationSpeed;
		float _speed = enemyObject.mooveSpeed;
		float distance = Vector2.Distance((Vector2)transform.position, (Vector2)target.position);
		transform.Translate(transform.up * _speed * Time.deltaTime);
	}
	void OnDestroy(){
		Player.OnPlayerDeath -= onPlayerdead;
	}
	void onPlayerdead(){
		playeralive = false;
	}
}
