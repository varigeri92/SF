﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

	public float lifetime;
	public int dmg;
	public float speed;
    public bool isBomb;
    public GameObject explosionFX;


	private void OnEnable()
	{
		StartCountdown();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" || collision.collider.tag == "Enemy")
        {
            OnColliding(collision.collider.gameObject);
        }
        else
        {
            if (!isBomb)
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
            if (go.GetComponent<BasicEnemy>() != null)
            {
			    go.GetComponent<BasicEnemy>().TakeDmg(dmg);
            }
		}
	}

	public void StartCountdown()
	{
        if (lifetime == 0)
        {
            return;
        }
        else
        {
            StartCoroutine(Countdown());
        }
	}

	public void OnColliding(GameObject go){
		DealDmg(dmg,go);
        if (isBomb)
        {
            StartCoroutine(Explode());
        }
        else
        {
    		Destroy(gameObject);
        }
	}

	public void OnCounterOver(){
        if (isBomb)
        {
            StartCoroutine(Explode());
        }
        else
        {
            Destroy(gameObject);
        }
    }

	IEnumerator Countdown(){

		yield return new WaitForSeconds(lifetime);
		OnCounterOver();


	}

    IEnumerator Explode()
    {
        speed = 0;
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        Instantiate(explosionFX,transform.position,Quaternion.identity);
        while (true)
        {
            collider.radius +=  100 * Time.deltaTime;
            yield return null;
            if (collider.radius > 25)
            {
                Destroy(gameObject);
            }
        }
    }
}
