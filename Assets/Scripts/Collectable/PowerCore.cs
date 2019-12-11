using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCore : PowerUp
{
	PowerUpType type = PowerUpType.Shield;

	private void OnEnable()
	{
		// StartCoroutine(StartCountDown());
	}

	IEnumerator StartCountDown(){
		yield return new WaitForSeconds(5f);
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player")) {
			PlayerProgress.Instance.PickUpPowerCore();
			base.PickedUp();
		}
	}
}
