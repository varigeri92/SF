using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {


	public delegate void OnEnemyDead(BasicEnemy enemy);
	public static event OnEnemyDead onEnemyDead;

	public EnemyObject enemyObject;

	Gun gun;
	Transform target;
	Rigidbody2D rb;
	bool playerAlive = true;



	void OnEnable()
	{
		if(GetComponentInChildren<Gun>() != null)
		{
			gun = GetComponentInChildren<Gun>();
		}
		else
		{
			gun = null;
		}

		target = GameObject.FindGameObjectWithTag("Player").transform;
		rb = GetComponent<Rigidbody2D>();
		Player.OnPlayerDeath += PlayerDead;
	}

	void FixedUpdate()
	{
		if(playerAlive){
			
			Vector2 direction = (Vector2)target.position - rb.position;
			direction.Normalize();
			float rotateamount = Vector3.Cross(direction, transform.up).z;
			rb.angularVelocity = -rotateamount * enemyObject.rotationSpeed;
			float _speed = enemyObject.mooveSpeed;
			float distance = Vector2.Distance((Vector2)transform.position, (Vector2)target.position);

			switch (enemyObject.type) {
				case EnemyType.Shooter:

					if (distance < 10)
					{
						Shoot();
					}

					if (distance < 5) {
						_speed = 0;
						rb.velocity = transform.right * enemyObject.mooveSpeed;
					} else {
						_speed = enemyObject.mooveSpeed;
						rb.velocity = transform.up * _speed;
					}

					if (distance < 3) {
						_speed = -enemyObject.mooveSpeed;
						rb.velocity = transform.up * _speed;
					}

					break;
				case EnemyType.Kamikaze:

					if (distance < 10) 
					{
						_speed = 4;
						rb.velocity = transform.up * _speed;
					}

					_speed = enemyObject.mooveSpeed;
					rb.velocity = transform.up * _speed;

					break;
				case EnemyType.Boss:
					
					break;
				default:
					Debug.LogWarning(enemyObject.type.ToString() + " enemyType is not defined in the 'BasicEnemy.cs'!");
					break;
			}
		}

	}


	void PlayerDead()
	{
		playerAlive = false;
	}

	private void OnDestroy()
	{
		Player.OnPlayerDeath -= PlayerDead;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Player") {
			collision.gameObject.GetComponent<Player>().TakeDmg(2);
			Die();
		}
	}

	private void Die()
	{
		if (onEnemyDead != null) {
			onEnemyDead(this);
		}
		Instantiate(enemyObject.exposionFX, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	public void TakeDmg(int dmg)
	{
		enemyObject.helath -= dmg;
		if (enemyObject.helath <= 0) {
			Die();
		}
	}

	void Shoot(){
		
		gun.Shooting(false);

	}

}
