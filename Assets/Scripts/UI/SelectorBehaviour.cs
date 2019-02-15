using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorBehaviour : MonoBehaviour {


	public bool haschild = false;
	public GameObject gun;


	public void PickUpCollectable(bool isWeapon){
		if(isWeapon){
			haschild = true;
		}
	}
}
