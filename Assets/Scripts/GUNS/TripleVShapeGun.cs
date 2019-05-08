using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleVShapeGun : Gun
{
    public Transform[] SpawnPoints;

    public GameObject projectile;
    float timer = 0;
    public float fireRate;


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

    void Shoot(bool isPlayer)
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            GameObject go = Instantiate(projectile, SpawnPoints[i].position, transform.rotation);
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
}
