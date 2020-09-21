using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Enemy {

	public float lifetime;
	public int dmg;

	public ObstacleObject obstacleObject;
	public GameObject FX;

	private void OnEnable()
	{
		dmg = obstacleObject.dmg;
		health = obstacleObject.health;

		StartCountdown();
	}

	private void FixedUpdate()
	{
		transform.position += transform.up * obstacleObject.moveSpeed * Time.deltaTime;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Projectile") 
		{
			TakeDmg(health);
		}
		else if (collision.collider.tag == "Player")
		{
			DealDmg(collision.collider.gameObject);

		}
	}


	public void DealDmg(GameObject go)
	{
		go.GetComponent<Player>().TakeDmg(health);
		TakeDmg(health);
	}

	public override void TakeDmg(int dmg){
		health = health - dmg;
		if(health <= 0){
			Die();
		}
	}

	public void StartCountdown()
	{
		StartCoroutine(Countdown());
	}

	public override void Die(){
		Instantiate(FX, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	public void OnCounterOver()
	{
		Die();
	}

	IEnumerator Countdown()
	{
		yield return new WaitForSeconds(lifetime);
		OnCounterOver();
	}
}
