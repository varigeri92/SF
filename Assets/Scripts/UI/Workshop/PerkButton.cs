using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class PerkDataStruct
{
    public Color IconColor;
    public Color availableColor;
    public Color lockedColor;
    public Color notEnoughCoresColor;
    public GameObject upgradedHighlight;
    public TMP_Text costText;
    public TMP_Text rankText;
    public Image icon;
}

public class PerkButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    // EVENTS
    public delegate void PointerEnterDelegate(string description, string tittle);
    public static event PointerEnterDelegate OnPointerEntered;

    public delegate void PointerExitDelegate();
    public static event PointerExitDelegate OnPointerExited;


    // NEW:
    // [SerializeField] UpgradeButtonObject perk;
    [SerializeField] PerkDataStruct perkData;
    [SerializeField] Perk perk;

    bool available;

    WorkshopUI workshopUI;
    Transform gunGrid;
    Transform ultimateGrid;

    private void Start()
	{
        // Gathering references:
		workshopUI = GameObject.FindGameObjectWithTag("Menu_Canvas").GetComponent<WorkshopUI>();
        gunGrid = GameObject.FindGameObjectWithTag("gunGrid").transform;
        ultimateGrid = GameObject.FindGameObjectWithTag("ultimateGrid").transform;

		WorkshopUI.OnUpgrade += InitButton;
		InitButton();
	}

	private void OnDestroy()
	{
		WorkshopUI.OnUpgrade -= InitButton;
	}

	void InitButton(){

		Image baseButton = GetComponent<Image>();
		baseButton.color = perkData.lockedColor;

		if (perk.upgraded) {
            perkData.upgradedHighlight.SetActive(true);
		}
		if(perk.requireLevel <= Global.Instance.PlayerData.level){
			if (perk.required != null) {
				if (perk.required.upgraded) {
					if ((Global.Instance.PlayerData.powerCores - perk.cost) > -1) {
						available = true;
						baseButton.color = perkData.availableColor;
                        perkData.icon.color = perkData.IconColor;
					} else {
						available = false;
						baseButton.color = perkData.notEnoughCoresColor;
                        perkData.icon.color = perkData.IconColor;
					}
				}
			} else {
				if ((Global.Instance.PlayerData.powerCores - perk.cost) > -1) {
					available = true;
					baseButton.color = perkData.availableColor;
                    perkData.icon.color = perkData.IconColor;
				} else {
					available = false;
					baseButton.color = perkData.notEnoughCoresColor;
                    perkData.icon.color = perkData.IconColor;
				}
			}
		}

		if(perk.upgraded){
			baseButton.color = perkData.availableColor;
		}
		string lvlString = "";

		if(perk.rank >= 4){
			lvlString = perk.rank + "+";
		}else{
			
			for (int i = 0; i < perk.rank; i++) {
				lvlString += "+";
			}
		}


        perkData.rankText.text = lvlString;
        perkData.costText.text = perk.cost.ToString();

        perkData.icon.sprite = perk.icon;
	}


	void OnUpgrade(){
        perkData.upgradedHighlight.SetActive(true);
		perk.upgraded = true;
        Global.Instance.PlayerData.powerCores -= perk.cost;
        Global.Instance.PlayerData.level++;
		workshopUI.Upgraded();
	}



	public void Upgrade(){
		if(available && !perk.upgraded){
			switch(perk.upgradeType){
				case UpgradeType.PlayerUpgrade :
                    {
                        PlayerPerk perk = this.perk as PlayerPerk;
                        switch (perk.upgradeProperty)
                        {
                            case UpgradeProperty.Health:
                                Global.Instance.PlayerData.maxHealth += (int)perk.upgradeValue;
                                break;
                            case UpgradeProperty.Speed:
                                Global.Instance.PlayerData.speed += perk.upgradeValue;
                                break;
                            case UpgradeProperty.BoostDuration:
                                Global.Instance.PlayerData.boostDecSpeed -= perk.upgradeValue;
                                break;
                            case UpgradeProperty.BoostRefill:
                                Global.Instance.PlayerData.boostFillSpeed += perk.upgradeValue;
                                break;
                            default:
                                break;
                        }
                        OnUpgrade();
                    }
					break;
				case UpgradeType.GunUpgrade:
                    {
                        GunPerk perk = this.perk as GunPerk;
                        switch (perk.upgradeProperty)
                        {
                            case UpgradeProperty.Damage:
                                perk.gunToUpgrade.damage += (int)perk.upgradeValue;
                                break;
                            case UpgradeProperty.FireRate:
                                perk.gunToUpgrade.fireRate += perk.upgradeValue;
                                break;
                            case UpgradeProperty.Ammo:
                                perk.gunToUpgrade.maxAmmo += (int)perk.upgradeValue;
                                perk.gunToUpgrade.startingAmmo += (int)perk.upgradeValue;
                                break;
                            case UpgradeProperty.Unlock:
                                Global.Instance.PlayerData.availableGuns.Add(perk.gunToUpgrade);
                                // SaveManager.Instance.AddAvailableGun(buttonObject.gunToUpgrade.index);
                                Instantiate(perk.gunToUpgrade.equipmentGridPrefab, gunGrid);
                                break;
                            default:
                                Debug.Log("Enumeration Missmach: Gun doeas not have a matchin propeety for " 
                                    + perk.upgradeProperty.ToString());
                                break;
                        }
                        OnUpgrade();
                    }
					break;
				case UpgradeType.Ultimate:
                    {
                        UltimatePerk perk = this.perk as UltimatePerk;
                        switch (perk.upgradeProperty) {
						    case UltimateUpgrade.Damage:
							    perk.ultimateToUpgrade.damage += (int)perk.upgradeValue;
							    break;
						    case UltimateUpgrade.Charges:
							    perk.ultimateToUpgrade.charges += (int)perk.upgradeValue;
							    break;
						    case UltimateUpgrade.Duration:
							    perk.ultimateToUpgrade.duration += perk.upgradeValue;
							    break;
                            case UltimateUpgrade.Unlock:
                                Global.Instance.PlayerData.unlockedUltimates.Add(perk.ultimateToUpgrade);
                                // SaveManager.Instance.AddAvailableUlt(buttonObject.ultimateToUpgrade.index);
                                Instantiate(perk.ultimateToUpgrade.equipmentGridPrefab, ultimateGrid);
                                break;
                            default:
							    Debug.Log("This UltimateProperty-Type is not implemented in this switch state!");
							    return;
					    }
					    OnUpgrade();
                    }  
					break;
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
        if (OnPointerEntered!=null)
            OnPointerEntered(perk.name, perk.name);
	}
	
	public void OnPointerExit(PointerEventData eventData)
	{
        if (OnPointerExited != null)
            OnPointerExited();
    }

	public void OnPointerDown(PointerEventData eventData)
	{
		Upgrade();
	}
}
