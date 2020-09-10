using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UltimateSelector : MonoBehaviour
{

	public PlayerObject playerObject;



	public void SetUltimate(Abilit_Object abilityObject)
	{
        Global.Instance.PlayerData.ultimate =  abilityObject.gameplayPrefab;
        Global.Instance.PlayerData.ultimateIcon = abilityObject.icon;

	}

}
