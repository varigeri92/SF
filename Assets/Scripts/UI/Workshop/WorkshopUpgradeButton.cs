using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorkshopUpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

	public UpgradeButtonObject buttonObject;

	private HowerText hoverText;
	private CanvasGroup hoverTextCanvasGroup;

	[SerializeField]
	Color availableColor;

	[SerializeField]
	Color lockedColor;

	[SerializeField]
	Color notEnoughCoresColor;

	[SerializeField]
	GameObject upgradedHighlight;
	[SerializeField]
	TMPro.TMP_Text cost;
	[SerializeField]
	TMPro.TMP_Text level;
	[SerializeField]
	UnityEngine.UI.Image icon;

	private bool available;

	WorkshopUI workshopUI;

	private void Start()
	{
		workshopUI = GameObject.FindGameObjectWithTag("WorkshopUI").GetComponent<WorkshopUI>();


		hoverText = GameObject.FindGameObjectWithTag("HoverText").GetComponent<HowerText>();
		hoverTextCanvasGroup = hoverText.GetComponent<CanvasGroup>();
		WorkshopUI.OnUpgrade += InitButton;
		InitButton();
	}

	private void OnDestroy()
	{
		WorkshopUI.OnUpgrade -= InitButton;
	}

	void InitButton(){

		UnityEngine.UI.Image baseButton = GetComponent<UnityEngine.UI.Image>();
		baseButton.color = lockedColor;

		if (buttonObject.upgraded) {
			upgradedHighlight.SetActive(true);
		}
		if(buttonObject.requireLevel <= buttonObject.playerObject.level){
			if (buttonObject.required != null) {
				if (buttonObject.required.upgraded) {
					if ((buttonObject.playerObject.powerCores - buttonObject.cost) > -1) {
						available = true;
						baseButton.color = availableColor;
					} else {
						available = false;
						baseButton.color = notEnoughCoresColor;
					}
				}
			} else {
				if ((buttonObject.playerObject.powerCores - buttonObject.cost) > -1) {
					available = true;
					baseButton.color = availableColor;
				} else {
					available = false;
					baseButton.color = notEnoughCoresColor;
				}
			}
		}

		if(buttonObject.upgraded){
			baseButton.color = availableColor;
		}

		string lvlString = "";
		for (int i = 0; i < buttonObject.lvl; i++) {
			lvlString += "+";
		}

		level.text = lvlString;
		cost.text = buttonObject.cost.ToString();

		icon.sprite = buttonObject.icon;
	}





	public void Upgrade(){
		if(available && !buttonObject.upgraded){
			switch(buttonObject.upgradeType){
				case UpgradeType.PlayerUpgrade :
					if(buttonObject.upgradeProperty == UpgradeProperty.Health){
						buttonObject.playerObject.maxHealth += (int)buttonObject.upgradeValue;
						upgradedHighlight.SetActive(true);
						buttonObject.upgraded = true;
						buttonObject.playerObject.powerCores -= buttonObject.cost;
						buttonObject.playerObject.level++;
						workshopUI.Upgraded();
						Debug.Log(buttonObject.playerObject.maxHealth);
					}
					break;
				case UpgradeType.GunUpgrade:
					
					break;
				case UpgradeType.Ultimate:
					
					break;
					
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		hoverText.SetTittle(buttonObject.tittleText);
		hoverText.SetDescription(buttonObject.descriptionText);
		hoverTextCanvasGroup.alpha = 1;
		if(available && !buttonObject.upgraded){
			upgradedHighlight.SetActive(true);
		}
	}
	
	public void OnPointerExit(PointerEventData eventData)
	{
		if (available && !buttonObject.upgraded) {
			upgradedHighlight.SetActive(false);
		}
		hoverTextCanvasGroup.alpha = 0;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("Hello");
		Upgrade();
	}
}
