using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

	public float lifetime;
	public int dmg;
	public float speed;
    public bool isBomb;
    public GameObject explosionFX;
	bool exploding = false;

	public float spread = 1.5f;
	//public bool isSpreading = false;

	private void OnEnable()
	{
		StartCountdown();
		Vector3 rot = transform.rotation.eulerAngles + new Vector3 (0,0,Random.Range(-spread, spread));
		transform.rotation = Quaternion.Euler(rot);
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
			if(!exploding){
				Debug.Log("Exposion cased by collision!");
				StartCoroutine(Explode());
			}
        }
        else
        {
    		Destroy(gameObject);
        }
	}

	public void OnCounterOver(){
        if (isBomb)
        {
			if (!exploding) {
				Debug.Log("Exposion cased by Timer");
				StartCoroutine(Explode());
			}
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
		exploding = true;
        speed = 0;
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        Instantiate(explosionFX,transform.position,Quaternion.identity);
        while (true)
        {
            collider.radius +=  1000 * Time.deltaTime;
            yield return null;
            if (collider.radius > 4)
            {
                Destroy(gameObject);
            }
        }
    }
}
