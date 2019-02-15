using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {




	private void OnEnable()
	{
		StartCoroutine(Counter());
	}
	public virtual void PickedUp(){
		Destroy(this.gameObject);
	}



	IEnumerator Counter (){
		yield return new WaitForSeconds(10);
		Destroy(this.gameObject);
	}
}
