using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUltPerk", menuName = "Perk/UltimatePerk")]
public class UltimatePerk : Perk
{
    public Abilit_Object ultimateToUpgrade;
    public UltimateUpgrade upgradeProperty;
    public float upgradeValue;

    private void OnEnable()
    {
        upgradeType = UpgradeType.Ultimate;
    }
}
