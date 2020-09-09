using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerPerk", menuName = "Perk/PlayerPerk")]
public class PlayerPerk : Perk
{
    public UpgradeProperty upgradeProperty;
    public float upgradeValue;

    private void OnEnable()
    {
        upgradeType = UpgradeType.PlayerUpgrade;
    }
}
