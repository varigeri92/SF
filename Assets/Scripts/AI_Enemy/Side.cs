using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side : BasicEnemy
{

    Gun sideGun;
    public bool baseDead = false;
    public bool following;

    float movetime = 1.5f;

    FourSideEnemy center;
    

    private void OnEnable()
    {
        health = enemyObject.health;
        sideGun = GetComponentInChildren<Gun>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        int rndm = Random.Range(0, 2);
        if (rndm == 0)
        {
            left = false;
        }

        center = GetComponentInParent<FourSideEnemy>();
        
    }

    public override void Die()
    {
        if (center != null)
        {
            center.Sides.Remove(this);
            if (center.Sides.Count == 0)
            {
                center.Die();
            }
        }

        base.Die();

    }

    public override void Follow()
    {
        if (baseDead && !following)
        {
            transform.Translate(Vector2.up * enemyObject.mooveSpeed * Time.deltaTime);
            movetime -= Time.deltaTime;
            if (movetime <= 0)
            {
                following = true;
            }
        }
        else if(following)
        {
            base.Follow();
        }

    }

    public override void Shoot()
    {
        base.Shoot(sideGun);
    }


}
