using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGunPerk", menuName = "Perk/GunPerk")]
public class GunPerk : Perk
{
    public GunObject gunToUpgrade;

    public UpgradeProperty upgradeProperty;
    public float upgradeValue;

    public GameObject gunToUnlock;

    private void OnEnable()
    {
        upgradeType = UpgradeType.GunUpgrade;
    }
}
