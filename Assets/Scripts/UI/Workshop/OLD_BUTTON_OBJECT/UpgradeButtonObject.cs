using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Upgrade Button", menuName = "Ubgrade Button")]
public class UpgradeButtonObject : ScriptableObject
{
	public UpgradeButtonObject required;
	public int requireLevel;
	public bool upgraded;
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
