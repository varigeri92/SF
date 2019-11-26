using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun
{


    public Transform spawnPoint;
    public float fireRate;
    public GameObject projectile;
    public float projectileForce;
    LineRenderer line;

    GameObject hitObject;

    Vector2 hitpoint = new Vector2();
    [SerializeField]
    LayerMask mask;

    float timer = 0;
    private void OnEnable()
    {
        timer = 1;
        line = GetComponent<LineRenderer>();
            // Instantiate(projectile, spawnPoint).GetComponent<LineRenderer>();
    }
    private void Update()
    {
       
    }

    public override void Shooting(bool isPlayer)
    {
        RaycastHit2D hit = Physics2D.Raycast(spawnPoint.position, spawnPoint.transform.up, 200f, mask);

        if (hit)
        {
            hitpoint = hit.point;
            Debug.DrawLine(spawnPoint.position, hit.point);
            line.SetPosition(0, spawnPoint.transform.position);
            line.SetPosition(1, hitpoint);
            hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("Projectile"))
            {
                Destroy(hitObject);
            }
        }

        if (ammo > 0 || ammo == -999)
        {
            line.enabled = true;
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
        else if (ammo != -999 && ammo <= 0)
        {
            StopShooting();
        }
        base.Shooting(isPlayer);
    }
    public override void StopShooting()
    {
        line.enabled = false;
    }

    private void Shoot(bool isPlayer)
    {
        if (hitObject != null)
        {
            if (hitObject.CompareTag("Enemy"))
            {
                if (hitObject.GetComponent<Enemy>() != null)
                {
                    hitObject.GetComponent<Enemy>().TakeDmg(1);
                }else if(hitObject.GetComponent<SwarmObject>() != null)
                {
                    hitObject.GetComponent<SwarmObject>().TakeDmg(1);
                }
            }
        }    
       base.Playsound();
    }

}
