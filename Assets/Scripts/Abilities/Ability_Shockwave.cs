using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Shockwave : Ability
{

	[SerializeField]
	CircleCollider2D _collider;

	[SerializeField]
	float radius;

	[SerializeField]
	float raiseRate;

    ShakeCamera shaker;

	public string Sucess;

    private void OnEnable()
    {
        shaker = Camera.main.GetComponent<ShakeCamera>();
    }

    public override void FireAbility()
	{

		Debug.Log(Sucess);
        if (charges > 0) {
            shaker.Shake();
            base.FireAbility();
			charges--;
			chargesText.text = charges.ToString();

			Instantiate(ability.effect, transform.position, Quaternion.identity);
			StartCoroutine(raiseCollider());
		}

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Enemy")){
			if(collision.gameObject.GetComponent<Enemy>() != null){
				collision.gameObject.GetComponent<Enemy>().TakeDmg(ability.damage);
			}else{
				Debug.Log(collision.gameObject.name + "Dont have an Enemy Component on it...");
			}
		}
	}

	IEnumerator raiseCollider(){
		while (_collider.radius < radius){
			_collider.radius += raiseRate * Time.deltaTime;
			yield return null;
		}
		_collider.radius = 0.0001f;
	}
}
