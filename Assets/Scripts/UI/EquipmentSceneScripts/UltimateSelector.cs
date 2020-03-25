using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UltimateSelector : MonoBehaviour
{

	public PlayerObject playerObject;



	public void SetUltimate(GameObject _abilityPrefab)
	{
		playerObject.ultimate = _abilityPrefab;
		playerObject.ultimateIcon = _abilityPrefab.GetComponent<Ability>().ability.icon;

	}

}
