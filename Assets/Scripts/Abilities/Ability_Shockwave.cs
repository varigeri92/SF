using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Shockwave : Ability
{

	[SerializeField]
	CircleCollider2D collider;

	[SerializeField]
	float radius;

	[SerializeField]
	float raiseRate;


	public string Sucess;

	public override void FireAbility()
	{

		Debug.Log(Sucess);

		if (charges > 0) {
			base.FireAbility();
			charges--;
			chargesText.text = charges.ToString();

			Instantiate(ability.effect, transform);
			StartCoroutine(raiseCollider());
		}

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Enemy")){
			collision.gameObject.GetComponent<Enemy>().TakeDmg(ability.damage);
		}
	}

	IEnumerator raiseCollider(){
		while (collider.radius < radius){
			collider.radius += raiseRate * Time.deltaTime;
			yield return null;
		}
		collider.radius = 0.0001f;
	}
}
