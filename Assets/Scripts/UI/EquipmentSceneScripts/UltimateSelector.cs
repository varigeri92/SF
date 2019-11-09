using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UltimateSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

	GameObject icon;
	GameObject abilityPrefab;


	GameObject selectedAbility;

	[SerializeField]
	bool isPointerOver;

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
		if(transform.childCount > 0){
			Destroy(transform.GetChild(0).gameObject);
		}

		if (icon != null){
			Instantiate(icon, transform);
			selectedAbility = abilityPrefab;
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
