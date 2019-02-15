using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

	public float lifetime;
	public int dmg;
	public float speed;


	private void OnEnable()
	{
		StartCountdown();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.tag == "Player" || collision.collider.tag == "Enemy"){
			OnColliding(collision.collider.gameObject);
		}else{
			Destroy(gameObject);
		}

	}
	public void FixedUpdate()
	{
		transform.Translate(Vector3.up*speed*Time.fixedDeltaTime);
	}

	public void DealDmg(int dmg, GameObject go){
		if(go.tag == "Player"){
			go.GetComponent<Player>().TakeDmg(dmg);

		}else if(go.tag == "Enemy"){
			go.GetComponent<BasicEnemy>().TakeDmg(dmg);

		}
	}

	public void StartCountdown()
	{
		StartCoroutine(Countdown());
	}

	public void OnColliding(GameObject go){
		DealDmg(dmg,go);
		Destroy(gameObject);
	}

	public void OnCounterOver(){
		Destroy(gameObject);
	}

	IEnumerator Countdown(){
		yield return new WaitForSeconds(lifetime);
		OnCounterOver();
	}
}
