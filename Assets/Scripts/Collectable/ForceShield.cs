using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceShield : PowerUp
{

    PowerUpType type = PowerUpType.Shield;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().PickupForceShield();
            base.PickedUp();
        }
    }
}
