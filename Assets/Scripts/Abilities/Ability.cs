﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{

	public Abilit_Object ability;

	public int charges;

	public TMPro.TMP_Text chargesText;

	private void Start()
	{
		charges = ability.charges;
		chargesText = GameObject.FindGameObjectWithTag("UltimatePanel").transform.Find("Charges_Value").GetComponent<TMPro.TMP_Text>();
		chargesText.text = charges.ToString();
	}

	public virtual void FireAbility(){
		Debug.Log("Base ability Fired!");
        if (chargesText == null)
        {
            Debug.Log("Charges Text is Null!! ");
            chargesText = GameObject.FindGameObjectWithTag("UltimatePanel").transform.Find("Charges_Value").GetComponent<TMPro.TMP_Text>();
            chargesText.text = charges.ToString();
        }
	}

}
