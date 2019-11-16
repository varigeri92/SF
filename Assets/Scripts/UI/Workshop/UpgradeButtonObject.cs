using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType { PlayerUpgrade, GunUpgrade, Ultimate, ItemUnlock}
public enum UpgradeProperty{ Damage, FireRate, Health, Ammo, Speed, BoostDuration, BoostRefill}
public enum UnlockableType {Gun, Ultimate}
public enum UltimateUpgrade {Charges, Duration, Damage}

[CreateAssetMenu(fileName = "New Upgrade Button", menuName = "Ubgrade Button")]
public class UpgradeButtonObject : ScriptableObject
{
	public UpgradeButtonObject required;
	public int requireLevel;
	public bool upgraded = false;
	public Sprite icon;
	public int cost;
	public int lvl;

	public string tittleText;
	[Multiline]
	public string descriptionText;

	public UpgradeType upgradeType;
	public GunObject gunToUpgrade;
	public PlayerObject playerObject;
	public Abilit_Object ultimateToUpgrade;

	public UltimateUpgrade ultimateUpgrade;

	public UnlockableType unlockableType;

	public UpgradeProperty upgradeProperty;
	public float upgradeValue;

	public GameObject ItemToUnlock;
}
