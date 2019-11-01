using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{

	public Abilit_Object ability;

	public virtual void FireAbility(){
		Debug.Log("Base ability Fired!");
	}

}
