using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGun : MonoBehaviour
{
    [SerializeField]
    private int _ammo;

    public void AddAmmo(int ammo)
    {
        _ammo += ammo;
        Debug.Log("Ammo added! Ammo:" + _ammo);
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
