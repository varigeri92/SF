using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BASECLASS//
public class Perk : ScriptableObject
{
    public UpgradeType upgradeType { get; protected set; }
    public bool upgraded;
    public Perk required;
    public int requireLevel;
    public Sprite icon;
    public int cost;
    public int rank;

    public string tittleText;
    [Multiline]
    public string descriptionText;
}

