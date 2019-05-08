using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun
{


    public Transform spawnPoint;
    public float fireRate;
    public GameObject projectile;
    public float projectileForce;

    float timer = 0;

    public override void Shooting(bool isPlayer)
    {
        if (ammo > 0 || ammo == -999)
        {
            timer += fireRate * Time.deltaTime;
            if (timer >= 1)
            {
                if (ammo != -999)
                {
                    ammo--;
                }
                Shoot(isPlayer);
                timer = 0;
            }
        }
    }
    private void Shoot(bool isPlayer)
    {
        GameObject go = Instantiate(projectile, spawnPoint);
        if (isPlayer)
        {
            go.layer = 12;
        }
        else
        {
            go.layer = 11;
        }
        base.Playsound();
    }

}
