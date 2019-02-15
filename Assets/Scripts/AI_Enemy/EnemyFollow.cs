using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {

	public delegate void OnEnemyDead(EnemyFollow enemy);
	public static event OnEnemyDead onEnemyDead;

	public int healt = 3;

	public float speed;
	public Rigidbody2D rb;
	public Transform target;
	public float rotateSpeed = 1000;
	public GameObject FX;
	public List<Transform> attachPoints = new List<Transform>();
	public List<Gun> activeGuns = new List<Gun>();

	public bool kamikaze;


	bool playeralive = true;
	// Use this for initialization
	void OnEnable () {
		Debug.LogWarning("The Script EnemyFollow on the GameObject: " + 
		                 this.gameObject.name + 
		                 " is outdated! Use 'BasicEnemy' instead!");
		

		target = GameObject.FindGameObjectWithTag("Player").transform;
		rb = GetComponent<Rigidbody2D>();
		Player.OnPlayerDeath += PlayerMeghalodott;

	}

	private void OnDestroy()
	{
		Player.OnPlayerDeath -= PlayerMeghalodott;
	}

	void PlayerMeghalodott(){
		playeralive = false;
	}

	// Update is called once per frame
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.tag == "Player"){
			collision.gameObject.GetComponent<Player>().TakeDmg(2);
			Die();
		}
	}

	private void FixedUpdate()
	{
		if(!playeralive){
			return;
		}

		Vector2 direction = (Vector2)target.position - rb.position;
		direction.Normalize();
		float rotateamount = Vector3.Cross(direction, transform.up).z;
		rb.angularVelocity = -rotateamount * rotateSpeed;
		float _speed = speed;
		float distance = Vector2.Distance((Vector2)transform.position, (Vector2)target.position);

		if (distance < 10){
			if(!kamikaze){
				Shoot();
			}else{
				_speed = 4;
				rb.velocity = transform.up * _speed;
			}

		}
		if(!kamikaze){
			
			if (distance < 5) {
				_speed = 0;
				rb.velocity = transform.right * speed;
			} else {
				_speed = speed;
				rb.velocity = transform.up * _speed;
			}

			if (distance < 3) {
				_speed = -speed;
				rb.velocity = transform.up * _speed;
			}
		} else {
			_speed = speed;
			rb.velocity = transform.up * _speed;
		}
	}

	void Shoot(){
		foreach (Gun gun in activeGuns) {

			gun.Shooting(false);

		}
	}

	public void TakeDmg(int dmg){
		healt -= dmg;
		if(healt <= 0){
			Die();
		}
	}
	private void Die(){
		if(onEnemyDead != null){
			onEnemyDead(this);
		}
		Instantiate(FX,transform.position,Quaternion.identity);
		Destroy(gameObject);
	}
}
