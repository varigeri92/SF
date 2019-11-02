using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType { PlayerUpgrade, GunUpgrade, Ultimate, ItemUnlock}
public enum UpgradeProperty{ Damage, FireRate, Health, Other}

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
	public string descriptionText;

	public UpgradeType upgradeType;
	public GunObject gunToUpgrade;
	public PlayerObject playerObject;

	public UpgradeProperty upgradeProperty;
	public float upgradeValue;

	public GameObject ItemToUnlock;
}
