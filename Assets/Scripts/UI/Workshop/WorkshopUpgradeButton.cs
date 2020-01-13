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
	Color IconColor;

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
		workshopUI = GameObject.FindGameObjectWithTag("Menu_Canvas").GetComponent<WorkshopUI>();


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
						icon.color = IconColor;
					} else {
						available = false;
						baseButton.color = notEnoughCoresColor;
						icon.color = IconColor;
					}
				}
			} else {
				if ((buttonObject.playerObject.powerCores - buttonObject.cost) > -1) {
					available = true;
					baseButton.color = availableColor;
					icon.color = IconColor;
				} else {
					available = false;
					baseButton.color = notEnoughCoresColor;
					icon.color = IconColor;
				}
			}
		}

		if(buttonObject.upgraded){
			baseButton.color = availableColor;
		}
		string lvlString = "";

		if(buttonObject.lvl >= 4){
			lvlString = buttonObject.lvl + "+";
		}else{
			
			for (int i = 0; i < buttonObject.lvl; i++) {
				lvlString += "+";
			}
		}


		level.text = lvlString;
		cost.text = buttonObject.cost.ToString();

		icon.sprite = buttonObject.icon;
	}


	void OnUpgrade(){
		upgradedHighlight.SetActive(true);
		buttonObject.upgraded = true;
		buttonObject.playerObject.powerCores -= buttonObject.cost;
		buttonObject.playerObject.level++;
		workshopUI.Upgraded();
	}




	public void Upgrade(){
		if(available && !buttonObject.upgraded){
			switch(buttonObject.upgradeType){
				case UpgradeType.PlayerUpgrade :

					switch (buttonObject.upgradeProperty) {
						case UpgradeProperty.Health:
							buttonObject.playerObject.maxHealth += (int)buttonObject.upgradeValue;
							break;
						case UpgradeProperty.Speed:
							buttonObject.playerObject.speed += buttonObject.upgradeValue;
							break;
						case UpgradeProperty.BoostDuration:
							buttonObject.playerObject.boostDecSpeed -= buttonObject.upgradeValue;
							break;
						case UpgradeProperty.BoostRefill:
							buttonObject.playerObject.boostFillSpeed += buttonObject.upgradeValue;
							break;
						default:
							break;
					}
					OnUpgrade();
					break;
				case UpgradeType.GunUpgrade:

					if (buttonObject.upgradeProperty == UpgradeProperty.Damage) {
						buttonObject.gunToUpgrade.damage += (int)buttonObject.upgradeValue;
					}else if(buttonObject.upgradeProperty == UpgradeProperty.FireRate){
						buttonObject.gunToUpgrade.fireRate += buttonObject.upgradeValue;
					}else if(buttonObject.upgradeProperty == UpgradeProperty.Ammo){
						buttonObject.gunToUpgrade.maxAmmo += (int)buttonObject.upgradeValue;
						buttonObject.gunToUpgrade.startingAmmo += (int)buttonObject.upgradeValue;
					}
					OnUpgrade();

					break;
				case UpgradeType.Ultimate:

					switch (buttonObject.ultimateUpgrade) {
						case UltimateUpgrade.Damage:
							buttonObject.ultimateToUpgrade.damage += (int)buttonObject.upgradeValue;
							break;
						case UltimateUpgrade.Charges:
							buttonObject.ultimateToUpgrade.charges += (int)buttonObject.upgradeValue;
							break;
						case UltimateUpgrade.Duration:
							buttonObject.ultimateToUpgrade.duration += buttonObject.upgradeValue;
							break;
						default:
							Debug.Log("This UltimateProperty-Type is not implemented in this switch state!");
							return;
					}
					OnUpgrade();
					break;

				case UpgradeType.ItemUnlock:
					switch (buttonObject.unlockableType) {
						case UnlockableType.Gun:
							buttonObject.playerObject.availableGuns.Add(buttonObject.ItemToUnlock);
							SaveManager.Instance.AddAvailableGun(buttonObject.gunToUpgrade.index);
							break;
						case UnlockableType. Ultimate:
							buttonObject.playerObject.ultimates.Add(buttonObject.ItemToUnlock);
							SaveManager.Instance.AddAvailableUlt(buttonObject.ultimateToUpgrade.index);
							break;
						default:
							Debug.Log("This Unlockable Type is not implemented in this switch state!");
							return;

					}
					OnUpgrade();
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
		Upgrade();
	}
}
