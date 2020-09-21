using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy {


	public delegate void OnEnemyDead(BasicEnemy enemy);
	public static event OnEnemyDead onEnemyDead;

	public EnemyObject enemyObject;

	public Gun gun;


    void OnEnable()
	{
        health = enemyObject.health;
		if(GetComponentInChildren<Gun>() != null)
		{
			gun = GetComponentInChildren<Gun>();
		}
		else
		{
			gun = null;
		}
        int rndm = Random.Range(0, 2);
        if (rndm == 0)
        {
            left = false;
        }

		target = GameObject.FindGameObjectWithTag("Player").transform;
		rb = GetComponent<Rigidbody2D>();
		Player.OnPlayerDeath += PlayerDead;
	}

	void FixedUpdate()
	{
        Follow();
	}

    public virtual void Follow()
    {
        if (playerAlive)
        {
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();
            float rotateamount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateamount * enemyObject.rotationSpeed;
            float _speed = enemyObject.mooveSpeed;
            distance = Vector2.Distance((Vector2)transform.position, (Vector2)target.position);

            switch (enemyObject.type)
            {
                case EnemyType.Shooter:

					if (distance < enemyObject.shootingDistance)
                    {
                        Shoot();
                    }

                    if (distance < 15)
                    {
                        _speed = 0;
                        if (left)
                            rb.velocity = -transform.right * enemyObject.mooveSpeed;
                        else
                            rb.velocity = transform.right * enemyObject.mooveSpeed;
                    }
                    else
                    {
                        _speed = enemyObject.mooveSpeed;
                        rb.velocity = transform.up * _speed;
                    }

                    if (distance < 10)
                    {
                        _speed = -enemyObject.mooveSpeed;
                        rb.velocity = transform.up * _speed;
                    }

                    break;
                case EnemyType.Kamikaze:

                    if (distance < 10)
                    {
                        _speed = 1.5f * enemyObject.mooveSpeed;
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

	public override void Die()
	{
        if (onEnemyDead != null)
        {
            onEnemyDead(this);
        }
		GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().EnemyDestroyed();
		SpawnPowerCore();
		Instantiate(enemyObject.exposionFX, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
	public virtual void Die(bool destroyOnly)
	{
		GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().EnemyDestroyed();
		if (onEnemyDead != null) {
			onEnemyDead(this);
		}
		Destroy(gameObject);
	}

	public override void TakeDmg(int dmg)
	{
		health -= dmg;
		if (health <= 0) {
			Die();
		}
	}

	public virtual void Shoot(){
 
		gun.Shooting(false);

	}

    public virtual void Shoot(Gun _gun)
    {
        _gun.Shooting(false);

    }

	void SpawnPowerCore(){
		int rand = Random.Range(1,101);
		if(rand <= enemyObject.chancetoSpawn){
			Instantiate(enemyObject.powerCore, transform.position, Quaternion.identity);
			}
	}
}
