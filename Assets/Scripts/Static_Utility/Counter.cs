using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour{


	public IEnumerator Countdow(float lifetime, GameObject go){

		yield return new WaitForSeconds(lifetime);
		Destroy(go);
	}
}
