using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UltimateSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

	GameObject icon;
	GameObject abilityPrefab;
	public PlayerObject playerObject;

	GameObject selectedAbility;

	[SerializeField]
	bool isPointerOver;


	private void Start()
	{
		if (playerObject.ultimateIcon != null) {
			Instantiate(playerObject.ultimateIcon, transform);
			selectedAbility = playerObject.ultimate;
		}
	}

	private void Update()
	{
		if(Input.GetMouseButtonUp(0)){
			if(isPointerOver){
				SetUltimate();
			}
		}
	}

	public void SetSelectionOnDragBegin(GameObject _icon, GameObject _abilityPrefab, bool isGun)
	{
		if (isGun)
		{
			icon = null;
			abilityPrefab = null;
		}
		else
		{
			icon = _icon;
			abilityPrefab = _abilityPrefab;
		}
	}


	public void SetUltimate()
	{


		if (icon != null){
			if (transform.childCount > 0) {
				Destroy(transform.GetChild(0).gameObject);
			}
			Instantiate(icon, transform);
			selectedAbility = abilityPrefab;
			playerObject.ultimate = selectedAbility;
			playerObject.ultimateIcon = selectedAbility.GetComponent<Ability>().ability.icon;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isPointerOver = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isPointerOver = false;
	}
}
