using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : Gun {
	

	public Transform spawnPoint;
	public float fireRate;
	public GameObject projectile;
	public float projectileForce;

	float timer = 0;

	private void OnEnable()
	{
		if (gunObject != null) {
			fireRate = gunObject.fireRate;
		}
	}

	public override void Shooting(bool isPlayer)
	{
		if (ammo > 0 || ammo == -999) {
			timer += fireRate * Time.deltaTime;
			if (timer >= 1) {
				if (ammo != -999) {
					ammo--;
				}
				Shoot(isPlayer);
				timer = 0;
			}
		}
	}

	private void Shoot( bool isPlayer){
        //POOL
		GameObject go = Instantiate(projectile, spawnPoint.position, transform.rotation);
		if(isPlayer){
			go.layer = 12;
		}else{
			go.layer = 11;
		}
		base.Playsound();

		if(gunObject != null && isPlayer){
			go.GetComponent<ProjectileBehaviour>().dmg = gunObject.damage;	
		}
	}

}
