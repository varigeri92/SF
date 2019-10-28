using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmObject : MonoBehaviour
{
    public EnemyObject enemyObject;
    public SwarmController myController;
	public int health;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            OnColliding(collision.collider.gameObject);
        }
        else
        {
            Die();
        }

    }
	void OnEnable()
	{
		health = enemyObject.health;
	}
    public void OnColliding(GameObject go)
    {
        DealDmg(enemyObject.health, go);
        Die();
    }
    public void DealDmg(int dmg, GameObject go)
    {
        if (go.tag == "Player")
        {
            go.GetComponent<Player>().TakeDmg(dmg);
        }
    }
    public  void TakeDmg(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        if(myController != null)
        myController.SwarmObjectDied();

        Instantiate(enemyObject.exposionFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
