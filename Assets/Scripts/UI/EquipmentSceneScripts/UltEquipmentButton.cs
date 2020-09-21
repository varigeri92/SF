using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltEquipmentButton : EquipmentButton
{
    public Abilit_Object ultimate;
    void Start()
    {

        
        Init();
    }


    public void SetPalyerUltimate()
    {
        equipmentUIManager.SetUltimate(ultimate);
    }


    public override void OnButtonClick()
    {
        base.OnButtonClick();
        SetPalyerUltimate();
    }
}
