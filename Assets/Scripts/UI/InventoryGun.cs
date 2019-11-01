﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGun : MonoBehaviour
{
    [SerializeField]
    private int _ammo;
	public GunObject gunObject;

    public void AddAmmo(int ammo)
    {
		int precalcAmmo = _ammo + ammo;
		if(precalcAmmo >= gunObject.maxAmmo){
			_ammo = gunObject.maxAmmo;
		}else{
			_ammo = precalcAmmo;
		}

        //Debug.Log("Ammo added! Ammo:" + _ammo);
    }

    public void SetAmmo(int newAmmo)
    {
        _ammo = newAmmo;
        Debug.Log(_ammo);
    }

    public int GetAmmo()
    {
        return _ammo;
    }

}
